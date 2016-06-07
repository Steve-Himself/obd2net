using System.ComponentModel;

// ReSharper disable InconsistentNaming

namespace Obd2Net
{
    public enum CompressionTest
    {
        [Description("EGR and/or VVT System")] ERGSystem = 0,
        [Description("PM filter monitoring")] PMFilterMonitoring = 1,
        [Description("Exhaust Gas Sensor")] ExhaustGasSensor = 2,
        [Description("Boost Pressure")] BoostPressure = 4,
        [Description("NOx/SCR Monitor")] NOxSCRMonitor = 6,
        [Description("NMHC Catalyst")] NMHCCatalyst = 7
    }
}