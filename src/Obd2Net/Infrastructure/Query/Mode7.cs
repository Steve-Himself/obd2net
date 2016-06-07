using System.Collections.Generic;
using Obd2Net.InfrastructureContracts.Commands;
using Obd2Net.InfrastructureContracts.Enums;

namespace Obd2Net.Infrastructure.Query
{
    public class Mode7
    {
        public Mode7()
        {
            Commands = new List<IOBDCommand>
            {
                GetFreezeDtc
            };
        }
        public IEnumerable<IOBDCommand> Commands { get; private set; }

        public IOBDCommand<IDictionary<string, string>> GetFreezeDtc { get; } = new OBDCommand<IDictionary<string, string>>("GET_FREEZE_DTC", "Get Freeze DTCs", "07", 0, Decoders.DTC, ECU.All, false);
    }
}