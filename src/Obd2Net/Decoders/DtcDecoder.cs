using System;
using System.Collections.Generic;
using System.Linq;
using Obd2Net.Infrastructure;
using Obd2Net.InfrastructureContracts.Enums;
using Obd2Net.InfrastructureContracts.Protocols;
using Obd2Net.InfrastructureContracts.Response;
using Obd2Net.Protocols;

namespace Obd2Net.Decoders
{
    internal sealed class DtcDecoder : IDecoder<IDictionary<string, string>>
    {
        private const string UnknownErrorCode = "Unknown error code";

        public IOBDResponse<IDictionary<string, string>> Execute(params IMessage[] messages)
        {
            var codes = new Dictionary<string, string>();
            var d = messages.SelectMany(m => m.Data).ToArray();

            for (var n = 1; n < d.Length; n += 2)
            {
                var dtc = SingleDtc(new[] { d[n - 1], d[n] });
                if (string.IsNullOrWhiteSpace(dtc)) continue;

                string desc = null;

                if (Codes.DTC.ContainsKey(dtc))
                    desc = Codes.DTC[dtc];
                codes[dtc] = desc ?? UnknownErrorCode;
            }

            return new OBDResponse<IDictionary<string, string>>(codes, Unit.None);
        }

        private string SingleDtc(byte[] bytes)
        {
            if (bytes.Length != 2 || (bytes[0] == 0 && bytes[0] == 0))
                return null;

            // BYTES: (16,      35      )
            // HEX:    4   1    2   3
            // BIN:    01000001 00100011
            //         [][][  in hex   ]
            //         | / /
            // DTC:    C0123
            var type = new[] { "P", "C", "B", "U" };
            var dtc = type[bytes[0] >> 6] + // the last 2 bits of the first byte
                      ((bytes[0] >> 4) & 0x30) + // the next pair of 2 bits. Mask off the bits we read above
                      BitConverter.ToString(bytes, 0).Replace("-", "").Substring(1, 3);
            return dtc;
        }
    }
}