using Obd2Net.InfrastructureContracts;

namespace Obd2Net.Protocols.Can
{
    // ReSharper disable once InconsistentNaming
    public class ISO_15765_4_29bit_250k : CanProtocol
    {
        public ISO_15765_4_29bit_250k(IMessage[] messages)
            : base(messages, 29)
        {
        }

        public override string ElmName => "ISO 15765-4 (CAN 29/250)";

        public override string ElmId => "9";
    }
}