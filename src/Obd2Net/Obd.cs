using System.Collections.Generic;
using System.Linq;
using Obd2Net.InfrastructureContracts;
using Obd2Net.InfrastructureContracts.Commands;
using Obd2Net.InfrastructureContracts.Enums;

namespace Obd2Net
{
    public sealed class Obd
    {
        private readonly object _syncLock = new object();
        private readonly ILogger _logger;
        private string _lastCommand;

        private IPort _port;
        private readonly ICommands _commands;
        private List<IOBDCommand> _supportedCommands = new List<IOBDCommand>();

        internal Obd(ILogger logger, IPort port, ICommands commands)
        {
            _logger = logger;
            _port = port;
            _commands = commands;

            Connect(); // initialize by connecting and loading sensors
            LoadCommands(); // try to load the car's supported commands
        }

        /// <summary>
        ///     returns the OBD connection status
        /// </summary>
        public OBDStatus Status => _port?.Status ?? OBDStatus.NotConnected;

        public IDecoderValue<TResult> Query<TResult>(IOBDCommand<TResult> cmd, bool force = false)
        {
            lock (_syncLock)
            {
                //primary API function. Sends commands to the car, and
                //protects against sending unsupported commands.

                if (Status == OBDStatus.NotConnected)
                {
                    _logger.Error("Query failed, no connection available");
                    return DecoderValue<TResult>.Empty;
                }

                if (!force && !Supports(cmd))
                {
                    _logger.Error($"'{cmd}' is not supported");
                    return DecoderValue<TResult>.Empty;
                }

                // send command and retrieve message
                _logger.Debug($"Sending command: {cmd}");
                var cmdString = BuildCommandString(cmd);
                var messages = _port.SendAndParse(cmdString);

                // if we're sending a new command, note it
                if (string.IsNullOrWhiteSpace(cmdString))
                    _lastCommand = cmdString;

                if (messages == null || messages.Length == 0)
                {
                    _logger.Error("No valid OBD Messages returned");
                    return DecoderValue<TResult>.Empty;
                }

                // Trim bytes based on cmd.Bytes size
                foreach (var message in messages)
                {
                    message.Data = message.Data.Take(cmd.Bytes).ToArray();
                }

                return cmd.Decoder(messages);
            }
        }

        public IDecoderValue<TResult> Query<TCommand, TResult>(bool force = false)
            where TCommand : IOBDCommand<TResult>
        {
            var cmd = _commands.Get<TCommand, TResult>();
            return Query(cmd, force);
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
            if (_port.Config.Fast && cmd.Fast)
            {
                cmdString += _port.Ecus.Count().ToString(); // TODO: ?? str(len(self.port.ecus()));
            }

            // if we sent this last time, just send
            if (_port.Config.Fast && cmdString == _lastCommand)
            {
                cmdString = "";
            }

            return cmdString;
        }

        /// <summary>
        ///     Attempts to instantiate an ELM327 connection object.
        /// </summary>
        private void Connect()
        {
            lock (_syncLock)
            {
                _port.Connect();
                // if the connection failed, close it
                if (_port.Status == OBDStatus.NotConnected)
                {
                    // the ELM327 class will report its own errors
                    Close();
                }
            }
        }

        /// <summary>
        ///     Closes the connection, and clears supported_commands
        /// </summary>
        private void Close()
        {
            lock (_syncLock)
            {
                _supportedCommands = new List<IOBDCommand>();

                if (_port != null)
                {
                    _logger.Info("Closing connection");
                    _port.Close();
                    _port = null;
                }
            }
        }

        /// <summary>
        ///     Queries for available PIDs, sets their support status, and compiles a list of command objects.
        /// </summary>
        private void LoadCommands()
        {
            lock (_syncLock)
            {

                if (Status != OBDStatus.CarConnected)
                {
                    _logger.Debug("Cannot load commands: No connection to car");
                    return;
                }

                _logger.Debug("querying for supported PIDs (commands)...");
                var pidGetters = _commands.PidGetters();
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

                            if (_commands.HasPid(mode, pid))
                            {
                                _supportedCommands.Add(_commands.Get(mode, pid));
                            }

                            // set support for mode 2 commands
                            if (mode == 1 && _commands.HasPid(2, pid))
                            {
                                _supportedCommands.Add(_commands.Get(2, pid));
                            }
                        }
                    }
                }
                _logger.Debug($"finished querying with {_supportedCommands.Count} commands supported");
            }
        }
    }
}