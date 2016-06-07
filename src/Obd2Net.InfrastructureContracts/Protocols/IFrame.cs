namespace Obd2Net.InfrastructureContracts.Protocols
{
    public interface IFrame
    {
        string Raw { get; set; }
        byte[] Data { get; set; }
        int Priority { get; set; }
        int AddrMode { get; set; }
        int RxId { get; set; }
        int TxId { get; set; }
        int Type { get; set; }
        int SeqIndex { get; set; }
        int DataLen { get; set; }
    }
}