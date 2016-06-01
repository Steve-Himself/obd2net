using Obd2Net.InfrastructureContracts.Enums;

namespace Obd2Net.InfrastructureContracts
{
    public interface IMessage
    {
        ECU Ecu { get; set; }
        IFrame[] Frames { get; set; }
        byte[] Data { get; set; }
        string Hex { get; }
        string Raw { get; }
        int? TxId { get; }
        bool Parsed { get; }
    }
}