using Obd2Net.Decoders;
using Obd2Net.InfrastructureContracts.Enums;

namespace Obd2Net.Commands
{
    public abstract class PidCommandBase : OBDCommandBase<PidDecoder, string>
    {
        protected PidCommandBase(PidDecoder decoder, Mode mode, string name, string description, string command, int bytes, ECU ecu, bool fast) : base(decoder, mode, name, description, command, bytes, ecu, fast)
        {
        }
    }
}