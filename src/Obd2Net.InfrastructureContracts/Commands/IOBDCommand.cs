using System;
using Obd2Net.InfrastructureContracts.Enums;

namespace Obd2Net.InfrastructureContracts.Commands
{
    public interface IOBDCommand<T> : IOBDCommand
    {
        Func<IMessage[], IDecoderValue<T>> Decoder { get; }
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

    public interface IMode1OBDCommand { }

    public interface IPidOBDCommand
    {
    }
}