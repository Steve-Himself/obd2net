using Obd2Net.InfrastructureContracts;

namespace Obd2Net.Protocols.Can
{
    // ReSharper disable once InconsistentNaming
    public class ISO_15765_4_29bit_500k : CanProtocol
    {
        public ISO_15765_4_29bit_500k(IMessage[] messages)
            : base(messages, 29)
        {
        }

        public override string ElmName => "ISO 15765-4 (CAN 29/500)";

        public override string ElmId => "7";
    }
}