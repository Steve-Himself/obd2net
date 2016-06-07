using System;
using Obd2Net.InfrastructureContracts.Commands;
using Obd2Net.InfrastructureContracts.Enums;
using Obd2Net.InfrastructureContracts.Protocols;

namespace Obd2Net.InfrastructureContracts.Response
{
    public interface IOBDResponse
    {
        DateTime Time { get; set; }
        IMessage[] Messages { get; set; }
        Unit Unit { get; set; }
        object Raw { get; }
        IOBDCommand Command { get; set; }
    }

    public interface IOBDResponse<T> : IOBDResponse
    {
        T Value { get; set; }
    }
}