using Obd2Net.InfrastructureContracts;

namespace Obd2Net.Protocols.Legacy
{
    // ReSharper disable once InconsistentNaming
    public class ISO_14230_4_5baud : LegacyProtocol
    {
        public override string ElmName => "ISO 14230-4 (KWP 5BAUD)";

        public override string ElmId => "4";
    }
}