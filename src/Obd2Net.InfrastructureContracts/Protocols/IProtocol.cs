namespace Obd2Net.InfrastructureContracts.Protocols
{
    public interface IProtocol
    {
        string ElmName { get; }
        string ElmId { get; }
        bool ParseFrame(IFrame frame);
        bool ParseMessage(IMessage message);
        IMessage[] Parse(params string[] lines);
    }
}