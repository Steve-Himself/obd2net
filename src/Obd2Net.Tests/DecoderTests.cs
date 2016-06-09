using System.Collections;
using System.Collections.Generic;
using FluentAssertions;
using NUnit.Framework;
using Obd2Net.Decoders;
using Obd2Net.Infrastructure;
using Obd2Net.InfrastructureContracts.Enums;
using Obd2Net.InfrastructureContracts.Protocols;
using Obd2Net.InfrastructureContracts.Response;
using Obd2Net.Protocols;

namespace Obd2Net.Tests
{
    public class DecoderTests
    {
        private T Decoder<T>() where T : class, IDecoder, new()
        {
            return new T();
        }

        [TestCase("00000000", "00000000000000000000000000000000", Unit.None)]
        [TestCase("F00AA00F", "11110000000010101010000000001111", Unit.None)]
        [TestCase("11", "00010001", Unit.None)]
        public void PidTest(string hex, string result, Unit unit)
        {
            Decoder<PidDecoder>().Execute(Message.FromHexString(hex)).ShouldBeEquivalentTo(new OBDResponse<string>(result, unit));
        }


        [Test]
        [Ignore("TODO")]
        public void EvapPressureTest()
        {
            true.Should().BeFalse();
        }

        [Test]
        [Ignore("TODO")]
        public void ObdComplianceTest()
        {
            true.Should().BeFalse();
        }

        [Test]
        [Ignore("TODO")]
        public void StatusTest()
        {
            true.Should().BeFalse();
        }

        [TestCase("00", 0, Unit.Count)]
        [TestCase("0F", 15, Unit.Count)]
        [TestCase("03E8", 1000, Unit.Count)]
        public void CountTest(string hex, int result, Unit unit)
        {
            Decoder<CountDecoder>().Execute(Message.FromHexString(hex)).ShouldBeEquivalentTo(new OBDResponse<int>(result, unit));
        }

        [TestCase("00", 0, Unit.Percent)]
        [TestCase("FF", 100, Unit.Percent)]
        [TestCase("03E8", 1.18, Unit.Percent)]
        public void PercentTest(string hex, decimal result, Unit unit)
        {
            Decoder<PercentDecoder>().Execute(Message.FromHexString(hex)).ShouldBeEquivalentTo(new OBDResponse<decimal>(result, unit));
        }

        [TestCase("00", -100, Unit.Percent)]
        [TestCase("80", 0, Unit.Percent)]
        [TestCase("FF", 99.21875, Unit.Percent)]
        public void PercentCenteredTest(string hex, decimal result, Unit unit)
        {
            Decoder<PercentCenteredDecoder>().Execute(Message.FromHexString(hex)).ShouldBeEquivalentTo(new OBDResponse<decimal>(result, unit));
        }

        [TestCase("00", -40, Unit.C)]
        [TestCase("FF", 215, Unit.C)]
        [TestCase("03E8", 960, Unit.C)]
        public void TemperatureTest(string hex, int result, Unit unit)
        {
            Decoder<TemperatureDecoder>().Execute(Message.FromHexString(hex)).ShouldBeEquivalentTo(new OBDResponse<int>(result, unit));
        }

        [TestCase("0000", -40, Unit.C)]
        [TestCase("FFFF", 6513.5, Unit.C)]
        public void CatalystTemperatureTest(string hex, decimal result, Unit unit)
        {
            Decoder<CatalystTemperatureDecoder>().Execute(Message.FromHexString(hex)).ShouldBeEquivalentTo(new OBDResponse<decimal>(result, unit));
        }

        [TestCase("00000000", -128, Unit.Ma)]
        [TestCase("00008000", 0, Unit.Ma)]
        [TestCase("0000FFFF", 127.99609375, Unit.Ma)]
        [TestCase("ABCD8000", 0, Unit.Ma)]
        public void CurrentCenteredTest(string hex, decimal result, Unit unit)
        {
            Decoder<CurrentCenteredDecoder>().Execute(Message.FromHexString(hex)).ShouldBeEquivalentTo(new OBDResponse<decimal>(result, unit));
        }

        [TestCase("0000", 0, Unit.Volt)]
        [TestCase("FFFF", 1.275, Unit.Volt)]
        public void SensorVoltageTest(string hex, decimal result, Unit unit)
        {
            Decoder<SensorVoltageDecoder>().Execute(Message.FromHexString(hex)).ShouldBeEquivalentTo(new OBDResponse<decimal>(result, unit));
        }

        [TestCase("00000000", 0, Unit.Volt)]
        [TestCase("00008000", 4, Unit.Volt)]
        [TestCase("0000FFFF", 8, Unit.Volt)]
        [TestCase("ABCD0000", 0, Unit.Volt)]
        public void SensorVoltageBigTest(string hex, decimal result, Unit unit)
        {
            Decoder<SensorVoltageBigDecoder>().Execute(Message.FromHexString(hex)).ShouldBeEquivalentTo(new OBDResponse<decimal>(result, unit));
        }

        [TestCase("00", 0u, Unit.Kpa)]
        [TestCase("80", 384u, Unit.Kpa)]
        [TestCase("FF", 765u, Unit.Kpa)]
        public void FuelPressureTest(string hex, uint result, Unit unit)
        {
            Decoder<FuelPressureDecoder>().Execute(Message.FromHexString(hex)).ShouldBeEquivalentTo(new OBDResponse<uint>(result, unit));
        }

        [TestCase("00", 0u, Unit.Kpa)]
        [TestCase("80", 128u, Unit.Kpa)]
        public void PressureTest(string hex, uint result, Unit unit)
        {
            OldDecoders.Pressure(Message.FromHexString(hex)).ShouldBeEquivalentTo(new OBDResponse<uint>(result, unit));
        }

        [TestCase("0000", 0, Unit.Kpa)]
        [TestCase("0080", 10.112, Unit.Kpa)]
        [TestCase("FFFF", 5177.265, Unit.Kpa)]
        public void FuelPresureVacTest(string hex, decimal result, Unit unit)
        {
            Decoder<FuelPressureVacuumDecoder>().Execute(Message.FromHexString(hex)).ShouldBeEquivalentTo(new OBDResponse<decimal>(result, unit));
        }

        [TestCase("0000", 0, Unit.Kpa)]
        [TestCase("F00F", 614550, Unit.Kpa)]
        [TestCase("FFFF", 655350, Unit.Kpa)]
        public void FuelPresureDirectTest(string hex, int result, Unit unit)
        {
            Decoder<FuelPressureDirectDecoder>().Execute(Message.FromHexString(hex)).ShouldBeEquivalentTo(new OBDResponse<int>(result, unit));
        }

        [TestCase("0000", 0, Unit.Kpa)]
        [TestCase("F00F", 307.275, Unit.Kpa)]
        [TestCase("FFFF", 327.675, Unit.Kpa)]
        public void AbsEvapPressureTest(string hex, decimal result, Unit unit)
        {
            Decoder<AbsEvapPressureDecoder>().Execute(Message.FromHexString(hex)).ShouldBeEquivalentTo(new OBDResponse<decimal>(result, unit));
        }

        [TestCase("0000", -32767, Unit.Pa)]
        [TestCase("7FFF", 0, Unit.Pa)]
        [TestCase("FFFF", 32768, Unit.Pa)]
        public void EvapPressureAltTest(string hex, int result, Unit unit)
        {
            Decoder<EvapPressureAltDecoder>().Execute(Message.FromHexString(hex)).ShouldBeEquivalentTo(new OBDResponse<int>(result, unit));
        }

        [TestCase("0000", 0u, Unit.Rpm)]
        [TestCase("00C1", 48u, Unit.Rpm)]
        [TestCase("7FFF", 8191u, Unit.Rpm)]
        [TestCase("FFFF", 16383u, Unit.Rpm)]
        public void RpmTest(string hex, uint result, Unit unit)
        {
            Decoder<RpmDecoder>().Execute(Message.FromHexString(hex)).ShouldBeEquivalentTo(new OBDResponse<uint>(result, unit));
        }

        [TestCase("00", 0, Unit.Kph)]
        [TestCase("FF", 255, Unit.Kph)]
        [TestCase("A3", 163, Unit.Kph)]
        public void SpeedTest(string hex, decimal result, Unit unit)
        {
            Decoder<SpeedDecoder>().Execute(Message.FromHexString(hex)).ShouldBeEquivalentTo(new OBDResponse<decimal>(result, unit));
        }

        [TestCase("00", -64, Unit.Degrees)]
        [TestCase("FF", 63.5, Unit.Degrees)]
        [TestCase("A0", 16, Unit.Degrees)]
        public void TimingAdvanceTest(string hex, decimal result, Unit unit)
        {
            Decoder<TimingAdvanceDecoder>().Execute(Message.FromHexString(hex)).ShouldBeEquivalentTo(new OBDResponse<decimal>(result, unit));
        }

        [TestCase("0000", -210, Unit.Degrees)]
        [TestCase("FFFF", 302, Unit.Degrees)]
        [TestCase("C9A0", 193, Unit.Degrees)]
        public void InjectTimingTest(string hex, int result, Unit unit)
        {
            Decoder<InjectTimingDecoder>().Execute(Message.FromHexString(hex)).ShouldBeEquivalentTo(new OBDResponse<int>(result, unit));
        }

        [TestCase("0000", 0, Unit.Gps)]
        [TestCase("FFFF", 655.35, Unit.Gps)]
        [TestCase("C9A0", 516.16, Unit.Gps)]
        public void MafTest(string hex, decimal result, Unit unit)
        {
            Decoder<MafDecoder>().Execute(Message.FromHexString(hex)).ShouldBeEquivalentTo(new OBDResponse<decimal>(result, unit));
        }

        [TestCase("00000000", 0u, Unit.Gps)]
        [TestCase("FF000000", 2550u, Unit.Gps)]
        [TestCase("00ABCDEF", 0u, Unit.Gps)]
        public void MaxMafTest(string hex, uint result, Unit unit)
        {
            Decoder<MaxMafDecoder>().Execute(Message.FromHexString(hex)).ShouldBeEquivalentTo(new OBDResponse<uint>(result, unit));
        }

        [TestCase("0000", 0u, Unit.Sec)]
        [TestCase("FFFF", 65535u, Unit.Sec)]
        [TestCase("C9A0", 51616u, Unit.Sec)]
        public void SencondsTest(string hex, uint result, Unit unit)
        {
            Decoder<SecondsDecoder>().Execute(Message.FromHexString(hex)).ShouldBeEquivalentTo(new OBDResponse<uint>(result, unit));
        }

        [TestCase("0000", 0u, Unit.Min)]
        [TestCase("FFFF", 65535u, Unit.Min)]
        [TestCase("C9A0", 51616u, Unit.Min)]
        public void MinutesTest(string hex, uint result, Unit unit)
        {
            Decoder<MinutesDecoder>().Execute(Message.FromHexString(hex)).ShouldBeEquivalentTo(new OBDResponse<uint>(result, unit));
        }

        [TestCase("0000", 0u, Unit.Km)]
        [TestCase("FFFF", 65535u, Unit.Km)]
        [TestCase("C9A0", 51616u, Unit.Km)]
        public void DistanceTest(string hex, uint result, Unit unit)
        {
            Decoder<DistanceDecoder>().Execute(Message.FromHexString(hex)).ShouldBeEquivalentTo(new OBDResponse<uint>(result, unit));
        }

        [TestCase("0000", 0, Unit.Lph)]
        [TestCase("FFFF", 3276.75, Unit.Lph)]
        [TestCase("C9A0", 2580.80, Unit.Lph)]
        public void FuelRateTest(string hex, decimal result, Unit unit)
        {
            Decoder<FuelRateDecoder>().Execute(Message.FromHexString(hex)).ShouldBeEquivalentTo(new OBDResponse<decimal>(result, unit));
        }

        [TestCase("0100", "Open loop due to insufficient engine temperature", Unit.None)]
        [TestCase("0800", "Open loop due to system failure", Unit.None)]
        [TestCase("0300", null, Unit.None)]
        [TestCase("1000", "Closed loop, using at least one oxygen sensor but there is a fault in the feedback system", Unit.None)]
        public void FuelStatusTest(string hex, string result, Unit unit)
        {
            Decoder<FuelStatusDecoder>().Execute(Message.FromHexString(hex)).ShouldBeEquivalentTo(new OBDResponse<string>(result, unit));
        }

        [TestCase("0100", "Upstream", Unit.None)]
        [TestCase("0800", "Pump commanded on for diagnostics", Unit.None)]
        [TestCase("0300", null, Unit.None)]
        [TestCase("1000", null, Unit.None)]
        public void AirStatusTest(string hex, string result, Unit unit)
        {
            Decoder<AirStatusDecoder>().Execute(Message.FromHexString(hex)).ShouldBeEquivalentTo(new OBDResponse<string>(result, unit));
        }

        [TestCase("12.875", 12.875, Unit.Volt)]
        [TestCase("12", 12, Unit.Volt)]
        [TestCase("12ABCD", null, Unit.None)]
        public void ElmVoltageTest(string raw, decimal? result, Unit unit)
        {
            Decoder<ElmVoltageDecoder>().Execute(new Message(new Frame(raw))).ShouldBeEquivalentTo(new OBDResponse<decimal?>(result, unit));
        }

        [TestCaseSource(typeof(DtcTestCases), nameof(DtcTestCases.TestCases))]
        public void DtcTest(IMessage[] messages, IOBDResponse<IDictionary<string, string>> result)
        {
            Decoder<DtcDecoder>().Execute(messages).ShouldBeEquivalentTo(result);
        }

        public class DtcTestCases
        {
            public static IEnumerable TestCases
            {
                get
                {
                    yield return new TestCaseData(new[] {Message.FromHexString("0104")},
                        new OBDResponse<IDictionary<string, string>>(
                            new Dictionary<string, string>
                            {
                                {"P0104", "Mass or Volume Air Flow Circuit Intermittent"}
                            }, Unit.None))
                    {
                        TestName = "Single Code"
                    };

                    yield return new TestCaseData(new[] {Message.FromHexString("010480034123")},
                        new OBDResponse<IDictionary<string, string>>(
                            new Dictionary<string, string>
                            {
                                {"P0104", "Mass or Volume Air Flow Circuit Intermittent"},
                                {"B0003", "Unknown error code"},
                                {"C0123", "Unknown error code"}
                            }, Unit.None))
                    {
                        TestName = "Multiple Codes"
                    };

                    yield return new TestCaseData(new[] {Message.FromHexString("0104800341")},
                        new OBDResponse<IDictionary<string, string>>(
                            new Dictionary<string, string>
                            {
                                {"P0104", "Mass or Volume Air Flow Circuit Intermittent"},
                                {"B0003", "Unknown error code"}
                            }, Unit.None))
                    {
                        TestName = "Invalid code lengths are dropped"
                    };

                    yield return new TestCaseData(new[] {Message.FromHexString("000001040000")},
                        new OBDResponse<IDictionary<string, string>>(
                            new Dictionary<string, string>
                            {
                                {"P0104", "Mass or Volume Air Flow Circuit Intermittent"}
                            }, Unit.None))
                    {
                        TestName = "0000 codes are dropped"
                    };

                    yield return new TestCaseData(new[] {Message.FromHexString("0104"), Message.FromHexString("8003"), Message.FromHexString("0000")},
                        new OBDResponse<IDictionary<string, string>>(
                            new Dictionary<string, string>
                            {
                                {"P0104", "Mass or Volume Air Flow Circuit Intermittent"},
                                {"B0003", "Unknown error code"}
                            }, Unit.None))
                    {
                        TestName = "Multiple Messages"
                    };
                }
            }
        }
    }
}