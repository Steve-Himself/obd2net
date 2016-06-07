using System;
using Obd2Net.InfrastructureContracts;

namespace Obd2Net.Configuration
{
    public class OBDConfiguration : IOBDConfiguration
    {
        public OBDConfiguration()
        {
        }

        public OBDConfiguration(string portname, int baudrate, TimeSpan timeout, bool fast = true)
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

    public class DefaultOBDConfiguration : IOBDConfiguration
    {
        public int Baudrate => 9600;
        public string Portname => "COM1";
        public bool Fast => true;
        public TimeSpan Timeout => TimeSpan.FromSeconds(1);
    }
}
