using System;
using System.Collections.Generic;
using Obd2Net.Infrastructure.Commands;

namespace Obd2Net
{
    public class Commands : List<Dictionary<int, IOBDCommand>>
    {
        public List<IOBDCommand> BaseCommands()
        {
            throw new NotImplementedException();
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
    }
}