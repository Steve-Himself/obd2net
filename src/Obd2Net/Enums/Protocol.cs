using Obd2Net.Attributes;

namespace Obd2Net
{
    // ReSharper disable InconsistentNaming
    public enum Protocol
    {
        Auto = 0,
        [Order(3)]
        SAE_J1850_PWM = 1,
        [Order(6)]
        SAE_J1850_VPW = 2,
        [Order(7)]
        ISO_9141_2 = 3,
        [Order(8)]
        ISO_14230_4_5baud = 4,
        [Order(9)]
        ISO_14230_4_fast = 5,
        [Order(1)]
        ISO_15765_4_11bit_500k = 6,
        [Order(4)]
        ISO_15765_4_29bit_500k = 7,
        [Order(2)]
        ISO_15765_4_11bit_250k = 8,
        [Order(5)]
        ISO_15765_4_29bit_250k = 9,
        [Order(10)]
        SAE_J1939 = 10,
    }
    // ReSharper restore InconsistentNaming
}
