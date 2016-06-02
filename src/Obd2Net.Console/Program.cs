using System;
using Obd2Net.Configuration;
using Obd2Net.Logging;
using Obd2Net.Ports;
using Obd2Net.Protocols.Can;

namespace Obd2Net.Console
{
    class Program
    {
        static void Main(string[] args)
        {
            var log = new NullLogger();
            var protocol = new ISO_15765_4_29bit_500k();
            var config = new ObdConfiguration("COM3", 9600, TimeSpan.FromMilliseconds(500), true);
            var elm = new Elm327<ISO_15765_4_29bit_500k>(log, protocol, config);
            var obd = new Obd<ISO_15765_4_29bit_500k>(new NullLogger(), elm);
            var x = obd.Query(obd.Commands.Mode1.Speed);
            System.Console.WriteLine(x);
        }
    }
}
