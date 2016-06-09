using Obd2Net.InfrastructureContracts;
using Obd2Net.InfrastructureContracts.Enums;

namespace Obd2Net.Commands
{
    public abstract class OBDCommandBase<TDecoder, TResult> where TDecoder : IDecoder<TResult>
    {
        protected OBDCommandBase(TDecoder decoder, Mode mode, string name, string description, string command, int bytes, ECU ecu, bool fast)
        {
            Decoder = decoder;
            Name = name;
            Description = description;
            Command = command;
            Bytes = bytes;
            Ecu = ecu;
            Fast = fast;
            Mode = mode;
        }

        public virtual string Name { get; private set; }
        public virtual string Description { get; private set; }
        public virtual string Command { get; private set; }
        public virtual int Bytes { get; private set; }
        public TDecoder Decoder { get; private set; }
        public virtual ECU Ecu { get; private set; }
        public virtual bool Fast { get; private set; }
        public virtual Mode Mode { get; private set; }
    }
}