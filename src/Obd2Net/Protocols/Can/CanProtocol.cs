using System.Collections.Generic;
using System.Linq;
using Obd2Net.Extensions;
using Obd2Net.InfrastructureContracts.Protocols;

namespace Obd2Net.Protocols.Can
{
    public abstract class CanProtocol : ProtocolBase
    {
        private const byte FrameTypeSingleFrame = 0x00; // single frame
        private const byte FrameTypeFirstFrame = 0x10; // first frame of multi-frame message
        private const byte FrameTypeConsecutiveFrame = 0x20; // consecutive frame(s) of multi-frame message

        protected abstract int IdBits { get; }
        protected override int TxIdEngine => 0;

        public override bool ParseFrame(IFrame frame)
        {
            var raw = frame.Raw;

            // pad 11-bit CAN headers out to 32 bits for consistency,
            // since ELM already does this for 29-bit CAN headers

            //        7 E8 06 41 00 BE 7F B8 13
            // to:
            // 00 00 07 E8 06 41 00 BE 7F B8 13

            if (IdBits == 11)
                raw = "00000" + raw;

            var rawBytes = raw.ToByteArray();

            // check for valid size

            // TODO: lookup this limit
            // if len(raw_bytes) < 9:
            //     debug("Dropped frame for being too short")
            //     return false;

            // TODO: lookup this limit
            // if len(raw_bytes) > 16:
            //     debug("Dropped frame for being too long")
            //     return false;


            // read header information
            if (IdBits == 11)
            {
                // Ex.
                //       [   ]
                // 00 00 07 E8 06 41 00 BE 7F B8 13

                frame.Priority = rawBytes[2] & 0x0F; // always 7
                frame.AddrMode = rawBytes[3] & 0xF0; // 0xD0 = functional, 0xE0 = physical

                if (frame.AddrMode == 0xD0)
                {
                    //untested("11-bit functional request from tester")
                    frame.RxId = rawBytes[3] & 0x0F;
                    // usually (always?) 0x0F for broadcast
                    frame.TxId = 0xF1; // made-up to mimic all other protocols
                }
                else if ((rawBytes[3] & 0x08) != 0)
                {
                    frame.RxId = 0xF1; // made-up to mimic all other protocols
                    frame.TxId = rawBytes[3] & 0x07;
                }
                else
                {
                    //untested("11-bit message header from tester (functional or physical)")
                    frame.TxId = 0xF1; // made-up to mimic all other protocols
                    frame.RxId = rawBytes[3] & 0x07;
                }
            }
            else
            {
                // idBits == 29:
                frame.Priority = rawBytes[0]; // usually (always?) 0x18
                frame.AddrMode = rawBytes[1]; // DB = functional, DA = physical
                frame.RxId = rawBytes[2]; // 0x33 = broadcast (functional)
                frame.TxId = rawBytes[3]; // 0xF1 = tester ID
            }
            // extract the frame data
            //             [      Frame       ]
            // 00 00 07 E8 06 41 00 BE 7F B8 13
            frame.Data = rawBytes.Skip(4).ToArray();


            // read PCI byte (always first byte in the data section)
            //             v
            // 00 00 07 E8 06 41 00 BE 7F B8 13
            frame.Type = frame.Data[0] & 0xF0;
            if (frame.Type != FrameTypeSingleFrame && frame.Type != FrameTypeFirstFrame && frame.Type != FrameTypeConsecutiveFrame)
            {
                //debug("Dropping frame carrying unknown PCI frame type")
                return false;
            }

            if (frame.Type == FrameTypeSingleFrame)
            {
                // single frames have 4 bit length codes
                //              v
                // 00 00 07 E8 06 41 00 BE 7F B8 13
                frame.DataLen = frame.Data[0] & 0x0F;
            }
            else if (frame.Type == FrameTypeFirstFrame)
            {
                // First frames have 12 bit length codes
                //              v
                // 00 00 07 E8 06 41 00 BE 7F B8 13
                frame.DataLen = (frame.Data[0] & 0x0F) << 8;
                frame.DataLen += frame.Data[1];
            }
            else if (frame.Type == FrameTypeConsecutiveFrame)
            {
                // Consecutive frames have 4 bit sequence indices
                frame.SeqIndex = frame.Data[0] & 0x0F;
            }
            return true;
        }

        public override bool ParseMessage(IMessage message)
        {
            var frames = message.Frames;

            if (frames.Length == 1)
            {
                var frame = frames[0];

                if (frame.Type != FrameTypeSingleFrame)
                    return false; // Recieved lone frame not marked as single frame

                // extract data, ignore PCI byte and anything after the marked length
                //             [      Frame       ]
                //                [     Data      ]
                // 00 00 07 E8 06 41 00 BE 7F B8 13 xx xx xx xx, anything else is ignored
                message.Data = frame.Data.Skip(1).ToArray();
            }
            else
            {
                // sort FF and CF into their own lists

                var ff = new List<IFrame>();
                var cf = new List<IFrame>();

                foreach (var f in frames)
                {
                    if (f.Type == FrameTypeFirstFrame)
                        ff.Add(f);
                    else if (f.Type == FrameTypeConsecutiveFrame)
                        cf.Add(f);
                    // else Dropping frame in multi-frame response not marked as FF or CF
                }

                // check that we captured only one first-frame
                if (ff.Count > 1)
                    return false; // debug("Recieved multiple frames marked FF")
                if (ff.Count == 0)
                    return false; // debug("Never received frame marked FF")

                // check that there was at least one consecutive-frame
                if (cf.Count == 0)
                    return false; // debug("Never received frame marked CF")

                // calculate proper sequence indices from the lower 4 bits given
                for (var i = 1; i < cf.Count; i += 2)
                {
                    var prev = cf[i - 1];
                    var curr = cf[i];

                    // Frame sequence numbers only specify the low order bits, so compute the
                    // full sequence number from the frame number and the last sequence number seen:
                    // 1) take the high order bits from the last_sn and low order bits from the frame
                    var seq = (prev.SeqIndex & ~0x0F) + curr.SeqIndex;
                    // 2) if this is more than 7 frames away, we probably just wrapped (e.g.,
                    // last=0x0F current=0x01 should mean 0x11, not 0x01)
                    if (seq < prev.SeqIndex - 7)
                        // untested
                        seq += 0x10;

                    curr.SeqIndex = seq;
                }

                // sort the sequence indices
                cf = cf.OrderBy(k => k.SeqIndex).ToList();

                // check contiguity, and that we aren't missing any frames
                var isConsecutive = !cf.Select((i, j) => i.SeqIndex - j).Distinct().Skip(1).Any();
                if (!isConsecutive)
                    return false; // debug("Recieved multiline response with missing frames")

                // first frame:
                //             [       Frame         ]
                //             [PCI]                   <-- first frame has a 2 byte PCI
                //              [L ] [     Data      ] L = length of message in bytes
                // 00 00 07 E8 10 13 49 04 01 35 36 30

                // consecutive frame:
                //             [       Frame         ]
                //             []                       <-- consecutive frames have a 1 byte PCI
                //              N [       Data       ]  N = current frame number (rolls over to 0 after F)
                // 00 00 07 E8 21 32 38 39 34 39 41 43
                // 00 00 07 E8 22 00 00 00 00 00 00 31

                // original data:
                // [     specified message length (from first-frame)      ]
                // 49 04 01 35 36 30 32 38 39 34 39 41 43 00 00 00 00 00 00 31


                // on the first frame, skip PCI byte AND length code
                var data = ff[0].Data.Skip(2).ToList();

                // now that they're in order, load/accumulate the data from each CF frame
                foreach (var f in cf)
                {
                    data.AddRange(f.Data.Skip(1).ToArray()); // chop off the PCI byte    
                }

                // chop to the correct size (as specified in the first frame)
                message.Data = data.Take(ff[0].DataLen).ToArray();
            }

            // chop off the Mode/PID bytes based on the mode number
            var mode = message.Data[0];
            if (mode == 0x43)
            {
                // TODO: confirm this logic. I don't have any raw test data for it yet

                // fetch the DTC count, and use it as a length code
                var numDTCBytes = message.Data[1]*2;

                // skip the PID byte and the DTC count,
                message.Data = message.Data.Skip(2).Take(numDTCBytes).ToArray();
            }
            else
            {
                // skip the Mode and PID bytes
                //
                // single line response:
                //                      [  Data   ]
                // 00 00 07 E8 06 41 00 BE 7F B8 13
                //
                // OR, the data from a multiline response:
                //       [                     Data                       ]
                // 49 04 01 35 36 30 32 38 39 34 39 41 43 00 00 00 00 00 00
                message.Data = message.Data.Skip(2).ToArray();
            }
            return true;
        }
    }
}