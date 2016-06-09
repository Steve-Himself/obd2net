﻿using Obd2Net.Infrastructure;
using Obd2Net.InfrastructureContracts.Enums;
using Obd2Net.InfrastructureContracts.Protocols;
using Obd2Net.InfrastructureContracts.Response;

namespace Obd2Net.Decoders
{
    internal sealed class EvapPressureAltDecoder : IDecoder<int>
    {
        public IOBDResponse<int> Execute(params IMessage[] messages)
        {
            var d = messages[0].Data;
            var v = Utils.BytesToInt(d) - 32767;

            return new OBDResponse<int>(v, Unit.Pa);
        }
    }
}