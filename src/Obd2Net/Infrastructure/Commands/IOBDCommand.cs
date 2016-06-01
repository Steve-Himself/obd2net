using System;
using Obd2Net.Infrastructure.Response;
using Obd2Net.InfrastructureContracts;
using Obd2Net.InfrastructureContracts.Enums;

namespace Obd2Net.Infrastructure.Commands
{
    public interface IOBDCommand<T> : IOBDCommand
    {
        Func<IMessage[], DecoderValue<T>> Decoder { get; set; }
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