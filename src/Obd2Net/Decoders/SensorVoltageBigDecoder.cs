using Obd2Net.Infrastructure;
using Obd2Net.InfrastructureContracts;
using Obd2Net.InfrastructureContracts.Enums;
using Obd2Net.InfrastructureContracts.Protocols;
using Obd2Net.InfrastructureContracts.Response;

namespace Obd2Net.Decoders
{
    internal sealed class SensorVoltageBigDecoder : IDecoder<decimal>
    {
        public IOBDResponse<decimal> Execute(params IMessage[] messages)
        {
            var d = messages[0].Data;
            var x = Utils.BytesToInt(d, 2, 4);
            var v = decimal.Round(x * 8 / 65535m, 1);
            return new OBDResponse<decimal>(v, Unit.Volt);
        }
    }
}