using System.ComponentModel;

// ReSharper disable InconsistentNaming

namespace Obd2Net
{
    public enum FuelType
    {
        [Description("Not Available")]
        NotAvailable = 0,
        [Description("Gasoline")]
        Gasoline = 1,
        [Description("Methanol")]
        Methanol = 2,
        [Description("Ethanol")]
        Ethanol = 3,
        [Description("Diesel")]
        Diesel = 4,
        [Description("LPG")]
        LPG = 5,
        [Description("CNG")]
        CNG = 6,
        [Description("Propane")]
        Propane = 7,
        [Description("Electric")]
        Electric = 8,
        [Description("Bifuel Running Gasoline")]
        BifuelRunningGasoline = 9,
        [Description("Bifuel Running Methanol")]
        BifuelRunningMethanol = 10,
        [Description("Bifuel Running Ethanol")]
        BifuelRunningEthanol = 11,
        [Description("Bifuel Running LPG")]
        BifuelRunningLPG = 12,
        [Description("Bifuel Running CNG")]
        BifuelRunningCNG = 13,
        [Description("BifuelRunning Propane")]
        BifuelRunningPropane = 14,
        [Description("Bifuel Running Electricity")]
        BifuelRunningElectricity = 15,
        [Description("Bifuel RunningElectric and Combustion Engine")]
        BifuelRunningElectricAndCombustionEngine = 16,
        [Description("Hybrid Gasoline")]
        HybridGasoline = 17,
        [Description("Hybrid Ethanol")]
        HybridEthanol = 18,
        [Description("Hybrid Diesel")]
        HybridDiesel = 19,
        [Description("Hybrid Electric")]
        HybridElectric = 20,
        [Description("Hybrid Running Electric and Combustion Engine")]
        HybridRunningElectricAndCombustionEngine = 21,
        [Description("Hybrid Regenerative")]
        HybridRegenerative = 22,
        [Description("Bifuel Running Diesel")]
        BifuelRunningDiesel = 23
    }
}