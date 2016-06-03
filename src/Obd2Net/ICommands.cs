using System.Collections.Generic;
using Obd2Net.InfrastructureContracts.Commands;

namespace Obd2Net
{
    public interface ICommands
    {
        bool HasPid(int mode, int pid);
        IEnumerable<IOBDCommand<string>> PidGetters();
        IOBDCommand Get(int mode, int pid);
        IOBDCommand<TResult> Get<TCommand, TResult>() where TCommand : IOBDCommand<TResult>;
    }
}