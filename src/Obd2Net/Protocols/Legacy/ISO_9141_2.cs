using Obd2Net.InfrastructureContracts;

namespace Obd2Net.Protocols.Legacy
{
    // ReSharper disable once InconsistentNaming
    public class ISO_9141_2 : LegacyProtocol
    {
        public override string ElmName => "ISO 9141-2";

        public override string ElmId => "3";
    }
}