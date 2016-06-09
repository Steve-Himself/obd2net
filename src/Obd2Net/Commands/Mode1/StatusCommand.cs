using Obd2Net.Decoders;
using Obd2Net.InfrastructureContracts.Enums;

namespace Obd2Net.Commands.Mode1
{
    internal class StatusCommand : OBDCommandBase<StatusDecoder, string>
    {
        public StatusCommand(StatusDecoder decoder) : 
            base(decoder, Mode.Mode1, "STATUS", "Status since DTCs cleared", "0101", 4, ECU.Engine, true)
        {
        }
    }
}
