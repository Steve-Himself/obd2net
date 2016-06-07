using System.ComponentModel;

// ReSharper disable InconsistentNaming

namespace Obd2Net
{
    public enum SparkTests
    {
        [Description("EGR System")]
        EGRSystem = 0,
        [Description("Oxygen Sensor Heater")]
        OxygenSensorHeater = 1,
        [Description("Oxygen Sensor")]
        OxygenSensor = 2,
        [Description("A/C Refrigerant")]
        ACRefrigerant = 3,
        [Description("Secondary Air System")]
        SecondaryAirSystem = 4,
        [Description("Evaporative System")]
        EvaporativeSystem = 5,
        [Description("Heated Catalyst")]
        HeatedCatalyst = 6,
        [Description("Catalyst")]
        Catalyst = 7
    }
}