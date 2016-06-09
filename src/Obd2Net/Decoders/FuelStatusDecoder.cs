using System;
using Obd2Net.Extensions;
using Obd2Net.Infrastructure;
using Obd2Net.InfrastructureContracts.Enums;
using Obd2Net.InfrastructureContracts.Protocols;
using Obd2Net.InfrastructureContracts.Response;

namespace Obd2Net.Decoders
{
    internal sealed class FuelStatusDecoder : IDecoder<string>
    {
        public IOBDResponse<string> Execute(params IMessage[] messages)
        {
            var d = messages[0].Data;
            var f1 = (int)d[0]; // Single Fuel System in byte 1

            if (f1 <= 0 || !Enum.IsDefined(typeof(FuelStatus), f1))
                return new OBDResponse<string>(null, Unit.None);

            return new OBDResponse<string>(((FuelStatus)f1).GetDescription(), Unit.None);
        }
    }
}