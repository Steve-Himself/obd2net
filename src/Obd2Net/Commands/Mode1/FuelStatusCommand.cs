using Obd2Net.Decoders;
using Obd2Net.InfrastructureContracts.Enums;

namespace Obd2Net.Commands.Mode1
{
    internal class FuelStatusCommand : OBDCommandBase<FuelStatusDecoder, string>
    {
        public FuelStatusCommand(FuelStatusDecoder decoder) :
            base(decoder, Mode.Mode1, "FUEL_STATUS", "Fuel System Status", "0103", 2, ECU.Engine, true)
        {
        }
    }
}