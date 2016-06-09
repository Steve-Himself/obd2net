using Obd2Net.Decoders;
using Obd2Net.InfrastructureContracts.Enums;

namespace Obd2Net.Commands.Mode1
{
    internal class LongFuelTrim2Command : OBDCommandBase<PercentCenteredDecoder, decimal>
    {
        public LongFuelTrim2Command(PercentCenteredDecoder decoder) :
            base(decoder, Mode.Mode1, "LONG_FUEL_TRIM_2", "Long Term Fuel Trim - Bank 2", "0109", 1, ECU.Engine, true)
        {
        }
    }
}