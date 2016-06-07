using System;

namespace Obd2Net.InfrastructureContracts
{
    public interface IOBDConfiguration
    {
        int Baudrate { get; }
        string Portname { get; }
        bool Fast { get; }
        TimeSpan Timeout { get; }
    }
}