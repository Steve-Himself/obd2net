using System.Collections.Generic;
using System.Linq;
using Obd2Net.Extensions;
using Obd2Net.InfrastructureContracts;
using Obd2Net.InfrastructureContracts.Enums;
using Obd2Net.InfrastructureContracts.Protocols;

namespace Obd2Net.Protocols
{
    public abstract class ProtocolBase : IProtocol
    {
        private const string Space = " ";

        public IDictionary<int, ECU> EcuMap { get; private set; } = new Dictionary<int, ECU>();

        protected abstract int TxIdEngine { get; }

        public abstract string ElmName { get; }
        public abstract string ElmId { get; }

        public abstract bool ParseFrame(IFrame frame);
        public abstract bool ParseMessage(IMessage message);

        public IMessage[] Parse(params string[] lines)
        {
            var obdLines = new List<string>();
            var nonOBDLines = new List<string>();

            foreach (var line in lines)
            {
                var lineNoSpaces = line.Replace(Space, string.Empty);

                if (Utils.IsHex(lineNoSpaces))
                    obdLines.Add(lineNoSpaces);
                else
                    nonOBDLines.Add(line); // pass the original, un-scrubbed line
            }

            // ---------------------- handle valid OBD lines ----------------------

            // # parse each frame (each line)
            var frames = new List<IFrame>();
            foreach (var line in obdLines)
            {
                var frame = new Frame(line);

                //# subclass function to parse the lines into Frames
                //# drop frames that couldn't be parsed
                if (ParseFrame(frame))
                    frames.Add(frame);
            }
            //# group frames by transmitting ECU
            //# frames_by_ECU[tx_id] = [Frame, Frame]
            var framesByECU = new Dictionary<int, List<IFrame>>();
            foreach (var frame in frames)
            {
                if (!framesByECU.ContainsKey(frame.TxId))
                {
                    framesByECU[frame.TxId] = new List<IFrame> {frame};
                }
                else
                {
                    framesByECU[frame.TxId].Add(frame);
                }
            }
            //# parse frames into whole messages
            var messages = new List<IMessage>();
            foreach (var ecu in framesByECU)
            {
                //# new message object with a copy of the raw data
                //# and frames addressed for this ecu
                var message = new Message(ecu.Value.ToArray());

                //# subclass function to assemble frames into Messages
                if (ParseMessage(message))
                {
                    //# mark with the appropriate ECU ID
                    message.Ecu = EcuMap.ContainsKey(ecu.Key) ? EcuMap[ecu.Key] : ECU.Unknown;
                    messages.Add(message);
                }
            }
            //# ----------- handle invalid lines (probably from the ELM) -----------

            foreach (var line in nonOBDLines)
            {
                //# give each line its own message object
                //# messages are ECU.UNKNOWN by default
                messages.Add(new Message(new Frame(line)));
            }

            return messages.ToArray();
        }

        public void PopulateEcuMap(params string[] lines)
        {
            var messages = Parse(lines);
            EcuMap = new Dictionary<int, ECU>();

            //  filter out messages that don't contain any data
            //  this will prevent ELM responses from being mapped to ECUs
            messages = messages.Where(m => m.Parsed && m.TxId.HasValue).ToArray();

            //  populate the map
            if (messages.Length == 0)
                return;

            if (messages.Length == 1 && messages[0].TxId.HasValue)
            {
                //  if there's only one response, mark it as the engine regardless
                EcuMap[messages[0].TxId.Value] = ECU.Engine;
                return;
            }

            //  the engine is important
            //  if we can't find it, we'll use a fallback
            var foundEngine = false;

            //  if any TxIds are exact matches to the expected values, record them
            foreach (var m in messages)
            {
                if (!m.TxId.HasValue || m.TxId != TxIdEngine) continue;

                EcuMap[m.TxId.Value] = ECU.Engine;
                foundEngine = true;
                //  TODO: program more of these when we figure out their constants
                //  elif m.TxId == TX_ID_TRANSMISSION:
                //  EcuMap[m.TxId] = ECU.TRANSMISSION
            }

            if (!foundEngine)
            {
                //  last resort solution, choose ECU with the most bits set
                //  (most PIDs supported) to be the engine
                var best = 0;
                var txId = 0;

                foreach (var message in messages)
                {
                    var bits = message.Data.NumberOfSetBits();

                    if (bits <= best || !message.TxId.HasValue) continue;

                    best = bits;
                    txId = message.TxId.Value;
                }
                EcuMap[txId] = ECU.Engine;
            }
            //  any remaining TxIds are unknown
            foreach (var m in messages)
            {
                if (m.TxId.HasValue && EcuMap.ContainsKey(m.TxId.Value))
                    EcuMap[m.TxId.Value] = ECU.Unknown;
            }
        }
    }
}