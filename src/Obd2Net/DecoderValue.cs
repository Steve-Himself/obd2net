using Obd2Net.Extensions;
using Obd2Net.InfrastructureContracts;
using Obd2Net.InfrastructureContracts.Enums;

namespace Obd2Net
{
    public class DecoderValue<T> : IDecoderValue<T>
    {
        public static DecoderValue<T> Empty { get; } = new DecoderValue<T>(default(T), Unit.None);

        public DecoderValue(T value, Unit unit)
        {
            Value = value;
            Unit = unit;
        }

        public T Value { get; }
        public Unit Unit { get; }
        public object Raw => Value;

        public override string ToString()
        {
            return $"{Value} {Unit.GetDescription()}";
        }
    }
}