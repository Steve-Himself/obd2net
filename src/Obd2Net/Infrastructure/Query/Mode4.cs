using System.Collections.Generic;
using Obd2Net.InfrastructureContracts.Commands;
using Obd2Net.InfrastructureContracts.Enums;

namespace Obd2Net.Infrastructure.Query
{
    public class Mode4
    {
        public Mode4()
        {
            Commands = new List<IOBDCommand>
            {
                ClearDtc
            };
        }
        public IEnumerable<IOBDCommand> Commands { get; private set; }

        public IOBDCommand<string> ClearDtc { get; } = new OBDCommand<string>("CLEAR_DTC", "Clear DTCs and Freeze data", "04", 0, OldDecoders.Drop, ECU.All, false);
    }
}