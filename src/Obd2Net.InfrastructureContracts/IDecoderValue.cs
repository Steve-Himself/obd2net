using Obd2Net.InfrastructureContracts.Enums;

namespace Obd2Net.InfrastructureContracts
{
    public interface IDecoderValue
    {
        Unit Unit { get; }
        object Raw { get; }

    }
    public interface IDecoderValue<out T> : IDecoderValue
    {
        T Value { get; }
    }
}