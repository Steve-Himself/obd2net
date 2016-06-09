using System;
using Obd2Net.Infrastructure;
using Obd2Net.InfrastructureContracts.Enums;
using Obd2Net.InfrastructureContracts.Protocols;
using Obd2Net.InfrastructureContracts.Response;

namespace Obd2Net.Decoders
{
    internal sealed class SecondsDecoder : IDecoder<uint>
    {
        public IOBDResponse<uint> Execute(params IMessage[] messages)
        {
            var data = messages[0].Data;
            var v = Convert.ToUInt32(Utils.BytesToInt(data));
            return new OBDResponse<uint>(v, Unit.Sec);
        }
    }
}