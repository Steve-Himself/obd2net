using System.ComponentModel;

namespace Obd2Net
{
    public enum FuelStatus
    {
        None = 0,
        [Description("Open loop due to insufficient engine temperature")] OpenLoopDueToInsufficientEngineTemperature = 1,
        [Description("Closed loop, using oxygen sensor feedback to determine fuel mix")] ClosedLoopUsingOxygenSensorFeedbackToDetermineFuelMix = 2,
        [Description("Open loop due to engine load OR fuel cut due to deceleration")] OpenLoopDueToEngineLoadOrFuelCutDueToDeceleration = 4,
        [Description("Open loop due to system failure")] OpenLoopDueToSystemFailure = 8,
        [Description("Closed loop, using at least one oxygen sensor but there is a fault in the feedback system")] ClosedLoopUsingAtLeastOneOxygenSensorButThereIsFaultInFeedbackSystem = 16
    }
}