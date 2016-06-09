using System;
using Obd2Net.Infrastructure;
using Obd2Net.InfrastructureContracts;
using Obd2Net.InfrastructureContracts.Enums;
using Obd2Net.InfrastructureContracts.Protocols;
using Obd2Net.InfrastructureContracts.Response;

namespace Obd2Net.Decoders
{
    internal sealed class RpmDecoder : IDecoder<uint>
    {
        public IOBDResponse<uint> Execute(params IMessage[] messages)
        {
            var d = messages[0].Data;
            var v = Convert.ToUInt32(Utils.BytesToInt(d));
            if (v > 0)
                v = v / 4;
            return new OBDResponse<uint>(v, Unit.Rpm);
        }
    }
}