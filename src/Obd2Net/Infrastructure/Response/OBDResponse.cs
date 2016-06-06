using System;
using Obd2Net.Extensions;
using Obd2Net.InfrastructureContracts;
using Obd2Net.InfrastructureContracts.Commands;
using Obd2Net.InfrastructureContracts.Enums;
using Obd2Net.InfrastructureContracts.Response;

namespace Obd2Net.Infrastructure.Response
{
    internal class OBDResponse<T> : IOBDResponse<T>
    {
        internal static IOBDResponse<T> Empty { get; } = new OBDResponse<T>(default(T), Unit.None);

        internal OBDResponse(T value, Unit unit)
        {
            Value = value;
            Unit = unit;
        }

        public IOBDCommand Command { get; set; }
        public IMessage[] Messages { get; set; }
        public T Value { get; set; }
        public Unit Unit { get; set; }
        public DateTime Time { get; set; }
        public object Raw => Value;

        public override string ToString()
        {
            return $"{Command?.Name ?? "Empty"}: {Value} {Unit.GetDescription()}";
        }
    }
}