using Obd2Net.Decoders;
using Obd2Net.InfrastructureContracts.Enums;

namespace Obd2Net.Commands.Mode1
{
    internal class ShortFuelTrim1Command : OBDCommandBase<PercentCenteredDecoder, decimal>
    {
        public ShortFuelTrim1Command(PercentCenteredDecoder decoder) :
            base(decoder, Mode.Mode1, "SHORT_FUEL_TRIM_1", "Short Term Fuel Trim - Bank 1", "0106", 1, ECU.Engine, true)
        {
        }
    }
}