using Obd2Net.InfrastructureContracts;

namespace Obd2Net.Protocols.Legacy
{
    // ReSharper disable once InconsistentNaming
    public class ISO_14230_4_fast : LegacyProtocol
    {
        public ISO_14230_4_fast(IMessage[] messages)
            : base(messages)
        {
        }

        public override string ElmName => "ISO 14230-4 (KWP FAST)";

        public override string ElmId => "5";
    }
}