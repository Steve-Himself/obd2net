using System;
using System.Reactive.Linq;
using System.Reactive.Subjects;
using Obd2Net.Configuration;
using Obd2Net.InfrastructureContracts.Response;
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
            var config = new OBDConfiguration("COM3", 9600, TimeSpan.FromMilliseconds(500), true);
            var elm = new Elm327(log, protocol, config);
            
            var obd = new Obd(new NullLogger(), elm);
            
            var subject = new Subject<IOBDResponse>();

            Observable
                .Interval(TimeSpan.FromMilliseconds(100))
                .Subscribe(x => subject.OnNext(obd.Query(Commands.Mode1.Speed)));

            //Observable
            //    .Interval(TimeSpan.FromMilliseconds(5))
            //    .Subscribe(x => subject.OnNext(obd.Query<,>(fuelPressureCmd, true)));

            subject.DistinctUntilChanged(x => x).Subscribe(System.Console.WriteLine);

            System.Console.ReadKey();
        }
    }
}
