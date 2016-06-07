namespace Obd2Net.Protocols.Can
{
    // ReSharper disable once InconsistentNaming
    public class SAE_J1939 : CanProtocol
    {
        public override string ElmName => "SAE J1939 (CAN 29/250)";

        public override string ElmId => "9";

        protected override int IdBits => 29;
    }
}