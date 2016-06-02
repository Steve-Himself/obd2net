using System;
using Obd2Net.InfrastructureContracts;

namespace Obd2Net.Configuration
{
    public class ObdConfiguration : IObdConfiguration
    {
        public ObdConfiguration()
        {
        }

        public ObdConfiguration(string portname, int baudrate, TimeSpan timeout, bool fast = true)
        {
            Portname = portname;
            Baudrate = baudrate;
            Timeout = timeout;
            Fast = fast;
        }

        public string Portname { get; set; }
        public bool Fast { get; set; }
        public TimeSpan Timeout { get; set; }
        public int Baudrate { get; set; }
    }

    public class DefaultObdConfiguration : IObdConfiguration
    {
        public int Baudrate => 9600;
        public string Portname => "COM1";
        public bool Fast => true;
        public TimeSpan Timeout => TimeSpan.FromSeconds(1);
    }
}
