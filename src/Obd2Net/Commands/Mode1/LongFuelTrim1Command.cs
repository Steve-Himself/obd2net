using Obd2Net.Decoders;
using Obd2Net.InfrastructureContracts.Enums;

namespace Obd2Net.Commands.Mode1
{
    internal class LongFuelTrim1Command : OBDCommandBase<PercentCenteredDecoder, decimal>
    {
        public LongFuelTrim1Command(PercentCenteredDecoder decoder) :
            base(decoder, Mode.Mode1, "LONG_FUEL_TRIM_1", "Long Term Fuel Trim - Bank 1", "0107", 1, ECU.Engine, true)
        {
        }
    }
}