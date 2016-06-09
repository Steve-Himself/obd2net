using Obd2Net.Infrastructure;
using Obd2Net.InfrastructureContracts.Enums;
using Obd2Net.InfrastructureContracts.Protocols;
using Obd2Net.InfrastructureContracts.Response;

namespace Obd2Net.Decoders
{
    internal sealed class ElmVoltageDecoder : IDecoder<decimal?>
    {
        public IOBDResponse<decimal?> Execute(params IMessage[] messages)
        {
            decimal v;
            if (decimal.TryParse(messages[0].Frames[0].Raw, out v))
                return new OBDResponse<decimal?>(v, Unit.Volt);

            return new OBDResponse<decimal?>(null, Unit.None);
        }
    }
}