using System;
using Obd2Net.Extensions;
using Obd2Net.Infrastructure;
using Obd2Net.InfrastructureContracts.Enums;
using Obd2Net.InfrastructureContracts.Protocols;
using Obd2Net.InfrastructureContracts.Response;

namespace Obd2Net.Decoders
{
    internal sealed class ObdComplianceDecoder : IDecoder<string>
    {
        public IOBDResponse<string> Execute(params IMessage[] messages)
        {
            var d = messages[0].Data;
            var v = Utils.BytesToInt(d);

            if (v <= 0 || !Enum.IsDefined(typeof(OBDCompliance), v))
                return new OBDResponse<string>("Error: Unknown OBD compliance response", Unit.None);

            return new OBDResponse<string>(((OBDCompliance)v).GetDescription(), Unit.None);
        }
    }
}