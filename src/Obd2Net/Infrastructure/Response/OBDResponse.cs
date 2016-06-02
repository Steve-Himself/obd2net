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
        internal OBDResponse(IOBDCommand<T> command = null, params IMessage[] messages)
        {
            Command = command;
            Messages = messages ?? new IMessage[0];
        }

        public IOBDCommand<T> Command { get; }
        public IMessage[] Messages { get; }
        public T Value { get; set; }
        public Unit Unit { get; set; }
        public DateTime Time { get; set; }
        public override string ToString()
        {
            return $"{Value} {Unit.GetDescription()}";
        }
    }
}