using System.Collections.Generic;
using System.IO.Ports;
using System.Linq;
using Obd2Net.App_Packages.LibLog._4._2;
using Obd2Net.Infrastructure.Commands;
using Obd2Net.Infrastructure.Response;
using Obd2Net.InfrastructureContracts;
using Obd2Net.InfrastructureContracts.Enums;
using Obd2Net.Ports;

namespace Obd2Net
{
    public sealed class Obd
    {
        private static readonly ILog Logger = LogProvider.For<Obd>();
        private readonly bool _fast;
        private string _lastCommand;

        private IPort _port;
        private List<IOBDCommand> _supportedCommands;

        internal Obd(string portstr = null, int baudrate = 38400, string protocol = null, bool fast = true)
        {
            Commands = new Commands();
            _supportedCommands = Commands.BaseCommands();
            _fast = fast;

            Connect(portstr, baudrate, protocol); // initialize by connecting and loading sensors
            LoadCommands(); // try to load the car's supported commands
        }

        public Commands Commands { get; }

        /// <summary>
        ///     returns the OBD connection status
        /// </summary>
        public OBDStatus Status => _port?.Status ?? OBDStatus.NotConnected;

        public IOBDResponse<T> Query<T>(IOBDCommand<T> cmd, bool force = false)
        {
            using (LogProvider.OpenNestedContext("Query"))
            using (LogProvider.OpenMappedContext(cmd.Pid.ToString(), cmd.Description))
            {
                //primary API function. Sends commands to the car, and
                //protects against sending unsupported commands.

                if (Status == OBDStatus.NotConnected)
                {
                    Logger.Error("Query failed, no connection available");
                    return new OBDResponse<T>();
                }

                if (!force && !Supports(cmd))
                {
                    Logger.Error($"'{cmd}' is not supported");
                    return new OBDResponse<T>();
                }

                // send command and retrieve message
                Logger.Debug($"Sending command: {cmd}");
                var cmdString = BuildCommandString(cmd);
                var messages = _port.SendAndParse(cmdString);

                // if we're sending a new command, note it
                if (string.IsNullOrWhiteSpace(cmdString))
                    _lastCommand = cmdString;

                if (messages == null || messages.Length == 0)
                {
                    Logger.Error("No valid OBD Messages returned");
                    return new OBDResponse<T>();
                }
                return cmd.Execute(messages); // compute a response object
            }
        }

        public bool Supports(IOBDCommand cmd)
        {
            return _supportedCommands.Contains(cmd);
        }

        /// <summary>
        ///     assembles the appropriate command string
        /// </summary>
        private string BuildCommandString(IOBDCommand cmd)
        {
            var cmdString = cmd.Command;

            // only wait for as many ECUs as we've seen
            if (_fast && cmd.Fast)
            {
                cmdString += _port.Ecus.Length.ToString(); // TODO: ?? str(len(self.port.ecus()));
            }

            // if we sent this last time, just send
            if (_fast && cmdString == _lastCommand)
            {
                cmdString = "";
            }

            return cmdString;
        }

        /// <summary>
        ///     Attempts to instantiate an ELM327 connection object.
        /// </summary>
        private void Connect(string portstr, int baudrate, string protocol)
        {
            if (string.IsNullOrWhiteSpace(portstr))
            {
                Logger.Debug("Using scan_serial to select port");
                var portnames = SerialPort.GetPortNames();
                Logger.Debug("Available ports: " + string.Join(",", portnames));

                if (!portnames.Any())
                {
                    Logger.Debug("No OBD-II adapters found");
                    return;
                }

                foreach (var p in portnames)
                {
                    Logger.Debug($"Attempting to use port: {p}");
                    _port = new Elm327(p, baudrate, protocol);

                    if (_port.Status >= OBDStatus.ElmConnected)
                        break; // success! stop searching for serial
                }
            }
            else
            {
                Logger.Debug("Explicit port defined");
                _port = new Elm327(portstr, baudrate, protocol);
            }
            // if the connection failed, close it
            if (_port.Status == OBDStatus.NotConnected)
            {
                // the ELM327 class will report its own errors
                Close();
            }
        }

        /// <summary>
        ///     Closes the connection, and clears supported_commands
        /// </summary>
        private void Close()
        {
            _supportedCommands = new List<IOBDCommand>();

            if (_port != null)
            {
                Logger.Info("Closing connection");
                _port.Close();
                _port = null;
            }
        }

        /// <summary>
        ///     Queries for available PIDs, sets their support status, and compiles a list of command objects.
        /// </summary>
        private void LoadCommands()
        {
            if (Status != OBDStatus.CarConnected)
            {
                Logger.Debug("Cannot load commands: No connection to car");
                return;
            }

            Logger.Debug("querying for supported PIDs (commands)...");
            var pidGetters = Commands.PidGetters();
            foreach (var get in pidGetters)
            {
                // PID listing commands should sequentialy become supported
                // Mode 1 PID 0 is assumed to always be supported
                if (!Supports(get)) continue;

                // when querying, only use the blocking OBD.query()
                // prevents problems when query is redefined in a subclass (like Async)
                var response = Query(get, true); // ask nicely

                if (response == null) continue;

                var supported = response.Value; // string of binary 01010101010101

                // loop through PIDs binary
                for (var i = 0; i < supported.Length; i++)
                {
                    if (supported[i] == '1')
                    {
                        var mode = get.Mode;
                        var pid = get.Pid + i + 1;

                        if (Commands.HasPid(mode, pid))
                        {
                            _supportedCommands.Add(Commands[mode][pid]);
                        }

                        // set support for mode 2 commands
                        if (mode == 1 && Commands.HasPid(2, pid))
                        {
                            _supportedCommands.Add(Commands[2][pid]);
                        }
                    }
                }
            }
            Logger.Debug($"finished querying with {_supportedCommands.Count} commands supported");
        }
    }
}