using System;
using System.Reactive.Linq;
using System.Reactive.Subjects;
using Obd2Net.Configuration;
using Obd2Net.Infrastructure.Commands;
using Obd2Net.InfrastructureContracts;
using Obd2Net.Logging;
using Obd2Net.Ports;
using Obd2Net.Protocols.Can;

namespace Obd2Net.Console
{
    class Program
    {
        static void Main(string[] args)
        {
            var log = new ConsoleLogger();
            var protocol = new ISO_15765_4_29bit_500k();
            var config = new ObdConfiguration("COM3", 9600, TimeSpan.FromMilliseconds(500), true);
            var elm = new Elm327<ISO_15765_4_29bit_500k>(log, protocol, config);
            var commands = new Commands(new [] { new Speed() });
            var obd = new Obd(new NullLogger(), elm, commands);
            
            var subject = new Subject<IDecoderValue>();
            var speedCmd = new Speed();
            var fuelPressureCmd = new FuelPressure();

            Observable
                .Interval(TimeSpan.FromSeconds(1))
                .Subscribe(x => subject.OnNext(obd.Query(speedCmd, true)));

            Observable
                .Interval(TimeSpan.FromSeconds(5))
                .Subscribe(x => subject.OnNext(obd.Query(fuelPressureCmd, true)));

            subject.DistinctUntilChanged(x => x).Subscribe(System.Console.WriteLine);

            System.Console.ReadKey();
        }
    }
}
