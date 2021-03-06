using System;
using Obd2Net.Extensions;
using Obd2Net.Infrastructure;
using Obd2Net.InfrastructureContracts;
using Obd2Net.InfrastructureContracts.Enums;
using Obd2Net.InfrastructureContracts.Protocols;
using Obd2Net.InfrastructureContracts.Response;

namespace Obd2Net.Decoders
{
    internal sealed class FuelTypeDecoder : IDecoder<string>
    {
        public IOBDResponse<string> Execute(params IMessage[] messages)
        {
            var d = messages[0].Data;
            var v = (int)d[0];

            if (v <= 0 || !Enum.IsDefined(typeof(FuelType), v))
                return new OBDResponse<string>("Error: Unknown fuel type response", Unit.None);

            return new OBDResponse<string>(((FuelType)v).GetDescription(), Unit.None);
        }
    }
}