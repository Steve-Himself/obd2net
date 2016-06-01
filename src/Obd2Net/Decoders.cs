using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Obd2Net.Extensions;
using Obd2Net.InfrastructureContracts;
using Obd2Net.InfrastructureContracts.Enums;

namespace Obd2Net
{
    public static class Decoders
    {
        private const string UnknownErrorCode = "Unknown error code";

        public static DecoderValue<string> Pid(params IMessage[] messages)
        {
            var d = messages.First().Data;
            return new DecoderValue<string>(BytesToBits(d), Unit.None);
        }

        public static DecoderValue<uint> Minutes(params IMessage[] messages)
        {
            var data = messages.First().Data;
            var v = Convert.ToUInt32(Utils.BytesToInt(data));
            return new DecoderValue<uint>(v, Unit.Min);
        }

        private static string BytesToBits(IEnumerable<byte> bytes)
        {
            return string.Join("", bytes.Select(b => Convert.ToString(b, 2).PadLeft(8, '0')));
        }

        public static DecoderValue<decimal> Percent_centered(params IMessage[] messages)
        {
            var d = messages[0].Data;
            var v = (d[0] - 128)*100.0m/128.0m;
            return new DecoderValue<decimal>(v, Unit.Percent);
        }

        public static DecoderValue<string> Status(params IMessage[] messages)
        {
            var d = messages[0].Data;
            var bits = new BitArray(d);

            var output = new Status
            {
                Mil = bits[0],
                DTCCount = Utils.Unbin(bits, 0, 7),
                IgnitionType = Codes.IgnitionType[Utils.Unbin(bits, 12)]
            };

            output.Tests.Add(new Test("Misfire", bits[15], bits[11]));

            output.Tests.Add(new Test("Fuel System", bits[14], bits[10]));

            output.Tests.Add(new Test("Components", bits[13], bits[9]));


            // different tests for different ignition types 
            if (output.IgnitionType == Codes.IgnitionType[0])
            {
                // spark
                foreach (var i in Enumerable.Range(0, 7))
                {
                    if (Codes.SparkTests[i] == null) continue;

                    var t = new Test(Codes.SparkTests[i], bits[2*8 + i], bits[3*8 + i]);

                    output.Tests.Add(t);
                }
            }
            else if (output.IgnitionType == Codes.IgnitionType[1])
            {
                // compression
                foreach (var i in Enumerable.Range(0, 7))
                {
                    if (Codes.CompressionTests[i] != null)
                    {
                        var t = new Test(Codes.CompressionTests[i], bits[2*8 + i], bits[3*8 + i]);

                        output.Tests.Add(t);
                    }
                }
            }

            return new DecoderValue<string>(output.ToString(), Unit.None);
        }

        public static DecoderValue<decimal> Current_centered(params IMessage[] messages)
        {
            var d = messages[0].Data;
            var v = Utils.BytesToInt(d, 2, 4);
            var c = v/256.0m - 128;
            return new DecoderValue<decimal>(c, Unit.Ma);
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
        public static DecoderValue<IDictionary<string, string>> DTC(params IMessage[] messages)
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

            return new DecoderValue<IDictionary<string, string>>(codes, Unit.None);
        }

        public static DecoderValue<string> Drop(params IMessage[] messages)
        {
            return new DecoderValue<string>(null, Unit.None);
        }

        public static DecoderValue<string> Fuel_status(params IMessage[] messages)
        {
            var d = messages[0].Data;
            var f1 = (int) d[0]; // Single Fuel System in byte 1

            if (f1 <= 0 || !Enum.IsDefined(typeof(FuelStatus), f1))
                return new DecoderValue<string>(null, Unit.None);

            return new DecoderValue<string>(((FuelStatus) f1).GetDescription(), Unit.None);
        }

        public static DecoderValue<decimal> Percent(params IMessage[] messages)
        {
            var d = messages[0].Data;
            var v = d[0]*100.0m/255.0m;
            return new DecoderValue<decimal>(decimal.Round(v, 2), Unit.Percent);
        }

        public static DecoderValue<int> Temp(params IMessage[] messages)
        {
            var d = messages[0].Data;
            var v = Utils.BytesToInt(d) - 40;
            return new DecoderValue<int>(v, Unit.C);
        }

        public static DecoderValue<uint> Fuel_pressure(params IMessage[] messages)
        {
            var d = messages[0].Data;
            var v = d[0]*3u;
            return new DecoderValue<uint>(v, Unit.Kpa);
        }

        public static DecoderValue<uint> Pressure(params IMessage[] messages)
        {
            var d = messages[0].Data;
            var v = d[0]*1u;
            return new DecoderValue<uint>(v, Unit.Kpa);
        }

        public static DecoderValue<decimal> Rpm(params IMessage[] messages)
        {
            var d = messages[0].Data;
            var v = Utils.BytesToInt(d)*1.0m;
            if (v > 0)
                v = v/4;
            return new DecoderValue<decimal>(v, Unit.Rpm);
        }

        public static DecoderValue<decimal> Speed(params IMessage[] messages)
        {
            var d = messages[0].Data;
            var v = Utils.BytesToInt(d)*1.0m;
            return new DecoderValue<decimal>(v, Unit.Kph);
        }

        public static DecoderValue<decimal> Timing_advance(params IMessage[] messages)
        {
            var d = messages[0].Data;
            var v = (d[0] - 128)/2.0m;
            return new DecoderValue<decimal>(v, Unit.Degrees);
        }

        public static DecoderValue<decimal> Maf(params IMessage[] messages)
        {
            var d = messages[0].Data;
            var v = Utils.BytesToInt(d)/100m;
            return new DecoderValue<decimal>(v, Unit.Gps);
        }

        public static DecoderValue<string> Air_status(params IMessage[] messages)
        {
            var d = messages[0].Data;
            var v = (int) d[0];

            if (v <= 0 || !Enum.IsDefined(typeof(AirStatus), v))
                return new DecoderValue<string>(null, Unit.None);

            return new DecoderValue<string>(((AirStatus) v).GetDescription(), Unit.None);
        }

        public static DecoderValue<decimal?> Elm_voltage(params IMessage[] messages)
        {
            decimal v;
            if (decimal.TryParse(messages[0].Frames[0].Raw, out v))
                return new DecoderValue<decimal?>(v, Unit.Volt);

            return new DecoderValue<decimal?>(null, Unit.None);
        }

        public static DecoderValue<decimal> Sensor_voltage(params IMessage[] messages)
        {
            var d = messages[0].Data;
            var v = d[0]/200.0m;
            return new DecoderValue<decimal>(v, Unit.Volt);
        }

        public static DecoderValue<decimal> Sensor_voltage_big(params IMessage[] messages)
        {
            var d = messages[0].Data;
            var x = Utils.BytesToInt(d, 2, 4);
            var v = decimal.Round(x*8/65535m, 1);
            return new DecoderValue<decimal>(v, Unit.Volt);
        }

        public static DecoderValue<string> Obd_compliance(params IMessage[] messages)
        {
            var d = messages[0].Data;
            var v = Utils.BytesToInt(d);

            var s = "Error: Unknown OBD compliance response";
            if (v >= 0 && v < Codes.OBDCompliance.Count)
                s = Codes.OBDCompliance[v];

            return new DecoderValue<string>(s, Unit.None);
        }

        public static DecoderValue<uint> Seconds(params IMessage[] messages)
        {
            var data = messages.First().Data;
            var v = Convert.ToUInt32(Utils.BytesToInt(data));
            return new DecoderValue<uint>(v, Unit.Sec);
        }

        public static DecoderValue<uint> Distance(params IMessage[] messages)
        {
            var data = messages.First().Data;
            var v = Convert.ToUInt32(Utils.BytesToInt(data));
            return new DecoderValue<uint>(v, Unit.Km);
        }

        public static DecoderValue<decimal> Fuel_pres_vac(params IMessage[] messages)
        {
            var d = messages[0].Data;
            var v = Utils.BytesToInt(d);
            var k = v*0.079M;
            return new DecoderValue<decimal>(k, Unit.Kpa);
        }

        public static DecoderValue<int> Fuel_pres_direct(params IMessage[] messages)
        {
            var d = messages[0].Data;
            var v = Utils.BytesToInt(d)*10;
            return new DecoderValue<int>(v, Unit.Kpa);
        }

        public static DecoderValue<int> Count(params IMessage[] messages)
        {
            var d = messages[0].Data;
            var v = Utils.BytesToInt(d);
            return new DecoderValue<int>(v, Unit.Count);
        }

        public static DecoderValue<decimal> Evap_pressure(params IMessage[] messages)
        {
            var d = messages[0].Data;
            var a = Utils.TwosComp(d[0], 8);
            var b = Utils.TwosComp(d[1], 8);
            var v = (a*256.0m + b)/4.0m;
            return new DecoderValue<decimal>(v, Unit.Pa);
        }

        public static DecoderValue<decimal> Catalyst_temp(params IMessage[] messages)
        {
            var d = messages[0].Data;
            var v = Utils.BytesToInt(d);
            var k = v/10.0m - 40m;
            return new DecoderValue<decimal>(k, Unit.C);
        }

        public static DecoderValue<uint> Max_maf(params IMessage[] messages)
        {
            var d = messages[0].Data;
            var v = d[0]*10u;
            return new DecoderValue<uint>(v, Unit.Gps);
        }

        public static DecoderValue<string> Fuel_type(params IMessage[] messages)
        {
            var d = messages[0].Data;
            var v = (int) d[0];

            var s = "Error: Unknown fuel type response";

            if (v >= 0 && v < Codes.FuelTypes.Count)
                s = Codes.FuelTypes[v];

            return new DecoderValue<string>(s, Unit.None);
        }

        public static DecoderValue<decimal> Abs_evap_pressure(params IMessage[] messages)
        {
            var d = messages[0].Data;
            var v = Utils.BytesToInt(d)/200.0m;

            return new DecoderValue<decimal>(v, Unit.Kpa);
        }

        public static DecoderValue<int> Evap_pressure_alt(params IMessage[] messages)
        {
            var d = messages[0].Data;
            var v = Utils.BytesToInt(d) - 32767;

            return new DecoderValue<int>(v, Unit.Pa);
        }

        public static DecoderValue<int> Inject_timing(params IMessage[] messages)
        {
            var d = messages[0].Data;
            var v = (Utils.BytesToInt(d) - 26880)/128.0m;

            return new DecoderValue<int>(Convert.ToInt32(v), Unit.Degrees);
        }

        public static DecoderValue<decimal> Fuel_rate(params IMessage[] messages)
        {
            var d = messages[0].Data;
            var v = Utils.BytesToInt(d)*0.05m;

            return new DecoderValue<decimal>(decimal.Round(v, 2), Unit.Lph);
        }
    }
}