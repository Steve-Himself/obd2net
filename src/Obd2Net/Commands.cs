using System.Collections.Generic;
using System.Linq;
using Obd2Net.InfrastructureContracts.Commands;
using Obd2Net.InfrastructureContracts.Enums;

namespace Obd2Net
{
    public class Commands : ICommands
    {
        private readonly IEnumerable<IOBDCommand> _commands;

        public Commands(IEnumerable<IOBDCommand> commands)
        {
            _commands = commands;
        }

        public IEnumerable<IOBDCommand<string>> PidGetters()
        {
            return _commands.OfType<IPidOBDCommand>().OfType<IOBDCommand<string>>();
        }

        public IOBDCommand Get(int mode, int pid)
        {
            return _commands.FirstOrDefault(c => c.Mode == mode && c.Pid == pid);
        }

        public IOBDCommand<TResult> Get<TCommand, TResult>() where TCommand : IOBDCommand<TResult>
        {
            return _commands.OfType<TCommand>().FirstOrDefault();
        }

        public bool HasPid(int mode, int pid)
        {
            return _commands.Any(c => c.Mode == mode && c.Pid == pid);
        }       
    }
}