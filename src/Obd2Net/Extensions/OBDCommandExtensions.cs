using Obd2Net.InfrastructureContracts.Commands;

namespace Obd2Net.Extensions
{
    internal static class OBDCommandExtensions
    {
        public static int Pid(this IOBDCommand obdCommand)
        {
            return Utils.Unhex(obdCommand.Command, 2);
        }

    }
}
