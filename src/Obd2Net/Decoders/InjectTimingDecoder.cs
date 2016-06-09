using System;
using Obd2Net.Infrastructure;
using Obd2Net.InfrastructureContracts;
using Obd2Net.InfrastructureContracts.Enums;
using Obd2Net.InfrastructureContracts.Protocols;
using Obd2Net.InfrastructureContracts.Response;

namespace Obd2Net.Decoders
{
    internal sealed class InjectTimingDecoder : IDecoder<int>
    {
        public IOBDResponse<int> Execute(params IMessage[] messages)
        {
            var d = messages[0].Data;
            var v = (Utils.BytesToInt(d) - 26880) / 128.0m;

            return new OBDResponse<int>(Convert.ToInt32(v), Unit.Degrees);
        }
    }
}