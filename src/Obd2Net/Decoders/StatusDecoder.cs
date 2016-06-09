using System;
using System.Collections;
using Obd2Net.Extensions;
using Obd2Net.Infrastructure;
using Obd2Net.InfrastructureContracts.Enums;
using Obd2Net.InfrastructureContracts.Protocols;
using Obd2Net.InfrastructureContracts.Response;

namespace Obd2Net.Decoders
{
    internal sealed class StatusDecoder : IDecoder<string>
    {
        public IOBDResponse<string> Execute(params IMessage[] messages)
        {
            var d = messages[0].Data;
            var bits = new BitArray(d);

            var ignitionType = (IgnitionType)Utils.Unbin(bits, 12);
            var output = new Status
            {
                Mil = bits[0],
                DTCCount = Utils.Unbin(bits, 0, 7),
                IgnitionType = ignitionType.GetDescription()
            };

            output.Tests.Add(new Test("Misfire", bits[15], bits[11]));

            output.Tests.Add(new Test("Fuel System", bits[14], bits[10]));

            output.Tests.Add(new Test("Components", bits[13], bits[9]));


            // different tests for different ignition types 
            if (ignitionType == IgnitionType.Spark)
            {
                // spark
                foreach (int i in Enum.GetValues(typeof(SparkTests)))
                {
                    var t = new Test(((SparkTests)i).GetDescription(), bits[2 * 8 + i], bits[3 * 8 + i]);

                    output.Tests.Add(t);
                }
            }
            else if (ignitionType == IgnitionType.Compression)
            {
                // compression
                foreach (int i in Enum.GetValues(typeof(CompressionTest)))
                {
                    var t = new Test(((CompressionTest)i).GetDescription(), bits[2 * 8 + i], bits[3 * 8 + i]);

                    output.Tests.Add(t);
                }
            }

            return new OBDResponse<string>(output.ToString(), Unit.None);
        }
    }
}