using Obd2Net.InfrastructureContracts.Enums;

namespace Obd2Net.InfrastructureContracts
{
    public interface IDecoderValue<T>
    {
        Unit Unit { get; }
        T Value { get; }
    }
}