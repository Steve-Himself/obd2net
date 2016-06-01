using System;
using Obd2Net.InfrastructureContracts.Enums;

namespace Obd2Net.InfrastructureContracts
{
    public interface IPort
    {
        OBDStatus Status { get; }
        string PortName { get; }
        ECU[] Ecus { get; }
        string ProtocolName { get; }
        string ProtocolId { get; }
        void Close();
        IMessage[] SendAndParse(string cmd);
        string[] Send(string cmd, TimeSpan? delay = null);
    }
}