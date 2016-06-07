using System.Collections.Generic;
using Obd2Net.InfrastructureContracts.Enums;

namespace Obd2Net.InfrastructureContracts.Protocols
{
    public interface IProtocol
    {
        string ElmName { get; }
        string ElmId { get; }
        IDictionary<int, ECU> EcuMap { get; }
        bool ParseFrame(IFrame frame);
        bool ParseMessage(IMessage message);
        IMessage[] Parse(params string[] lines);
        void PopulateEcuMap(params string[] messages);
    }
}