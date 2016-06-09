using Obd2Net.Infrastructure;
using Obd2Net.InfrastructureContracts;
using Obd2Net.InfrastructureContracts.Enums;
using Obd2Net.InfrastructureContracts.Protocols;
using Obd2Net.InfrastructureContracts.Response;

namespace Obd2Net.Decoders
{
    internal sealed class DropDecoder : IDecoder<string>
    {
        public IOBDResponse<string> Execute(params IMessage[] messages)
        {
            return new OBDResponse<string>(null, Unit.None);
        }
    }
}