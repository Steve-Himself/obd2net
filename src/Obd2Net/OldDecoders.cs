using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Obd2Net.Extensions;
using Obd2Net.Infrastructure;
using Obd2Net.InfrastructureContracts.Enums;
using Obd2Net.InfrastructureContracts.Protocols;
using Obd2Net.Protocols;

namespace Obd2Net
{
    internal static class OldDecoders
    {
        private const string UnknownErrorCode = "Unknown error code";

        public static OBDResponse<string> Pid(params IMessage[] messages)
        {
            var d = messages[0].Data;
            return new OBDResponse<string>(Utils.BytesToBits(d), Unit.None);
        }

        public static OBDResponse<uint> Minutes(params IMessage[] messages)
        {
            var data = messages[0].Data;
            var v = Convert.ToUInt32(Utils.BytesToInt(data));
            return new OBDResponse<uint>(v, Unit.Min);
        }

        public static OBDResponse<decimal> Percent_centered(params IMessage[] messages)
        {
            var d = messages[0].Data;
            var v = (d[0] - 128)*100.0m/128.0m;
            return new OBDResponse<decimal>(v, Unit.Percent);
        }

        public static OBDResponse<string> Status(params IMessage[] messages)
        {
            var d = messages[0].Data;
            var bits = new BitArray(d);

            var ignitionType = (IgnitionType) Utils.Unbin(bits, 12);
            var output = new Status
            {
                Mil = bits[0],
                DTCCount = Utils.Unbin(bits, 0, 7),
                IgnitionType = ignitionType.GetDescription()
            };

            output.Tests.Add(new Test("Misfire", bits[15], bits[11]));

            output.Tests.Add(new Test("Fuel System", bits[14], bits[10]));

            output.Tests.Add(new Test("Components", bits[13], bits[9]));


            // different tests for different ignition types 
            if (ignitionType == IgnitionType.Spark)
            {
                // spark
                foreach (int i in Enum.GetValues(typeof(SparkTests)))
                {
                    var t = new Test(((SparkTests)i).GetDescription(), bits[2*8 + i], bits[3*8 + i]);

                    output.Tests.Add(t);
                }
            }
            else if (ignitionType == IgnitionType.Compression)
            {
                // compression
                foreach (int i in Enum.GetValues(typeof(CompressionTest)))
                {
                        var t = new Test(((CompressionTest)i).GetDescription(), bits[2*8 + i], bits[3*8 + i]);

                        output.Tests.Add(t);
                }
            }

            return new OBDResponse<string>(output.ToString(), Unit.None);
        }

        public static OBDResponse<decimal> Current_centered(params IMessage[] messages)
        {
            var d = messages[0].Data;
            var v = Utils.BytesToInt(d, 2, 4);
            var c = v/256.0m - 128;
            return new OBDResponse<decimal>(c, Unit.Ma);
        }

        internal static string SingleDtc(byte[] bytes)
        {
            if (bytes.Length != 2 || (bytes[0] == 0 && bytes[0] == 0))
                return null;

            // BYTES: (16,      35      )
            // HEX:    4   1    2   3
            // BIN:    01000001 00100011
            //         [][][  in hex   ]
            //         | / /
            // DTC:    C0123
            var type = new[] {"P", "C", "B", "U"};
            var dtc = type[bytes[0] >> 6] + // the last 2 bits of the first byte
                      ((bytes[0] >> 4) & 0x30) + // the next pair of 2 bits. Mask off the bits we read above
                      BitConverter.ToString(bytes, 0).Replace("-", "").Substring(1, 3);
            return dtc;
        }

        /// <summary>
        ///     Converts a frame of 2-byte DTCs into a list of DTCs
        /// </summary>
        public static OBDResponse<IDictionary<string, string>> DTC(params IMessage[] messages)
        {
            var codes = new Dictionary<string, string>();
            var d = messages.SelectMany(m => m.Data).ToArray();

            for (var n = 1; n < d.Length; n += 2)
            {
                var dtc = SingleDtc(new[] {d[n - 1], d[n]});
                if (string.IsNullOrWhiteSpace(dtc)) continue;

                string desc = null;

                if (Codes.DTC.ContainsKey(dtc))
                    desc = Codes.DTC[dtc];
                codes[dtc] = desc ?? UnknownErrorCode;
            }

            return new OBDResponse<IDictionary<string, string>>(codes, Unit.None);
        }

        public static OBDResponse<string> Drop(params IMessage[] messages)
        {
            return new OBDResponse<string>(null, Unit.None);
        }

        public static OBDResponse<string> Fuel_status(params IMessage[] messages)
        {
            var d = messages[0].Data;
            var f1 = (int) d[0]; // Single Fuel System in byte 1

            if (f1 <= 0 || !Enum.IsDefined(typeof(FuelStatus), f1))
                return new OBDResponse<string>(null, Unit.None);

            return new OBDResponse<string>(((FuelStatus) f1).GetDescription(), Unit.None);
        }

        public static OBDResponse<decimal> Percent(params IMessage[] messages)
        {
            var d = messages[0].Data;
            var v = d[0]*100.0m/255.0m;
            return new OBDResponse<decimal>(decimal.Round(v, 2), Unit.Percent);
        }

        public static OBDResponse<int> Temp(params IMessage[] messages)
        {
            var d = messages[0].Data;
            var v = Utils.BytesToInt(d) - 40;
            return new OBDResponse<int>(v, Unit.C);
        }

        public static OBDResponse<uint> Fuel_pressure(params IMessage[] messages)
        {
            var d = messages[0].Data;
            var v = d[0]*3u;
            return new OBDResponse<uint>(v, Unit.Kpa);
        }

        public static OBDResponse<uint> Pressure(params IMessage[] messages)
        {
            var d = messages[0].Data;
            var v = d[0]*1u;
            return new OBDResponse<uint>(v, Unit.Kpa);
        }

        public static OBDResponse<decimal> Rpm(params IMessage[] messages)
        {
            var d = messages[0].Data;
            var v = Utils.BytesToInt(d)*1.0m;
            if (v > 0)
                v = v/4;
            return new OBDResponse<decimal>(v, Unit.Rpm);
        }

        public static OBDResponse<uint> Speed(params IMessage[] messages)
        {
            var d = messages[0].Data;
            var v = Convert.ToUInt32(Utils.BytesToInt(d));
            return new OBDResponse<uint>(v, Unit.Kph);
        }

        public static OBDResponse<decimal> Timing_advance(params IMessage[] messages)
        {
            var d = messages[0].Data;
            var v = (d[0] - 128)/2.0m;
            return new OBDResponse<decimal>(v, Unit.Degrees);
        }

        public static OBDResponse<decimal> Maf(params IMessage[] messages)
        {
            var d = messages[0].Data;
            var v = Utils.BytesToInt(d)/100m;
            return new OBDResponse<decimal>(v, Unit.Gps);
        }

        public static OBDResponse<string> Air_status(params IMessage[] messages)
        {
            var d = messages[0].Data;
            var v = (int) d[0];

            if (v <= 0 || !Enum.IsDefined(typeof(AirStatus), v))
                return new OBDResponse<string>(null, Unit.None);

            return new OBDResponse<string>(((AirStatus) v).GetDescription(), Unit.None);
        }

        public static OBDResponse<decimal?> Elm_voltage(params IMessage[] messages)
        {
            decimal v;
            if (decimal.TryParse(messages[0].Frames[0].Raw, out v))
                return new OBDResponse<decimal?>(v, Unit.Volt);

            return new OBDResponse<decimal?>(null, Unit.None);
        }

        public static OBDResponse<decimal> Sensor_voltage(params IMessage[] messages)
        {
            var d = messages[0].Data;
            var v = d[0]/200.0m;
            return new OBDResponse<decimal>(v, Unit.Volt);
        }

        public static OBDResponse<decimal> Sensor_voltage_big(params IMessage[] messages)
        {
            var d = messages[0].Data;
            var x = Utils.BytesToInt(d, 2, 4);
            var v = decimal.Round(x*8/65535m, 1);
            return new OBDResponse<decimal>(v, Unit.Volt);
        }

        public static OBDResponse<string> Obd_compliance(params IMessage[] messages)
        {
            var d = messages[0].Data;
            var v = Utils.BytesToInt(d);

            if (v <= 0 || !Enum.IsDefined(typeof(OBDCompliance), v))
                return new OBDResponse<string>("Error: Unknown OBD compliance response", Unit.None);

            return new OBDResponse<string>(((OBDCompliance)v).GetDescription(), Unit.None);
        }

        public static OBDResponse<uint> Seconds(params IMessage[] messages)
        {
            var data = messages[0].Data;
            var v = Convert.ToUInt32(Utils.BytesToInt(data));
            return new OBDResponse<uint>(v, Unit.Sec);
        }

        public static OBDResponse<uint> Distance(params IMessage[] messages)
        {
            var data = messages[0].Data;
            var v = Convert.ToUInt32(Utils.BytesToInt(data));
            return new OBDResponse<uint>(v, Unit.Km);
        }

        public static OBDResponse<decimal> Fuel_pres_vac(params IMessage[] messages)
        {
            var d = messages[0].Data;
            var v = Utils.BytesToInt(d);
            var k = v*0.079M;
            return new OBDResponse<decimal>(k, Unit.Kpa);
        }

        public static OBDResponse<int> Fuel_pres_direct(params IMessage[] messages)
        {
            var d = messages[0].Data;
            var v = Utils.BytesToInt(d)*10;
            return new OBDResponse<int>(v, Unit.Kpa);
        }

        public static OBDResponse<int> Count(params IMessage[] messages)
        {
            var d = messages[0].Data;
            var v = Utils.BytesToInt(d);
            return new OBDResponse<int>(v, Unit.Count);
        }

        public static OBDResponse<decimal> Evap_pressure(params IMessage[] messages)
        {
            var d = messages[0].Data;
            var a = Utils.TwosComp(d[0], 8);
            var b = Utils.TwosComp(d[1], 8);
            var v = (a*256.0m + b)/4.0m;
            return new OBDResponse<decimal>(v, Unit.Pa);
        }

        public static OBDResponse<decimal> Catalyst_temp(params IMessage[] messages)
        {
            var d = messages[0].Data;
            var v = Utils.BytesToInt(d);
            var k = v/10.0m - 40m;
            return new OBDResponse<decimal>(k, Unit.C);
        }

        public static OBDResponse<uint> Max_maf(params IMessage[] messages)
        {
            var d = messages[0].Data;
            var v = d[0]*10u;
            return new OBDResponse<uint>(v, Unit.Gps);
        }

        public static OBDResponse<string> Fuel_type(params IMessage[] messages)
        {
            var d = messages[0].Data;
            var v = (int) d[0];

            if (v <= 0 || !Enum.IsDefined(typeof(FuelType), v))
                return new OBDResponse<string>("Error: Unknown fuel type response", Unit.None);

            return new OBDResponse<string>(((FuelType)v).GetDescription(), Unit.None);
        }

        public static OBDResponse<decimal> Abs_evap_pressure(params IMessage[] messages)
        {
            var d = messages[0].Data;
            var v = Utils.BytesToInt(d)/200.0m;

            return new OBDResponse<decimal>(v, Unit.Kpa);
        }

        public static OBDResponse<int> Evap_pressure_alt(params IMessage[] messages)
        {
            var d = messages[0].Data;
            var v = Utils.BytesToInt(d) - 32767;

            return new OBDResponse<int>(v, Unit.Pa);
        }

        public static OBDResponse<int> Inject_timing(params IMessage[] messages)
        {
            var d = messages[0].Data;
            var v = (Utils.BytesToInt(d) - 26880)/128.0m;

            return new OBDResponse<int>(Convert.ToInt32(v), Unit.Degrees);
        }

        public static OBDResponse<decimal> Fuel_rate(params IMessage[] messages)
        {
            var d = messages[0].Data;
            var v = Utils.BytesToInt(d)*0.05m;

            return new OBDResponse<decimal>(decimal.Round(v, 2), Unit.Lph);
        }

        public static OBDResponse<string> RawString(params IMessage[] messages)
        {
            return new OBDResponse<string>(string.Join("\n", messages.Select(m => m.Raw).ToArray()), Unit.None);
        }
    }
}