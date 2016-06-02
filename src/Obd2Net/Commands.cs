using System.Collections.Generic;
using Obd2Net.Infrastructure.Commands;
using Obd2Net.InfrastructureContracts.Commands;
using Obd2Net.InfrastructureContracts.Enums;

namespace Obd2Net
{
    public class Commands : List<Dictionary<int, IOBDCommand>>
    {
        public List<IOBDCommand> BaseCommands()
        {
            return new List<IOBDCommand>();
        }

        public IEnumerable<IOBDCommand<string>> PidGetters()
        {
            return new List<IOBDCommand<string>>();
        }

        public bool HasPid(int mode, int pid)
        {
            if (mode < 0 || pid < 0)
                return false;
            if (mode >= Count)
                return false;
            if (pid >= this[mode].Count)
                return false;
            return true;
        }

        public Mode01 Mode1 { get; } = new Mode01();
        public Mode02 Mode2 { get; } = new Mode02();

        public class Mode01
        {
            protected virtual string Mode => "01";

            public IOBDCommand<decimal> Speed => new OBDCommand<decimal>("Speed", "Speed", $"{Mode}0D", 1, Decoders.Speed, ECU.Engine, true);
        }

        public class Mode02 : Mode01
        {
            protected override string Mode => "02";
        }

    }
}