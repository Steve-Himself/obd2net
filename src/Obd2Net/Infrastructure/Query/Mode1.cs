using System.Collections.Generic;
using Obd2Net.InfrastructureContracts.Commands;
using Obd2Net.InfrastructureContracts.Enums;

namespace Obd2Net.Infrastructure.Query
{
    public class Mode1
    {
        public Mode1()
        {
            Commands = new List<IOBDCommand>
            {
                PidsA,
                Status,
                FreezeDtc,
                FuelStatus,
                EngineLoad,
                CoolantTemp,
                ShortFuelTrim1,
                LongFuelTrim1,
                ShortFuelTrim2,
                LongFuelTrim2,
                FuelPressure,
                IntakePressure,
                Rpm,
                Speed,
                TimingAdvance,
                IntakeTemp,
                Maf,
                ThrottlePos,
                AirStatus,
                O2Sensors,
                O2B1S1,
                O2B1S2,
                O2B1S3,
                O2B1S4,
                O2B2S1,
                O2B2S2,
                O2B2S3,
                O2B2S4,
                OBDCompliance,
                O2SensorsAlt,
                AuxInputStatus,
                RunTime,
                PidsB,
                DistanceWMil,
                FuelRailPressureVac,
                FuelRailPressureDirect,
                O2S1WrVoltage,
                O2S2WrVoltage,
                O2S3WrVoltage,
                O2S4WrVoltage,
                O2S5WrVoltage,
                O2S6WrVoltage,
                O2S7WrVoltage,
                O2S8WrVoltage,
                CommandedEgr,
                EgrError,
                EvaporativePurge,
                FuelLevel,
                WarmupsSinceDtcClear,
                DistanceSinceDtcClear,
                EvapVaporPressure,
                BarometricPressure,
                O2S1WrCurrent,
                O2S2WrCurrent,
                O2S3WrCurrent,
                O2S4WrCurrent,
                O2S5WrCurrent,
                O2S6WrCurrent,
                O2S7WrCurrent,
                O2S8WrCurrent,
                CatalystTempB1S1,
                CatalystTempB2S1,
                CatalystTempB1S2,
                CatalystTempB2S2,
                PidsC,
                StatusDriveCycle,
                ControlModuleVoltage,
                AbsoluteLoad,
                CommandEquivRatio,
                RelativeThrottlePos,
                AmbiantAirTemp,
                ThrottlePosB,
                ThrottlePosC,
                AcceleratorPosD,
                AcceleratorPosE,
                AcceleratorPosF,
                ThrottleActuator,
                RunTimeMil,
                TimeSinceDtcCleared,
                MaxValues,
                MaxMaf,
                FuelType,
                EthanolPercent,
                EvapVaporPressureAbs,
                EvapVaporPressureAlt,
                ShortO2TrimB1,
                LongO2TrimB1,
                ShortO2TrimB2,
                LongO2TrimB2,
                FuelRailPressureAbs,
                RelativeAccelPos,
                HybridBatteryRemaining,
                OilTemp,
                FuelInjectTiming,
                FuelRate,
                EmissionReq
            };
        }

        public IEnumerable<IOBDCommand> Commands { get; private set; }
        public IOBDCommand<string> PidsA { get; } = new PidCommand("PIDS_A", "Supported PIDs [01-20]", "0100", 4, ECU.Engine, true);
        public IOBDCommand<string> Status { get; } = new OBDCommand<string>("STATUS", "Status since DTCs cleared", "0101", 4, Decoders.Status, ECU.Engine, true);
        public IOBDCommand<string> FreezeDtc { get; } = new OBDCommand<string>("FREEZE_DTC", "Freeze DTC", "0102", 2, Decoders.Drop, ECU.Engine, true);
        public IOBDCommand<string> FuelStatus { get; } = new OBDCommand<string>("FUEL_STATUS", "Fuel System Status", "0103", 2, Decoders.Fuel_status, ECU.Engine, true);
        public IOBDCommand<decimal> EngineLoad { get; } = new OBDCommand<decimal>("ENGINE_LOAD", "Calculated Engine Load", "0104", 1, Decoders.Percent, ECU.Engine, true);
        public IOBDCommand<int> CoolantTemp { get; } = new OBDCommand<int>("COOLANT_TEMP", "Engine Coolant Temperature", "0105", 1, Decoders.Temp, ECU.Engine, true);
        public IOBDCommand<decimal> ShortFuelTrim1 { get; } = new OBDCommand<decimal>("SHORT_FUEL_TRIM_1", "Short Term Fuel Trim - Bank 1", "0106", 1, Decoders.Percent_centered, ECU.Engine, true);
        public IOBDCommand<decimal> LongFuelTrim1 { get; } = new OBDCommand<decimal>("LONG_FUEL_TRIM_1", "Long Term Fuel Trim - Bank 1", "0107", 1, Decoders.Percent_centered, ECU.Engine, true);
        public IOBDCommand<decimal> ShortFuelTrim2 { get; } = new OBDCommand<decimal>("SHORT_FUEL_TRIM_2", "Short Term Fuel Trim - Bank 2", "0108", 1, Decoders.Percent_centered, ECU.Engine, true);
        public IOBDCommand<decimal> LongFuelTrim2 { get; } = new OBDCommand<decimal>("LONG_FUEL_TRIM_2", "Long Term Fuel Trim - Bank 2", "0109", 1, Decoders.Percent_centered, ECU.Engine, true);
        public IOBDCommand<uint> FuelPressure { get; } = new OBDCommand<uint>("FUEL_PRESSURE", "Fuel Pressure", "010A", 1, Decoders.Fuel_pressure, ECU.Engine, true);
        public IOBDCommand<uint> IntakePressure { get; } = new OBDCommand<uint>("INTAKE_PRESSURE", "Intake Manifold Pressure", "010B", 1, Decoders.Pressure, ECU.Engine, true);
        public IOBDCommand<decimal> Rpm { get; } = new OBDCommand<decimal>("RPM", "Engine RPM", "010C", 2, Decoders.Rpm, ECU.Engine, true);
        public IOBDCommand<uint> Speed { get; } = new OBDCommand<uint>("SPEED", "Vehicle Speed", "010D", 1, Decoders.Speed, ECU.Engine, true);
        public IOBDCommand<decimal> TimingAdvance { get; } = new OBDCommand<decimal>("TIMING_ADVANCE", "Timing Advance", "010E", 1, Decoders.Timing_advance, ECU.Engine, true);
        public IOBDCommand<int> IntakeTemp { get; } = new OBDCommand<int>("INTAKE_TEMP", "Intake Air Temp", "010F", 1, Decoders.Temp, ECU.Engine, true);
        public IOBDCommand<decimal> Maf { get; } = new OBDCommand<decimal>("MAF", "Air Flow Rate (MAF)", "0110", 2, Decoders.Maf, ECU.Engine, true);
        public IOBDCommand<decimal> ThrottlePos { get; } = new OBDCommand<decimal>("THROTTLE_POS", "Throttle Position", "0111", 1, Decoders.Percent, ECU.Engine, true);
        public IOBDCommand<string> AirStatus { get; } = new OBDCommand<string>("AIR_STATUS", "Secondary Air Status", "0112", 1, Decoders.Air_status, ECU.Engine, true);
        public IOBDCommand<string> O2Sensors { get; } = new OBDCommand<string>("O2_SENSORS", "O2 Sensors Present", "0113", 1, Decoders.Drop, ECU.Engine, true);
        public IOBDCommand<decimal> O2B1S1 { get; } = new OBDCommand<decimal>("O2_B1S1", "O2: Bank 1 - Sensor 1 Voltage", "0114", 2, Decoders.Sensor_voltage, ECU.Engine, true);
        public IOBDCommand<decimal> O2B1S2 { get; } = new OBDCommand<decimal>("O2_B1S2", "O2: Bank 1 - Sensor 2 Voltage", "0115", 2, Decoders.Sensor_voltage, ECU.Engine, true);
        public IOBDCommand<decimal> O2B1S3 { get; } = new OBDCommand<decimal>("O2_B1S3", "O2: Bank 1 - Sensor 3 Voltage", "0116", 2, Decoders.Sensor_voltage, ECU.Engine, true);
        public IOBDCommand<decimal> O2B1S4 { get; } = new OBDCommand<decimal>("O2_B1S4", "O2: Bank 1 - Sensor 4 Voltage", "0117", 2, Decoders.Sensor_voltage, ECU.Engine, true);
        public IOBDCommand<decimal> O2B2S1 { get; } = new OBDCommand<decimal>("O2_B2S1", "O2: Bank 2 - Sensor 1 Voltage", "0118", 2, Decoders.Sensor_voltage, ECU.Engine, true);
        public IOBDCommand<decimal> O2B2S2 { get; } = new OBDCommand<decimal>("O2_B2S2", "O2: Bank 2 - Sensor 2 Voltage", "0119", 2, Decoders.Sensor_voltage, ECU.Engine, true);
        public IOBDCommand<decimal> O2B2S3 { get; } = new OBDCommand<decimal>("O2_B2S3", "O2: Bank 2 - Sensor 3 Voltage", "011A", 2, Decoders.Sensor_voltage, ECU.Engine, true);
        public IOBDCommand<decimal> O2B2S4 { get; } = new OBDCommand<decimal>("O2_B2S4", "O2: Bank 2 - Sensor 4 Voltage", "011B", 2, Decoders.Sensor_voltage, ECU.Engine, true);
        public IOBDCommand<string> OBDCompliance { get; } = new OBDCommand<string>("OBD_COMPLIANCE", "OBD Standards Compliance", "011C", 1, Decoders.Obd_compliance, ECU.Engine, true);
        public IOBDCommand<string> O2SensorsAlt { get; } = new OBDCommand<string>("O2_SENSORS_ALT", "O2 Sensors Present (alternate)", "011D", 1, Decoders.Drop, ECU.Engine, true);
        public IOBDCommand<string> AuxInputStatus { get; } = new OBDCommand<string>("AUX_INPUT_STATUS", "Auxiliary input status", "011E", 1, Decoders.Drop, ECU.Engine, true);
        public IOBDCommand<uint> RunTime { get; } = new OBDCommand<uint>("RUN_TIME", "Engine Run Time", "011F", 2, Decoders.Seconds, ECU.Engine, true);
        public IOBDCommand<string> PidsB { get; } = new PidCommand("PIDS_B", "Supported PIDs [21-40]", "0120", 4, ECU.Engine, true);
        public IOBDCommand<uint> DistanceWMil { get; } = new OBDCommand<uint>("DISTANCE_W_MIL", "Distance Traveled with MIL on", "0121", 2, Decoders.Distance, ECU.Engine, true);
        public IOBDCommand<decimal> FuelRailPressureVac { get; } = new OBDCommand<decimal>("FUEL_RAIL_PRESSURE_VAC", "Fuel Rail Pressure (relative to vacuum)", "0122", 2, Decoders.Fuel_pres_vac, ECU.Engine, true);
        public IOBDCommand<int> FuelRailPressureDirect { get; } = new OBDCommand<int>("FUEL_RAIL_PRESSURE_DIRECT", "Fuel Rail Pressure (direct inject)", "0123", 2, Decoders.Fuel_pres_direct, ECU.Engine, true);
        public IOBDCommand<decimal> O2S1WrVoltage { get; } = new OBDCommand<decimal>("O2_S1_WR_VOLTAGE", "02 Sensor 1 WR Lambda Voltage", "0124", 4, Decoders.Sensor_voltage_big, ECU.Engine, true);
        public IOBDCommand<decimal> O2S2WrVoltage { get; } = new OBDCommand<decimal>("O2_S2_WR_VOLTAGE", "02 Sensor 2 WR Lambda Voltage", "0125", 4, Decoders.Sensor_voltage_big, ECU.Engine, true);
        public IOBDCommand<decimal> O2S3WrVoltage { get; } = new OBDCommand<decimal>("O2_S3_WR_VOLTAGE", "02 Sensor 3 WR Lambda Voltage", "0126", 4, Decoders.Sensor_voltage_big, ECU.Engine, true);
        public IOBDCommand<decimal> O2S4WrVoltage { get; } = new OBDCommand<decimal>("O2_S4_WR_VOLTAGE", "02 Sensor 4 WR Lambda Voltage", "0127", 4, Decoders.Sensor_voltage_big, ECU.Engine, true);
        public IOBDCommand<decimal> O2S5WrVoltage { get; } = new OBDCommand<decimal>("O2_S5_WR_VOLTAGE", "02 Sensor 5 WR Lambda Voltage", "0128", 4, Decoders.Sensor_voltage_big, ECU.Engine, true);
        public IOBDCommand<decimal> O2S6WrVoltage { get; } = new OBDCommand<decimal>("O2_S6_WR_VOLTAGE", "02 Sensor 6 WR Lambda Voltage", "0129", 4, Decoders.Sensor_voltage_big, ECU.Engine, true);
        public IOBDCommand<decimal> O2S7WrVoltage { get; } = new OBDCommand<decimal>("O2_S7_WR_VOLTAGE", "02 Sensor 7 WR Lambda Voltage", "012A", 4, Decoders.Sensor_voltage_big, ECU.Engine, true);
        public IOBDCommand<decimal> O2S8WrVoltage { get; } = new OBDCommand<decimal>("O2_S8_WR_VOLTAGE", "02 Sensor 8 WR Lambda Voltage", "012B", 4, Decoders.Sensor_voltage_big, ECU.Engine, true);
        public IOBDCommand<decimal> CommandedEgr { get; } = new OBDCommand<decimal>("COMMANDED_EGR", "Commanded EGR", "012C", 1, Decoders.Percent, ECU.Engine, true);
        public IOBDCommand<decimal> EgrError { get; } = new OBDCommand<decimal>("EGR_ERROR", "EGR Error", "012D", 1, Decoders.Percent_centered, ECU.Engine, true);
        public IOBDCommand<decimal> EvaporativePurge { get; } = new OBDCommand<decimal>("EVAPORATIVE_PURGE", "Commanded Evaporative Purge", "012E", 1, Decoders.Percent, ECU.Engine, true);
        public IOBDCommand<decimal> FuelLevel { get; } = new OBDCommand<decimal>("FUEL_LEVEL", "Fuel Level Input", "012F", 1, Decoders.Percent, ECU.Engine, true);
        public IOBDCommand<int> WarmupsSinceDtcClear { get; } = new OBDCommand<int>("WARMUPS_SINCE_DTC_CLEAR", "Number of warm-ups since codes cleared", "0130", 1, Decoders.Count, ECU.Engine, true);
        public IOBDCommand<uint> DistanceSinceDtcClear { get; } = new OBDCommand<uint>("DISTANCE_SINCE_DTC_CLEAR", "Distance traveled since codes cleared", "0131", 2, Decoders.Distance, ECU.Engine, true);
        public IOBDCommand<decimal> EvapVaporPressure { get; } = new OBDCommand<decimal>("EVAP_VAPOR_PRESSURE", "Evaporative system vapor pressure", "0132", 2, Decoders.Evap_pressure, ECU.Engine, true);
        public IOBDCommand<uint> BarometricPressure { get; } = new OBDCommand<uint>("BAROMETRIC_PRESSURE", "Barometric Pressure", "0133", 1, Decoders.Pressure, ECU.Engine, true);
        public IOBDCommand<decimal> O2S1WrCurrent { get; } = new OBDCommand<decimal>("O2_S1_WR_CURRENT", "02 Sensor 1 WR Lambda Current", "0134", 4, Decoders.Current_centered, ECU.Engine, true);
        public IOBDCommand<decimal> O2S2WrCurrent { get; } = new OBDCommand<decimal>("O2_S2_WR_CURRENT", "02 Sensor 2 WR Lambda Current", "0135", 4, Decoders.Current_centered, ECU.Engine, true);
        public IOBDCommand<decimal> O2S3WrCurrent { get; } = new OBDCommand<decimal>("O2_S3_WR_CURRENT", "02 Sensor 3 WR Lambda Current", "0136", 4, Decoders.Current_centered, ECU.Engine, true);
        public IOBDCommand<decimal> O2S4WrCurrent { get; } = new OBDCommand<decimal>("O2_S4_WR_CURRENT", "02 Sensor 4 WR Lambda Current", "0137", 4, Decoders.Current_centered, ECU.Engine, true);
        public IOBDCommand<decimal> O2S5WrCurrent { get; } = new OBDCommand<decimal>("O2_S5_WR_CURRENT", "02 Sensor 5 WR Lambda Current", "0138", 4, Decoders.Current_centered, ECU.Engine, true);
        public IOBDCommand<decimal> O2S6WrCurrent { get; } = new OBDCommand<decimal>("O2_S6_WR_CURRENT", "02 Sensor 6 WR Lambda Current", "0139", 4, Decoders.Current_centered, ECU.Engine, true);
        public IOBDCommand<decimal> O2S7WrCurrent { get; } = new OBDCommand<decimal>("O2_S7_WR_CURRENT", "02 Sensor 7 WR Lambda Current", "013A", 4, Decoders.Current_centered, ECU.Engine, true);
        public IOBDCommand<decimal> O2S8WrCurrent { get; } = new OBDCommand<decimal>("O2_S8_WR_CURRENT", "02 Sensor 8 WR Lambda Current", "013B", 4, Decoders.Current_centered, ECU.Engine, true);
        public IOBDCommand<decimal> CatalystTempB1S1 { get; } = new OBDCommand<decimal>("CATALYST_TEMP_B1S1", "Catalyst Temperature: Bank 1 - Sensor 1", "013C", 2, Decoders.Catalyst_temp, ECU.Engine, true);
        public IOBDCommand<decimal> CatalystTempB2S1 { get; } = new OBDCommand<decimal>("CATALYST_TEMP_B2S1", "Catalyst Temperature: Bank 2 - Sensor 1", "013D", 2, Decoders.Catalyst_temp, ECU.Engine, true);
        public IOBDCommand<decimal> CatalystTempB1S2 { get; } = new OBDCommand<decimal>("CATALYST_TEMP_B1S2", "Catalyst Temperature: Bank 1 - Sensor 2", "013E", 2, Decoders.Catalyst_temp, ECU.Engine, true);
        public IOBDCommand<decimal> CatalystTempB2S2 { get; } = new OBDCommand<decimal>("CATALYST_TEMP_B2S2", "Catalyst Temperature: Bank 2 - Sensor 2", "013F", 2, Decoders.Catalyst_temp, ECU.Engine, true);
        public IOBDCommand<string> PidsC { get; } = new PidCommand("PIDS_C", "Supported PIDs [41-60]", "0140", 4, ECU.Engine, true);
        public IOBDCommand<string> StatusDriveCycle { get; } = new OBDCommand<string>("STATUS_DRIVE_CYCLE", "Monitor status this drive cycle", "0141", 4, Decoders.Drop, ECU.Engine, true);
        public IOBDCommand<string> ControlModuleVoltage { get; } = new OBDCommand<string>("CONTROL_MODULE_VOLTAGE", "Control module voltage", "0142", 2, Decoders.Drop, ECU.Engine, true);
        public IOBDCommand<string> AbsoluteLoad { get; } = new OBDCommand<string>("ABSOLUTE_LOAD", "Absolute load value", "0143", 2, Decoders.Drop, ECU.Engine, true);
        public IOBDCommand<string> CommandEquivRatio { get; } = new OBDCommand<string>("COMMAND_EQUIV_RATIO", "Command equivalence ratio", "0144", 2, Decoders.Drop, ECU.Engine, true);
        public IOBDCommand<decimal> RelativeThrottlePos { get; } = new OBDCommand<decimal>("RELATIVE_THROTTLE_POS", "Relative throttle position", "0145", 1, Decoders.Percent, ECU.Engine, true);
        public IOBDCommand<int> AmbiantAirTemp { get; } = new OBDCommand<int>("AMBIANT_AIR_TEMP", "Ambient air temperature", "0146", 1, Decoders.Temp, ECU.Engine, true);
        public IOBDCommand<decimal> ThrottlePosB { get; } = new OBDCommand<decimal>("THROTTLE_POS_B", "Absolute throttle position B", "0147", 1, Decoders.Percent, ECU.Engine, true);
        public IOBDCommand<decimal> ThrottlePosC { get; } = new OBDCommand<decimal>("THROTTLE_POS_C", "Absolute throttle position C", "0148", 1, Decoders.Percent, ECU.Engine, true);
        public IOBDCommand<decimal> AcceleratorPosD { get; } = new OBDCommand<decimal>("ACCELERATOR_POS_D", "Accelerator pedal position D", "0149", 1, Decoders.Percent, ECU.Engine, true);
        public IOBDCommand<decimal> AcceleratorPosE { get; } = new OBDCommand<decimal>("ACCELERATOR_POS_E", "Accelerator pedal position E", "014A", 1, Decoders.Percent, ECU.Engine, true);
        public IOBDCommand<decimal> AcceleratorPosF { get; } = new OBDCommand<decimal>("ACCELERATOR_POS_F", "Accelerator pedal position F", "014B", 1, Decoders.Percent, ECU.Engine, true);
        public IOBDCommand<decimal> ThrottleActuator { get; } = new OBDCommand<decimal>("THROTTLE_ACTUATOR", "Commanded throttle actuator", "014C", 1, Decoders.Percent, ECU.Engine, true);
        public IOBDCommand<uint> RunTimeMil { get; } = new OBDCommand<uint>("RUN_TIME_MIL", "Time run with MIL on", "014D", 2, Decoders.Minutes, ECU.Engine, true);
        public IOBDCommand<uint> TimeSinceDtcCleared { get; } = new OBDCommand<uint>("TIME_SINCE_DTC_CLEARED", "Time since trouble codes cleared", "014E", 2, Decoders.Minutes, ECU.Engine, true);
        public IOBDCommand<string> MaxValues { get; } = new OBDCommand<string>("MAX_VALUES", "Various Max values", "014F", 4, Decoders.Drop, ECU.Engine, true);
        public IOBDCommand<uint> MaxMaf { get; } = new OBDCommand<uint>("MAX_MAF", "Maximum value for mass air flow sensor", "0150", 4, Decoders.Max_maf, ECU.Engine, true);
        public IOBDCommand<string> FuelType { get; } = new OBDCommand<string>("FUEL_TYPE", "Fuel Type", "0151", 1, Decoders.Fuel_type, ECU.Engine, true);
        public IOBDCommand<decimal> EthanolPercent { get; } = new OBDCommand<decimal>("ETHANOL_PERCENT", "Ethanol Fuel Percent", "0152", 1, Decoders.Percent, ECU.Engine, true);
        public IOBDCommand<decimal> EvapVaporPressureAbs { get; } = new OBDCommand<decimal>("EVAP_VAPOR_PRESSURE_ABS", "Absolute Evap system Vapor Pressure", "0153", 2, Decoders.Abs_evap_pressure, ECU.Engine, true);
        public IOBDCommand<int> EvapVaporPressureAlt { get; } = new OBDCommand<int>("EVAP_VAPOR_PRESSURE_ALT", "Evap system vapor pressure", "0154", 2, Decoders.Evap_pressure_alt, ECU.Engine, true);
        public IOBDCommand<decimal> ShortO2TrimB1 { get; } = new OBDCommand<decimal>("SHORT_O2_TRIM_B1", "Short term secondary O2 trim - Bank 1", "0155", 2, Decoders.Percent_centered, ECU.Engine, true);
        public IOBDCommand<decimal> LongO2TrimB1 { get; } = new OBDCommand<decimal>("LONG_O2_TRIM_B1", "Long term secondary O2 trim - Bank 1", "0156", 2, Decoders.Percent_centered, ECU.Engine, true);
        public IOBDCommand<decimal> ShortO2TrimB2 { get; } = new OBDCommand<decimal>("SHORT_O2_TRIM_B2", "Short term secondary O2 trim - Bank 2", "0157", 2, Decoders.Percent_centered, ECU.Engine, true);
        public IOBDCommand<decimal> LongO2TrimB2 { get; } = new OBDCommand<decimal>("LONG_O2_TRIM_B2", "Long term secondary O2 trim - Bank 2", "0158", 2, Decoders.Percent_centered, ECU.Engine, true);
        public IOBDCommand<int> FuelRailPressureAbs { get; } = new OBDCommand<int>("FUEL_RAIL_PRESSURE_ABS", "Fuel rail pressure (absolute)", "0159", 2, Decoders.Fuel_pres_direct, ECU.Engine, true);
        public IOBDCommand<decimal> RelativeAccelPos { get; } = new OBDCommand<decimal>("RELATIVE_ACCEL_POS", "Relative accelerator pedal position", "015A", 1, Decoders.Percent, ECU.Engine, true);
        public IOBDCommand<decimal> HybridBatteryRemaining { get; } = new OBDCommand<decimal>("HYBRID_BATTERY_REMAINING", "Hybrid battery pack remaining life", "015B", 1, Decoders.Percent, ECU.Engine, true);
        public IOBDCommand<int> OilTemp { get; } = new OBDCommand<int>("OIL_TEMP", "Engine oil temperature", "015C", 1, Decoders.Temp, ECU.Engine, true);
        public IOBDCommand<int> FuelInjectTiming { get; } = new OBDCommand<int>("FUEL_INJECT_TIMING", "Fuel injection timing", "015D", 2, Decoders.Inject_timing, ECU.Engine, true);
        public IOBDCommand<decimal> FuelRate { get; } = new OBDCommand<decimal>("FUEL_RATE", "Engine fuel rate", "015E", 2, Decoders.Fuel_rate, ECU.Engine, true);
        public IOBDCommand<string> EmissionReq { get; } = new OBDCommand<string>("EMISSION_REQ", "Designed emission requirements", "015F", 1, Decoders.Drop, ECU.Engine, true);
    }
}
