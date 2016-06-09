using Obd2Net.Decoders;
using Obd2Net.InfrastructureContracts.Enums;

namespace Obd2Net.Commands.Mode1
{
    internal class PidsACommand : PidCommandBase
    {
        public PidsACommand(PidDecoder decoder) : 
            base(decoder, Mode.Mode1, "PIDS_A", "Supported PIDs [01-20]", "0100", 4, ECU.Engine, true)
        {
        }
    }
}
