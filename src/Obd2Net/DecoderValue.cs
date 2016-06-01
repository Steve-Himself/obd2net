using Obd2Net.InfrastructureContracts;
using Obd2Net.InfrastructureContracts.Enums;

namespace Obd2Net
{
    public class DecoderValue<T> : IDecoderValue<T>
    {
        public DecoderValue(T value, Unit unit)
        {
            Value = value;
            Unit = unit;
        }

        public T Value { get; private set; }
        public Unit Unit { get; private set; }
    }
}