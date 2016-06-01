using System.ComponentModel;

namespace Obd2Net.InfrastructureContracts.Enums
{
    public enum Unit
    {
        [Description("None")] None,
        [Description("Ratio")] Ratio,
        [Description("Count")] Count,
        [Description("%")] Percent,
        [Description("RPM")] Rpm,
        [Description("Volt")] Volt,
        [Description("F")] F,
        [Description("C")] C,
        [Description("Second")] Sec,
        [Description("Minute")] Min,
        [Description("Pa")] Pa,
        [Description("kPa")] Kpa,
        [Description("psi")] Psi,
        [Description("kph")] Kph,
        [Description("mph")] Mph,
        [Description("Degrees")] Degrees,
        [Description("Grams per Second")] Gps,
        [Description("mA")] Ma,
        [Description("km")] Km,
        [Description("Liters per Hour")] Lph
    }
}