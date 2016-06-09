using Obd2Net.Decoders;
using Obd2Net.InfrastructureContracts.Enums;

namespace Obd2Net.Commands.Mode1
{
    internal class EngineLoadCommand : OBDCommandBase<PercentDecoder, decimal>
    {
        public EngineLoadCommand(PercentDecoder decoder) :
            base(decoder, Mode.Mode1, "ENGINE_LOAD", "Calculated Engine Load", "0104", 1, ECU.Engine, true)
        {
        }
    }
}