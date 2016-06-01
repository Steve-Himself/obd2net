using System.ComponentModel;

namespace Obd2Net.InfrastructureContracts.Enums
{
    public enum OBDStatus
    {
        [Description("Not Connected")]
        NotConnected,
        [Description("ELM Connected")]
        ElmConnected,
        [Description("Car Connected")]
        CarConnected
    }
}