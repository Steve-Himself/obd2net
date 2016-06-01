using System;
using Obd2Net.InfrastructureContracts.Enums;
using Obd2Net.InfrastructureContracts.Response;

namespace Obd2Net.InfrastructureContracts.Commands
{
    public interface IOBDCommand<T> : IOBDCommand
    {
        Func<IMessage[], IDecoderValue<T>> Decoder { get; set; }
        IOBDResponse<T> Execute(IMessage[] messages);
    }

    public interface IOBDCommand
    {
        string Name { get; set; }
        string Description { get; set; }
        string Command { get; set; }
        int Bytes { get; set; }
        ECU Ecu { get; set; }
        bool Fast { get; set; }
        int Mode { get; }
        int Pid { get; }
    }
}