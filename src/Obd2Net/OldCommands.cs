using System.Collections.Generic;
using System.Linq;
using Obd2Net.Infrastructure;
using Obd2Net.Infrastructure.Query;
using Obd2Net.InfrastructureContracts.Commands;

namespace Obd2Net
{
    public static class OldCommands
    {
        public static Mode1 Mode1 { get; } = new Mode1();
        public static Mode2 Mode2 { get; } = new Mode2();
        public static Mode3 Mode3 { get; } = new Mode3();
        public static Mode4 Mode4 { get; } = new Mode4();
        public static Mode7 Mode7 { get; } = new Mode7();
        public static Misc Misc { get; } = new Misc();

        static OldCommands()
        {
            All.AddRange(Mode1.Commands);
            All.AddRange(Mode2.Commands);
            All.AddRange(Mode3.Commands);
            All.AddRange(Mode4.Commands);
            All.AddRange(Mode7.Commands);
            All.AddRange(Misc.Commands);
        }

        public static List<IOBDCommand> All { get; } = new List<IOBDCommand>();

        public static IOBDCommand[] BaseCommands => new IOBDCommand[]
        {
            Mode1.PidsA,
            Mode3.GetDtc,
            Mode4.ClearDtc,
            Mode7.GetFreezeDtc,
            Misc.ElmVersion,
            Misc.ElmVoltage
        };

        public static IEnumerable<IOBDCommand<string>> PidGetters()
        {
            return All.OfType<PidCommand>();
        }

        public static IOBDCommand Get(int mode, int pid)
        {
            return All.FirstOrDefault(c => c.Mode == mode && c.Pid == pid);
        }

        public static IOBDCommand<TResult> Get<TCommand, TResult>() where TCommand : IOBDCommand<TResult>
        {
            return All.OfType<TCommand>().FirstOrDefault();
        }

        public static bool HasPid(int mode, int pid)
        {
            return All.Any(c => c.Mode == mode && c.Pid == pid);
        }
    }
}