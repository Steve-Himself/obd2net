using System;
using Obd2Net.Infrastructure.Commands;
using Obd2Net.InfrastructureContracts;
using Obd2Net.InfrastructureContracts.Enums;

namespace Obd2Net.Infrastructure.Response
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