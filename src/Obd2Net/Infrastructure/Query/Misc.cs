using System.Collections.Generic;
using Obd2Net.Infrastructure.Commands;
using Obd2Net.InfrastructureContracts.Commands;
using Obd2Net.InfrastructureContracts.Enums;

namespace Obd2Net.Infrastructure.Query
{
    public class Misc
    {
        public Misc()
        {
            Commands = new List<IOBDCommand>
            {
                ElmVersion,
                ElmVoltage
            };
        }
        public IEnumerable<IOBDCommand> Commands { get; private set; }

        public IOBDCommand<string> ElmVersion { get; } = new OBDCommand<string>("ELM_VERSION", "ELM327 version string", "ATI", 0, Decoders.RawString, ECU.Unknown, false);
        public IOBDCommand<decimal?> ElmVoltage { get; } = new OBDCommand<decimal?>("ELM_VOLTAGE", "Voltage detected by OBD-II adapter", "ATRV", 0, Decoders.Elm_voltage, ECU.Unknown, false);

    }
}