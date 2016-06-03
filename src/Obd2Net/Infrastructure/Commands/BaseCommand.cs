using System;
using Obd2Net.InfrastructureContracts;
using Obd2Net.InfrastructureContracts.Commands;
using Obd2Net.InfrastructureContracts.Enums;

namespace Obd2Net.Infrastructure.Commands
{
    public abstract class BaseCommand<T> : IOBDCommand<T>
    {
        public virtual string Name => nameof(BaseCommand<T>);
        //public virtual string Description => nameof(BaseCommand<T>);
        public abstract string Command { get; }
        public abstract int Bytes { get; }
        public abstract ECU Ecu { get; }
        public abstract bool Fast { get; }
        public virtual int Mode => Utils.Unhex(Command, 0, 2);
        public virtual int Pid => Utils.Unhex(Command, 2, 2);
        public abstract Func<IMessage[], IDecoderValue<T>> Decoder { get; }
    }
}