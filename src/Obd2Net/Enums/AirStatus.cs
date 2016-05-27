using System.ComponentModel;

namespace Obd2Net
{
    public enum AirStatus
    {
        None = 0,
        [Description("Upstream")]
        Upstream = 1,
        [Description("Downstream of catalytic converter")]
        DownstreamOfCatalyticConverter = 2,
        [Description("From the outside atmosphere or off")]
        FromOutsideAtmosphereOrOff = 4,
        [Description("Pump commanded on for diagnostics")]
        PumpCommandedOnForDiagnostics = 8
    };
}
