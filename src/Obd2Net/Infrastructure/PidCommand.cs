using Obd2Net.InfrastructureContracts.Enums;

namespace Obd2Net.Infrastructure
{
    internal class PidCommand : OBDCommand<string>
    {
        public PidCommand(string name, string description, string command, int bytes, ECU ecu = ECU.All, bool fast = false) 
            : base(name, description, command, bytes, OldDecoders.Pid, ecu, fast)
        {
        }
    }
}