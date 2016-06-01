using Obd2Net.InfrastructureContracts;

namespace Obd2Net.Protocols.Can
{
    // ReSharper disable once InconsistentNaming
    public class SAE_J1939 : CanProtocol
    {
        public SAE_J1939(IMessage[] messages)
            : base(messages, 29)
        {
        }

        public override string ElmName => "SAE J1939 (CAN 29/250)";

        public override string ElmId => "9";
    }
}