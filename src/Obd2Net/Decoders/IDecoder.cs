using Obd2Net.InfrastructureContracts.Protocols;
using Obd2Net.InfrastructureContracts.Response;

namespace Obd2Net.Decoders
{
    public interface IDecoder { }
    public interface IDecoder<T> : IDecoder
    {
        IOBDResponse<T> Execute(params IMessage[] messages);
    }
}