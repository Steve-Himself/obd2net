using Obd2Net.InfrastructureContracts;

namespace Obd2Net.Protocols.Can
{
    // ReSharper disable once InconsistentNaming
    public class ISO_15765_4_11bit_250k : CanProtocol
    {
        public ISO_15765_4_11bit_250k(IMessage[] messages)
            : base(messages, 11)
        {
        }

        public override string ElmName => "ISO 15765-4 (CAN 11/250)";

        public override string ElmId => "8";
    }
}