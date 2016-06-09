using Obd2Net.Decoders;
using Obd2Net.InfrastructureContracts.Enums;

namespace Obd2Net.Commands.Mode1
{
    internal class FreezeDtcCommand : OBDCommandBase<DropDecoder, string>
    {
        public FreezeDtcCommand(DropDecoder decoder) :
            base(decoder, Mode.Mode1, "FREEZE_DTC", "Freeze DTC", "0102", 2, ECU.Engine, true)
        {
        }
    }
}