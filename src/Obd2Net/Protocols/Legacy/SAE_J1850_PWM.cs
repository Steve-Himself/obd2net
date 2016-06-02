using Obd2Net.InfrastructureContracts;

namespace Obd2Net.Protocols.Legacy
{
    // ReSharper disable once InconsistentNaming
    public class SAE_J1850_PWM : LegacyProtocol
    {
        public override string ElmName => "SAE J1850 PWM";

        public override string ElmId => "1";
    }
}