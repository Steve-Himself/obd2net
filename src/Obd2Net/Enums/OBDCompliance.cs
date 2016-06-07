using System.ComponentModel;

// ReSharper disable InconsistentNaming

namespace Obd2Net
{
    public enum OBDCompliance
    {
        [Description("Undefined")] Undefined = 0,
        [Description("OBD-II as defined by the CARB")] OBD2_Carb = 1,
        [Description("OBD as defined by the EPA")] OBD_EPA = 2,
        [Description("OBD and OBD-II")] OBD_OBD2 = 3,
        [Description("OBD-I")] OBD1 = 4,
        [Description("Not OBD compliant")] NotOBDCompliant = 5,
        [Description("EOBD (Europe)")] EOBD_EU = 6,
        [Description("EOBD and OBD-II")] EOBD_OBD1 = 7,
        [Description("EOBD and OBD")] EOBD_OBD = 8,
        [Description("EOBD, OBD and OBD II")] EOBD_OBD_OBD2 = 9,
        [Description("JOBD (Japan)")] JOBD = 10,
        [Description("JOBD and OBD II")] JOBD_OBD2 = 11,
        [Description("JOBD and EOBD")] JOBD_EOBD = 12,
        [Description("JOBD, EOBD, and OBD II")] JOBD_EOBD_OBD2 = 13,
        [Description("Engine Manufacturer Diagnostics (EMD)")] EMD = 17,
        [Description("Engine Manufacturer Diagnostics Enhanced (EMD+)")] EMDPlus = 18,
        [Description("Heavy Duty On-Board Diagnostics (Child/Partial) (HD OBD-C)")] HD_OBD_C = 19,
        [Description("Heavy Duty On-Board Diagnostics (HD OBD)")] HD_OBD = 20,
        [Description("World Wide Harmonized OBD (WWH OBD)")] WWH_OBD = 21,
        [Description("Heavy Duty Euro OBD Stage I without NOx control (HD EOBD-I)")] HD_EOBD1 = 23,
        [Description("Heavy Duty Euro OBD Stage I with NOx control (HD EOBD-I N)")] HD_EOBD1_N = 24,
        [Description("Heavy Duty Euro OBD Stage II without NOx control (HD EOBD-II)")] HD_EOBD2 = 25,
        [Description("Heavy Duty Euro OBD Stage II with NOx control (HD EOBD-II N)")] HD_EOBD2_N = 26,
        [Description("Brazil OBD Phase 1 (OBDBr-1)")] OBDBr_1 = 28,
        [Description("Brazil OBD Phase 2 (OBDBr-2)")] OBDBr_2 = 29,
        [Description("Korean OBD (KOBD)")] KOBD = 30,
        [Description("India OBD I (IOBD I)")] IOBD1 = 31,
        [Description("India OBD II (IOBD II)")] IOBD2 = 32,
        [Description("Heavy Duty Euro OBD Stage VI (HD EOBD-IV)")] HD_EOBD_IV = 33
    }
}