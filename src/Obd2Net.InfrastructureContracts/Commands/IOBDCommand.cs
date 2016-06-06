using System;
using Obd2Net.InfrastructureContracts.Enums;
using Obd2Net.InfrastructureContracts.Response;

namespace Obd2Net.InfrastructureContracts.Commands
{
    public interface IOBDCommand<T> : IOBDCommand
    {
        Func<IMessage[], IOBDResponse<T>> Decoder { get; }
    }

    public interface IOBDCommand
    {
        string Name { get; }
        //string Description { get; }
        string Command { get; }
        int Bytes { get; }
        ECU Ecu { get; }
        bool Fast { get; }
        int Mode { get; }
        int Pid { get; }
    }
}