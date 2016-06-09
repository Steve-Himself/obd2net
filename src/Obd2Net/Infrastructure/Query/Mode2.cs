using System.Collections.Generic;
using Obd2Net.InfrastructureContracts.Commands;
using Obd2Net.InfrastructureContracts.Enums;

namespace Obd2Net.Infrastructure.Query
{
    public class Mode2
    {
        public Mode2()
        {
            Commands = new List<IOBDCommand>
            {
                Dtc_PidsA,
                Dtc_Status,
                Dtc_FreezeDtc,
                Dtc_FuelStatus,
                Dtc_EngineLoad,
                Dtc_CoolantTemp,
                Dtc_ShortFuelTrim1,
                Dtc_LongFuelTrim1,
                Dtc_ShortFuelTrim2,
                Dtc_LongFuelTrim2,
                Dtc_FuelPressure,
                Dtc_IntakePressure,
                Dtc_Rpm,
                Dtc_Speed,
                Dtc_TimingAdvance,
                Dtc_IntakeTemp,
                Dtc_Maf,
                Dtc_ThrottlePos,
                Dtc_AirStatus,
                Dtc_O2Sensors,
                Dtc_O2B1S1,
                Dtc_O2B1S2,
                Dtc_O2B1S3,
                Dtc_O2B1S4,
                Dtc_O2B2S1,
                Dtc_O2B2S2,
                Dtc_O2B2S3,
                Dtc_O2B2S4,
                Dtc_OBDCompliance,
                Dtc_O2SensorsAlt,
                Dtc_AuxInputStatus,
                Dtc_RunTime,
                Dtc_PidsB,
                Dtc_DistanceWMil,
                Dtc_FuelRailPressureVac,
                Dtc_FuelRailPressureDirect,
                Dtc_O2S1WrVoltage,
                Dtc_O2S2WrVoltage,
                Dtc_O2S3WrVoltage,
                Dtc_O2S4WrVoltage,
                Dtc_O2S5WrVoltage,
                Dtc_O2S6WrVoltage,
                Dtc_O2S7WrVoltage,
                Dtc_O2S8WrVoltage,
                Dtc_CommandedEgr,
                Dtc_EgrError,
                Dtc_EvaporativePurge,
                Dtc_FuelLevel,
                Dtc_WarmupsSinceDtcClear,
                Dtc_DistanceSinceDtcClear,
                Dtc_EvapVaporPressure,
                Dtc_BarometricPressure,
                Dtc_O2S1WrCurrent,
                Dtc_O2S2WrCurrent,
                Dtc_O2S3WrCurrent,
                Dtc_O2S4WrCurrent,
                Dtc_O2S5WrCurrent,
                Dtc_O2S6WrCurrent,
                Dtc_O2S7WrCurrent,
                Dtc_O2S8WrCurrent,
                Dtc_CatalystTempB1S1,
                Dtc_CatalystTempB2S1,
                Dtc_CatalystTempB1S2,
                Dtc_CatalystTempB2S2,
                Dtc_PidsC,
                Dtc_StatusDriveCycle,
                Dtc_ControlModuleVoltage,
                Dtc_AbsoluteLoad,
                Dtc_CommandEquivRatio,
                Dtc_RelativeThrottlePos,
                Dtc_AmbiantAirTemp,
                Dtc_ThrottlePosB,
                Dtc_ThrottlePosC,
                Dtc_AcceleratorPosD,
                Dtc_AcceleratorPosE,
                Dtc_AcceleratorPosF,
                Dtc_ThrottleActuator,
                Dtc_RunTimeMil,
                Dtc_TimeSinceDtcCleared,
                Dtc_MaxValues,
                Dtc_MaxMaf,
                Dtc_FuelType,
                Dtc_EthanolPercent,
                Dtc_EvapVaporPressureAbs,
                Dtc_EvapVaporPressureAlt,
                Dtc_ShortO2TrimB1,
                Dtc_LongO2TrimB1,
                Dtc_ShortO2TrimB2,
                Dtc_LongO2TrimB2,
                Dtc_FuelRailPressureAbs,
                Dtc_RelativeAccelPos,
                Dtc_HybridBatteryRemaining,
                Dtc_OilTemp,
                Dtc_FuelInjectTiming,
                Dtc_FuelRate,
                Dtc_EmissionReq
            };
        }

        public IEnumerable<IOBDCommand> Commands { get; private set; }
        public IOBDCommand<string> Dtc_PidsA { get; } = new PidCommand("DTC_PIDS_A", "Supported PIDs [01-20]", "0200", 4, ECU.Engine, true);
        public IOBDCommand<string> Dtc_Status { get; } = new OBDCommand<string>("DTC_STATUS", "Status since DTCs cleared", "0201", 4, OldDecoders.Status, ECU.Engine, true);
        public IOBDCommand<string> Dtc_FreezeDtc { get; } = new OBDCommand<string>("DTC_FREEZE_DTC", "Freeze DTC", "0202", 2, OldDecoders.Drop, ECU.Engine, true);
        public IOBDCommand<string> Dtc_FuelStatus { get; } = new OBDCommand<string>("DTC_FUEL_STATUS", "Fuel System Status", "0203", 2, OldDecoders.Fuel_status, ECU.Engine, true);
        public IOBDCommand<decimal> Dtc_EngineLoad { get; } = new OBDCommand<decimal>("DTC_ENGINE_LOAD", "Calculated Engine Load", "0204", 1, OldDecoders.Percent, ECU.Engine, true);
        public IOBDCommand<int> Dtc_CoolantTemp { get; } = new OBDCommand<int>("DTC_COOLANT_TEMP", "Engine Coolant Temperature", "0205", 1, OldDecoders.Temp, ECU.Engine, true);
        public IOBDCommand<decimal> Dtc_ShortFuelTrim1 { get; } = new OBDCommand<decimal>("DTC_SHORT_FUEL_TRIM_1", "Short Term Fuel Trim - Bank 1", "0206", 1, OldDecoders.Percent_centered, ECU.Engine, true);
        public IOBDCommand<decimal> Dtc_LongFuelTrim1 { get; } = new OBDCommand<decimal>("DTC_LONG_FUEL_TRIM_1", "Long Term Fuel Trim - Bank 1", "0207", 1, OldDecoders.Percent_centered, ECU.Engine, true);
        public IOBDCommand<decimal> Dtc_ShortFuelTrim2 { get; } = new OBDCommand<decimal>("DTC_SHORT_FUEL_TRIM_2", "Short Term Fuel Trim - Bank 2", "0208", 1, OldDecoders.Percent_centered, ECU.Engine, true);
        public IOBDCommand<decimal> Dtc_LongFuelTrim2 { get; } = new OBDCommand<decimal>("DTC_LONG_FUEL_TRIM_2", "Long Term Fuel Trim - Bank 2", "0209", 1, OldDecoders.Percent_centered, ECU.Engine, true);
        public IOBDCommand<uint> Dtc_FuelPressure { get; } = new OBDCommand<uint>("DTC_FUEL_PRESSURE", "Fuel Pressure", "020A", 1, OldDecoders.Fuel_pressure, ECU.Engine, true);
        public IOBDCommand<uint> Dtc_IntakePressure { get; } = new OBDCommand<uint>("DTC_INTAKE_PRESSURE", "Intake Manifold Pressure", "020B", 1, OldDecoders.Pressure, ECU.Engine, true);
        public IOBDCommand<decimal> Dtc_Rpm { get; } = new OBDCommand<decimal>("DTC_RPM", "Engine RPM", "020C", 2, OldDecoders.Rpm, ECU.Engine, true);
        public IOBDCommand<uint> Dtc_Speed { get; } = new OBDCommand<uint>("DTC_SPEED", "Vehicle Speed", "020D", 1, OldDecoders.Speed, ECU.Engine, true);
        public IOBDCommand<decimal> Dtc_TimingAdvance { get; } = new OBDCommand<decimal>("DTC_TIMING_ADVANCE", "Timing Advance", "020E", 1, OldDecoders.Timing_advance, ECU.Engine, true);
        public IOBDCommand<int> Dtc_IntakeTemp { get; } = new OBDCommand<int>("DTC_INTAKE_TEMP", "Intake Air Temp", "020F", 1, OldDecoders.Temp, ECU.Engine, true);
        public IOBDCommand<decimal> Dtc_Maf { get; } = new OBDCommand<decimal>("DTC_MAF", "Air Flow Rate (MAF)", "0210", 2, OldDecoders.Maf, ECU.Engine, true);
        public IOBDCommand<decimal> Dtc_ThrottlePos { get; } = new OBDCommand<decimal>("DTC_THROTTLE_POS", "Throttle Position", "0211", 1, OldDecoders.Percent, ECU.Engine, true);
        public IOBDCommand<string> Dtc_AirStatus { get; } = new OBDCommand<string>("DTC_AIR_STATUS", "Secondary Air Status", "0212", 1, OldDecoders.Air_status, ECU.Engine, true);
        public IOBDCommand<string> Dtc_O2Sensors { get; } = new OBDCommand<string>("DTC_O2_SENSORS", "O2 Sensors Present", "0213", 1, OldDecoders.Drop, ECU.Engine, true);
        public IOBDCommand<decimal> Dtc_O2B1S1 { get; } = new OBDCommand<decimal>("DTC_O2_B1S1", "O2: Bank 1 - Sensor 1 Voltage", "0214", 2, OldDecoders.Sensor_voltage, ECU.Engine, true);
        public IOBDCommand<decimal> Dtc_O2B1S2 { get; } = new OBDCommand<decimal>("DTC_O2_B1S2", "O2: Bank 1 - Sensor 2 Voltage", "0215", 2, OldDecoders.Sensor_voltage, ECU.Engine, true);
        public IOBDCommand<decimal> Dtc_O2B1S3 { get; } = new OBDCommand<decimal>("DTC_O2_B1S3", "O2: Bank 1 - Sensor 3 Voltage", "0216", 2, OldDecoders.Sensor_voltage, ECU.Engine, true);
        public IOBDCommand<decimal> Dtc_O2B1S4 { get; } = new OBDCommand<decimal>("DTC_O2_B1S4", "O2: Bank 1 - Sensor 4 Voltage", "0217", 2, OldDecoders.Sensor_voltage, ECU.Engine, true);
        public IOBDCommand<decimal> Dtc_O2B2S1 { get; } = new OBDCommand<decimal>("DTC_O2_B2S1", "O2: Bank 2 - Sensor 1 Voltage", "0218", 2, OldDecoders.Sensor_voltage, ECU.Engine, true);
        public IOBDCommand<decimal> Dtc_O2B2S2 { get; } = new OBDCommand<decimal>("DTC_O2_B2S2", "O2: Bank 2 - Sensor 2 Voltage", "0219", 2, OldDecoders.Sensor_voltage, ECU.Engine, true);
        public IOBDCommand<decimal> Dtc_O2B2S3 { get; } = new OBDCommand<decimal>("DTC_O2_B2S3", "O2: Bank 2 - Sensor 3 Voltage", "021A", 2, OldDecoders.Sensor_voltage, ECU.Engine, true);
        public IOBDCommand<decimal> Dtc_O2B2S4 { get; } = new OBDCommand<decimal>("DTC_O2_B2S4", "O2: Bank 2 - Sensor 4 Voltage", "021B", 2, OldDecoders.Sensor_voltage, ECU.Engine, true);
        public IOBDCommand<string> Dtc_OBDCompliance { get; } = new OBDCommand<string>("DTC_OBD_COMPLIANCE", "OBD Standards Compliance", "021C", 1, OldDecoders.Obd_compliance, ECU.Engine, true);
        public IOBDCommand<string> Dtc_O2SensorsAlt { get; } = new OBDCommand<string>("DTC_O2_SENSORS_ALT", "O2 Sensors Present (alternate)", "021D", 1, OldDecoders.Drop, ECU.Engine, true);
        public IOBDCommand<string> Dtc_AuxInputStatus { get; } = new OBDCommand<string>("DTC_AUX_INPUT_STATUS", "Auxiliary input status", "021E", 1, OldDecoders.Drop, ECU.Engine, true);
        public IOBDCommand<uint> Dtc_RunTime { get; } = new OBDCommand<uint>("DTC_RUN_TIME", "Engine Run Time", "021F", 2, OldDecoders.Seconds, ECU.Engine, true);
        public IOBDCommand<string> Dtc_PidsB { get; } = new PidCommand("DTC_PIDS_B", "Supported PIDs [21-40]", "0220", 4, ECU.Engine, true);
        public IOBDCommand<uint> Dtc_DistanceWMil { get; } = new OBDCommand<uint>("DTC_DISTANCE_W_MIL", "Distance Traveled with MIL on", "0221", 2, OldDecoders.Distance, ECU.Engine, true);
        public IOBDCommand<decimal> Dtc_FuelRailPressureVac { get; } = new OBDCommand<decimal>("DTC_FUEL_RAIL_PRESSURE_VAC", "Fuel Rail Pressure (relative to vacuum)", "0222", 2, OldDecoders.Fuel_pres_vac, ECU.Engine, true);
        public IOBDCommand<int> Dtc_FuelRailPressureDirect { get; } = new OBDCommand<int>("DTC_FUEL_RAIL_PRESSURE_DIRECT", "Fuel Rail Pressure (direct inject)", "0223", 2, OldDecoders.Fuel_pres_direct, ECU.Engine, true);
        public IOBDCommand<decimal> Dtc_O2S1WrVoltage { get; } = new OBDCommand<decimal>("DTC_O2_S1_WR_VOLTAGE", "02 Sensor 1 WR Lambda Voltage", "0224", 4, OldDecoders.Sensor_voltage_big, ECU.Engine, true);
        public IOBDCommand<decimal> Dtc_O2S2WrVoltage { get; } = new OBDCommand<decimal>("DTC_O2_S2_WR_VOLTAGE", "02 Sensor 2 WR Lambda Voltage", "0225", 4, OldDecoders.Sensor_voltage_big, ECU.Engine, true);
        public IOBDCommand<decimal> Dtc_O2S3WrVoltage { get; } = new OBDCommand<decimal>("DTC_O2_S3_WR_VOLTAGE", "02 Sensor 3 WR Lambda Voltage", "0226", 4, OldDecoders.Sensor_voltage_big, ECU.Engine, true);
        public IOBDCommand<decimal> Dtc_O2S4WrVoltage { get; } = new OBDCommand<decimal>("DTC_O2_S4_WR_VOLTAGE", "02 Sensor 4 WR Lambda Voltage", "0227", 4, OldDecoders.Sensor_voltage_big, ECU.Engine, true);
        public IOBDCommand<decimal> Dtc_O2S5WrVoltage { get; } = new OBDCommand<decimal>("DTC_O2_S5_WR_VOLTAGE", "02 Sensor 5 WR Lambda Voltage", "0228", 4, OldDecoders.Sensor_voltage_big, ECU.Engine, true);
        public IOBDCommand<decimal> Dtc_O2S6WrVoltage { get; } = new OBDCommand<decimal>("DTC_O2_S6_WR_VOLTAGE", "02 Sensor 6 WR Lambda Voltage", "0229", 4, OldDecoders.Sensor_voltage_big, ECU.Engine, true);
        public IOBDCommand<decimal> Dtc_O2S7WrVoltage { get; } = new OBDCommand<decimal>("DTC_O2_S7_WR_VOLTAGE", "02 Sensor 7 WR Lambda Voltage", "022A", 4, OldDecoders.Sensor_voltage_big, ECU.Engine, true);
        public IOBDCommand<decimal> Dtc_O2S8WrVoltage { get; } = new OBDCommand<decimal>("DTC_O2_S8_WR_VOLTAGE", "02 Sensor 8 WR Lambda Voltage", "022B", 4, OldDecoders.Sensor_voltage_big, ECU.Engine, true);
        public IOBDCommand<decimal> Dtc_CommandedEgr { get; } = new OBDCommand<decimal>("DTC_COMMANDED_EGR", "Commanded EGR", "022C", 1, OldDecoders.Percent, ECU.Engine, true);
        public IOBDCommand<decimal> Dtc_EgrError { get; } = new OBDCommand<decimal>("DTC_EGR_ERROR", "EGR Error", "022D", 1, OldDecoders.Percent_centered, ECU.Engine, true);
        public IOBDCommand<decimal> Dtc_EvaporativePurge { get; } = new OBDCommand<decimal>("DTC_EVAPORATIVE_PURGE", "Commanded Evaporative Purge", "022E", 1, OldDecoders.Percent, ECU.Engine, true);
        public IOBDCommand<decimal> Dtc_FuelLevel { get; } = new OBDCommand<decimal>("DTC_FUEL_LEVEL", "Fuel Level Input", "022F", 1, OldDecoders.Percent, ECU.Engine, true);
        public IOBDCommand<int> Dtc_WarmupsSinceDtcClear { get; } = new OBDCommand<int>("DTC_WARMUPS_SINCE_DTC_CLEAR", "Number of warm-ups since codes cleared", "0230", 1, OldDecoders.Count, ECU.Engine, true);
        public IOBDCommand<uint> Dtc_DistanceSinceDtcClear { get; } = new OBDCommand<uint>("DTC_DISTANCE_SINCE_DTC_CLEAR", "Distance traveled since codes cleared", "0231", 2, OldDecoders.Distance, ECU.Engine, true);
        public IOBDCommand<decimal> Dtc_EvapVaporPressure { get; } = new OBDCommand<decimal>("DTC_EVAP_VAPOR_PRESSURE", "Evaporative system vapor pressure", "0232", 2, OldDecoders.Evap_pressure, ECU.Engine, true);
        public IOBDCommand<uint> Dtc_BarometricPressure { get; } = new OBDCommand<uint>("DTC_BAROMETRIC_PRESSURE", "Barometric Pressure", "0233", 1, OldDecoders.Pressure, ECU.Engine, true);
        public IOBDCommand<decimal> Dtc_O2S1WrCurrent { get; } = new OBDCommand<decimal>("DTC_O2_S1_WR_CURRENT", "02 Sensor 1 WR Lambda Current", "0234", 4, OldDecoders.Current_centered, ECU.Engine, true);
        public IOBDCommand<decimal> Dtc_O2S2WrCurrent { get; } = new OBDCommand<decimal>("DTC_O2_S2_WR_CURRENT", "02 Sensor 2 WR Lambda Current", "0235", 4, OldDecoders.Current_centered, ECU.Engine, true);
        public IOBDCommand<decimal> Dtc_O2S3WrCurrent { get; } = new OBDCommand<decimal>("DTC_O2_S3_WR_CURRENT", "02 Sensor 3 WR Lambda Current", "0236", 4, OldDecoders.Current_centered, ECU.Engine, true);
        public IOBDCommand<decimal> Dtc_O2S4WrCurrent { get; } = new OBDCommand<decimal>("DTC_O2_S4_WR_CURRENT", "02 Sensor 4 WR Lambda Current", "0237", 4, OldDecoders.Current_centered, ECU.Engine, true);
        public IOBDCommand<decimal> Dtc_O2S5WrCurrent { get; } = new OBDCommand<decimal>("DTC_O2_S5_WR_CURRENT", "02 Sensor 5 WR Lambda Current", "0238", 4, OldDecoders.Current_centered, ECU.Engine, true);
        public IOBDCommand<decimal> Dtc_O2S6WrCurrent { get; } = new OBDCommand<decimal>("DTC_O2_S6_WR_CURRENT", "02 Sensor 6 WR Lambda Current", "0239", 4, OldDecoders.Current_centered, ECU.Engine, true);
        public IOBDCommand<decimal> Dtc_O2S7WrCurrent { get; } = new OBDCommand<decimal>("DTC_O2_S7_WR_CURRENT", "02 Sensor 7 WR Lambda Current", "023A", 4, OldDecoders.Current_centered, ECU.Engine, true);
        public IOBDCommand<decimal> Dtc_O2S8WrCurrent { get; } = new OBDCommand<decimal>("DTC_O2_S8_WR_CURRENT", "02 Sensor 8 WR Lambda Current", "023B", 4, OldDecoders.Current_centered, ECU.Engine, true);
        public IOBDCommand<decimal> Dtc_CatalystTempB1S1 { get; } = new OBDCommand<decimal>("DTC_CATALYST_TEMP_B1S1", "Catalyst Temperature: Bank 1 - Sensor 1", "023C", 2, OldDecoders.Catalyst_temp, ECU.Engine, true);
        public IOBDCommand<decimal> Dtc_CatalystTempB2S1 { get; } = new OBDCommand<decimal>("DTC_CATALYST_TEMP_B2S1", "Catalyst Temperature: Bank 2 - Sensor 1", "023D", 2, OldDecoders.Catalyst_temp, ECU.Engine, true);
        public IOBDCommand<decimal> Dtc_CatalystTempB1S2 { get; } = new OBDCommand<decimal>("DTC_CATALYST_TEMP_B1S2", "Catalyst Temperature: Bank 1 - Sensor 2", "023E", 2, OldDecoders.Catalyst_temp, ECU.Engine, true);
        public IOBDCommand<decimal> Dtc_CatalystTempB2S2 { get; } = new OBDCommand<decimal>("DTC_CATALYST_TEMP_B2S2", "Catalyst Temperature: Bank 2 - Sensor 2", "023F", 2, OldDecoders.Catalyst_temp, ECU.Engine, true);
        public IOBDCommand<string> Dtc_PidsC { get; } = new PidCommand("DTC_PIDS_C", "Supported PIDs [41-60]", "0240", 4, ECU.Engine, true);
        public IOBDCommand<string> Dtc_StatusDriveCycle { get; } = new OBDCommand<string>("DTC_STATUS_DRIVE_CYCLE", "Monitor status this drive cycle", "0241", 4, OldDecoders.Drop, ECU.Engine, true);
        public IOBDCommand<string> Dtc_ControlModuleVoltage { get; } = new OBDCommand<string>("DTC_CONTROL_MODULE_VOLTAGE", "Control module voltage", "0242", 2, OldDecoders.Drop, ECU.Engine, true);
        public IOBDCommand<string> Dtc_AbsoluteLoad { get; } = new OBDCommand<string>("DTC_ABSOLUTE_LOAD", "Absolute load value", "0243", 2, OldDecoders.Drop, ECU.Engine, true);
        public IOBDCommand<string> Dtc_CommandEquivRatio { get; } = new OBDCommand<string>("DTC_COMMAND_EQUIV_RATIO", "Command equivalence ratio", "0244", 2, OldDecoders.Drop, ECU.Engine, true);
        public IOBDCommand<decimal> Dtc_RelativeThrottlePos { get; } = new OBDCommand<decimal>("DTC_RELATIVE_THROTTLE_POS", "Relative throttle position", "0245", 1, OldDecoders.Percent, ECU.Engine, true);
        public IOBDCommand<int> Dtc_AmbiantAirTemp { get; } = new OBDCommand<int>("DTC_AMBIANT_AIR_TEMP", "Ambient air temperature", "0246", 1, OldDecoders.Temp, ECU.Engine, true);
        public IOBDCommand<decimal> Dtc_ThrottlePosB { get; } = new OBDCommand<decimal>("DTC_THROTTLE_POS_B", "Absolute throttle position B", "0247", 1, OldDecoders.Percent, ECU.Engine, true);
        public IOBDCommand<decimal> Dtc_ThrottlePosC { get; } = new OBDCommand<decimal>("DTC_THROTTLE_POS_C", "Absolute throttle position C", "0248", 1, OldDecoders.Percent, ECU.Engine, true);
        public IOBDCommand<decimal> Dtc_AcceleratorPosD { get; } = new OBDCommand<decimal>("DTC_ACCELERATOR_POS_D", "Accelerator pedal position D", "0249", 1, OldDecoders.Percent, ECU.Engine, true);
        public IOBDCommand<decimal> Dtc_AcceleratorPosE { get; } = new OBDCommand<decimal>("DTC_ACCELERATOR_POS_E", "Accelerator pedal position E", "024A", 1, OldDecoders.Percent, ECU.Engine, true);
        public IOBDCommand<decimal> Dtc_AcceleratorPosF { get; } = new OBDCommand<decimal>("DTC_ACCELERATOR_POS_F", "Accelerator pedal position F", "024B", 1, OldDecoders.Percent, ECU.Engine, true);
        public IOBDCommand<decimal> Dtc_ThrottleActuator { get; } = new OBDCommand<decimal>("DTC_THROTTLE_ACTUATOR", "Commanded throttle actuator", "024C", 1, OldDecoders.Percent, ECU.Engine, true);
        public IOBDCommand<uint> Dtc_RunTimeMil { get; } = new OBDCommand<uint>("DTC_RUN_TIME_MIL", "Time run with MIL on", "024D", 2, OldDecoders.Minutes, ECU.Engine, true);
        public IOBDCommand<uint> Dtc_TimeSinceDtcCleared { get; } = new OBDCommand<uint>("DTC_TIME_SINCE_DTC_CLEARED", "Time since trouble codes cleared", "024E", 2, OldDecoders.Minutes, ECU.Engine, true);
        public IOBDCommand<string> Dtc_MaxValues { get; } = new OBDCommand<string>("DTC_MAX_VALUES", "Various Max values", "024F", 4, OldDecoders.Drop, ECU.Engine, true);
        public IOBDCommand<uint> Dtc_MaxMaf { get; } = new OBDCommand<uint>("DTC_MAX_MAF", "Maximum value for mass air flow sensor", "0250", 4, OldDecoders.Max_maf, ECU.Engine, true);
        public IOBDCommand<string> Dtc_FuelType { get; } = new OBDCommand<string>("DTC_FUEL_TYPE", "Fuel Type", "0251", 1, OldDecoders.Fuel_type, ECU.Engine, true);
        public IOBDCommand<decimal> Dtc_EthanolPercent { get; } = new OBDCommand<decimal>("DTC_ETHANOL_PERCENT", "Ethanol Fuel Percent", "0252", 1, OldDecoders.Percent, ECU.Engine, true);
        public IOBDCommand<decimal> Dtc_EvapVaporPressureAbs { get; } = new OBDCommand<decimal>("DTC_EVAP_VAPOR_PRESSURE_ABS", "Absolute Evap system Vapor Pressure", "0253", 2, OldDecoders.Abs_evap_pressure, ECU.Engine, true);
        public IOBDCommand<int> Dtc_EvapVaporPressureAlt { get; } = new OBDCommand<int>("DTC_EVAP_VAPOR_PRESSURE_ALT", "Evap system vapor pressure", "0254", 2, OldDecoders.Evap_pressure_alt, ECU.Engine, true);
        public IOBDCommand<decimal> Dtc_ShortO2TrimB1 { get; } = new OBDCommand<decimal>("DTC_SHORT_O2_TRIM_B1", "Short term secondary O2 trim - Bank 1", "0255", 2, OldDecoders.Percent_centered, ECU.Engine, true);
        public IOBDCommand<decimal> Dtc_LongO2TrimB1 { get; } = new OBDCommand<decimal>("DTC_LONG_O2_TRIM_B1", "Long term secondary O2 trim - Bank 1", "0256", 2, OldDecoders.Percent_centered, ECU.Engine, true);
        public IOBDCommand<decimal> Dtc_ShortO2TrimB2 { get; } = new OBDCommand<decimal>("DTC_SHORT_O2_TRIM_B2", "Short term secondary O2 trim - Bank 2", "0257", 2, OldDecoders.Percent_centered, ECU.Engine, true);
        public IOBDCommand<decimal> Dtc_LongO2TrimB2 { get; } = new OBDCommand<decimal>("DTC_LONG_O2_TRIM_B2", "Long term secondary O2 trim - Bank 2", "0258", 2, OldDecoders.Percent_centered, ECU.Engine, true);
        public IOBDCommand<int> Dtc_FuelRailPressureAbs { get; } = new OBDCommand<int>("DTC_FUEL_RAIL_PRESSURE_ABS", "Fuel rail pressure (absolute)", "0259", 2, OldDecoders.Fuel_pres_direct, ECU.Engine, true);
        public IOBDCommand<decimal> Dtc_RelativeAccelPos { get; } = new OBDCommand<decimal>("DTC_RELATIVE_ACCEL_POS", "Relative accelerator pedal position", "025A", 1, OldDecoders.Percent, ECU.Engine, true);
        public IOBDCommand<decimal> Dtc_HybridBatteryRemaining { get; } = new OBDCommand<decimal>("DTC_HYBRID_BATTERY_REMAINING", "Hybrid battery pack remaining life", "025B", 1, OldDecoders.Percent, ECU.Engine, true);
        public IOBDCommand<int> Dtc_OilTemp { get; } = new OBDCommand<int>("DTC_OIL_TEMP", "Engine oil temperature", "025C", 1, OldDecoders.Temp, ECU.Engine, true);
        public IOBDCommand<int> Dtc_FuelInjectTiming { get; } = new OBDCommand<int>("DTC_FUEL_INJECT_TIMING", "Fuel injection timing", "025D", 2, OldDecoders.Inject_timing, ECU.Engine, true);
        public IOBDCommand<decimal> Dtc_FuelRate { get; } = new OBDCommand<decimal>("DTC_FUEL_RATE", "Engine fuel rate", "025E", 2, OldDecoders.Fuel_rate, ECU.Engine, true);
        public IOBDCommand<string> Dtc_EmissionReq { get; } = new OBDCommand<string>("DTC_EMISSION_REQ", "Designed emission requirements", "025F", 1, OldDecoders.Drop, ECU.Engine, true);
    }
}
