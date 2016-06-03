using System;
using Obd2Net.InfrastructureContracts;
using Obd2Net.InfrastructureContracts.Commands;
using Obd2Net.InfrastructureContracts.Enums;

namespace Obd2Net.Infrastructure.Commands
{
    public sealed class Speed : BaseCommand<uint>
    {
        public override string Command => "010D";
        public override int Bytes => 1;
        public override ECU Ecu => ECU.Engine;
        public override bool Fast => true;
        public override Func<IMessage[], IDecoderValue<uint>> Decoder => Decoders.Speed;
    }
}
