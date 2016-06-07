using System.Collections.Generic;
using Obd2Net.InfrastructureContracts.Commands;
using Obd2Net.InfrastructureContracts.Enums;

namespace Obd2Net.Infrastructure.Query
{
    public class Mode3
    {
        public Mode3()
        {
            Commands = new List<IOBDCommand>
            {
                GetDtc
            };
        }
        public IEnumerable<IOBDCommand> Commands { get; private set; }

        public IOBDCommand<IDictionary<string,string>> GetDtc { get; } = new OBDCommand<IDictionary<string, string>>("GET_DTC", "Get DTCs", "03", 0, Decoders.DTC, ECU.All, false);
    }
}
