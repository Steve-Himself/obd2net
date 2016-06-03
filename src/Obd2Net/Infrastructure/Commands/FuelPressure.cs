using System;
using Obd2Net.InfrastructureContracts;
using Obd2Net.InfrastructureContracts.Enums;

namespace Obd2Net.Infrastructure.Commands
{
    public sealed class FuelPressure : BaseCommand<uint>
    {
        public override string Command => "010A";
        public override int Bytes => 1;
        public override ECU Ecu => ECU.Engine;
        public override bool Fast => true;
        public override Func<IMessage[], IDecoderValue<uint>> Decoder => Decoders.Fuel_pressure;
    }
}