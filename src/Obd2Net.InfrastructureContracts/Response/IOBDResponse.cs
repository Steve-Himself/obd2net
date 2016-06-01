using System;
using Obd2Net.InfrastructureContracts.Commands;
using Obd2Net.InfrastructureContracts.Enums;

namespace Obd2Net.InfrastructureContracts.Response
{
    public interface IOBDResponse<T>
    {
        IOBDCommand<T> Command { get; }
        IMessage[] Messages { get; }
        T Value { get; set; }
        Unit Unit { get; set; }
        DateTime Time { get; set; }
    }
}