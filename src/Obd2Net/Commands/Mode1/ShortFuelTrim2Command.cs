using Obd2Net.Decoders;
using Obd2Net.InfrastructureContracts.Enums;

namespace Obd2Net.Commands.Mode1
{
    internal class ShortFuelTrim2Command : OBDCommandBase<PercentCenteredDecoder, decimal>
    {
        public ShortFuelTrim2Command(PercentCenteredDecoder decoder) :
            base(decoder, Mode.Mode1, "SHORT_FUEL_TRIM_2", "Short Term Fuel Trim - Bank 2", "0108", 1, ECU.Engine, true)
        {
        }
    }
}