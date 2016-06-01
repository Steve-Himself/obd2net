using System;
using System.Collections.Generic;
using System.IO.Ports;
using System.Linq;
using System.Threading;
using Obd2Net.App_Packages.LibLog._4._2;
using Obd2Net.InfrastructureContracts;
using Obd2Net.InfrastructureContracts.Enums;
using Obd2Net.InfrastructureContracts.Protocols;
using Obd2Net.Protocols;
using Obd2Net.Protocols.Can;
using Obd2Net.Protocols.Legacy;

namespace Obd2Net.Ports
{
    internal class Elm327 : IPort
    {
        private static readonly ILog Logger = LogProvider.For<Elm327>();

        private static readonly Dictionary<string, Type> SupportedProtocols = new Dictionary<string, Type>
        {
            {"0", null},
            {"1", typeof(SAE_J1850_PWM)},
            {"2", typeof(SAE_J1850_VPW)},
            {"3", typeof(ISO_9141_2)},
            {"4", typeof(ISO_14230_4_5baud)},
            {"5", typeof(ISO_14230_4_fast)},
            {"6", typeof(ISO_15765_4_11bit_500k)},
            {"7", typeof(ISO_15765_4_29bit_500k)},
            {"8", typeof(ISO_15765_4_11bit_250k)},
            {"9", typeof(ISO_15765_4_29bit_250k)},
            {"A", typeof(SAE_J1939)}
        };

        private static readonly string[] TryProtocolOrder =
        {
            "6", // ISO_15765_4_11bit_500k
            "8", // ISO_15765_4_11bit_250k
            "1", // SAE_J1850_PWM
            "7", // ISO_15765_4_29bit_500k
            "9", // ISO_15765_4_29bit_250k
            "2", // SAE_J1850_VPW
            "3", // ISO_9141_2
            "4", // ISO_14230_4_5baud
            "5", // ISO_14230_4_fast
            "A" // SAE_J1939
        };

        private SerialPort _port;

        public Elm327(string portname, int baudrate, string protocol)
        {
            PortName = portname;
            Protocol = new UnknownProtocol();
            _port = new SerialPort(portname, baudrate);

            // ------------- open port -------------
            try
            {
                Logger.Debug($"Opening serial port '{portname}'");
                _port = new SerialPort(portname, baudrate, Parity.None)
                {
                    StopBits = StopBits.One,
                    ReadTimeout = 3000,
                    WriteTimeout = 3000
                };
                _port.Open();
                Logger.Debug($"Serial port successfully opened on {PortName}");
            }
            catch (Exception e)
            {
                Error(e.Message);
                return;
            }

            // ---------------------------- ATZ (reset) ----------------------------
            try
            {
                Send("ATZ", TimeSpan.FromSeconds(1)); // wait 1 second for ELM to initialize
            }
            catch (Exception e)
            {
                Error(e.Message);
                return;
            }

            // -------------------------- ATE0 (echo OFF) --------------------------
            var r = Send("ATE0");
            if (!IsOk(r, true))
            {
                Error("ATE0 did not return 'OK'");
                return;
            }

            // ------------------------- ATH1 (headers ON) -------------------------
            r = Send("ATH1");
            if (!IsOk(r))
            {
                Error("ATH1 did not return 'OK', or echoing is still ON");
                return;
            }
            // ------------------------ ATL0 (linefeeds OFF) -----------------------
            r = Send("ATL0");
            if (!IsOk(r))
            {
                Error("ATL0 did not return 'OK'");
                return;
            }

            // by now, we've successfuly communicated with the ELM, but not the car
            Status = OBDStatus.ElmConnected;

            // try to communicate with the car, and load the correct protocol parser
            if (LoadProtocol(protocol))
            {
                Status = OBDStatus.CarConnected;
                Logger.Info("Connection successful");
            }
            else
            {
                Logger.Info("Connected to the adapter, but failed to connect to the vehicle");
            }
        }

        public IProtocol Protocol { get; private set; }

        public OBDStatus Status { get; private set; }
        public string PortName { get; }
        public ECU[] Ecus { get; }

        public string ProtocolName => Protocol.ElmName;

        public string ProtocolId => Protocol.ElmId;

        /// <summary>
        ///     Resets the device, and sets all attributes to unconnected states.
        /// </summary>
        public void Close()
        {
            Status = OBDStatus.NotConnected;
            Protocol = null;

            if (_port != null)
            {
                Write("ATZ");
                _port.Close();
                _port = null;
            }
        }

        public IMessage[] SendAndParse(string cmd)
        {
            if (Status == OBDStatus.NotConnected)
            {
                Logger.Debug("cannot send_and_parse() when unconnected");
                return null;
            }

            var lines = Send(cmd);
            var messages = Protocol.Parse(lines);
            return messages;
        }

        public string[] Send(string cmd, TimeSpan? delay = null)
        {
            Write(cmd);

            if (delay.HasValue)
            {
                Logger.Debug($"wait: {delay.Value.TotalMilliseconds} Milliseconds");
                Thread.Sleep(delay.Value);
            }

            return Read();
        }

        private bool LoadProtocol(string protocol)
        {
            if (protocol != null)
            {
                // an explicit protocol was specified
                if (SupportedProtocols.ContainsKey(protocol))
                {
                    Logger.Error($"{protocol} is not a valid protocol. Please use \"1\" through \"A\"");
                    return false;
                }
                return manual_protocol(protocol);
            }

            //  auto detect the protocol
            return auto_protocol();
        }

        private bool auto_protocol()
        {
            throw new NotImplementedException();
        }

        private bool manual_protocol(string protocol)
        {
            var r = Send($"ATTP{protocol}");
            var r0100 = Send("0100");

            if (!r0100.Any(m => m.Contains("UNABLE TO CONNECT")))
            {
                // success, found the protocol
                var protocolType = SupportedProtocols[protocol];
                Protocol = CreateProtocol(protocolType, r0100);
                return true;
            }
            return false;
        }

        private IProtocol CreateProtocol(Type protocolType, string[] r0100)
        {
            throw new NotImplementedException();
        }

        private bool IsOk(string[] lines, bool expectEcho = false)
        {
            if (lines == null || lines.Length == 0)
                return false;

            if (expectEcho)
            {
                //# don't test for the echo itself
                //# allow the adapter to already have echo disabled
                return lines.Any(l => l.Contains("OK"));
            }

            return lines.Length == 1 && lines[0] == "OK";
        }

        private void Error(string msg)
        {
            Close();

            Logger.Debug("Connection Error:");
            if (!string.IsNullOrWhiteSpace(msg))
                Logger.Debug(msg);
        }

        /// <summary>
        ///     "low-level" function to write a string to the port
        /// </summary>
        /// <param name="cmd"></param>
        private void Write(string cmd)
        {
            if (_port != null)
            {
                cmd += "\r\n"; // terminate
                Logger.Debug("write: " + cmd);
                _port.DiscardInBuffer(); // dump everything in the input buffer
                var buffer = Utils.GetBytes(cmd);
                _port.Write(buffer, 0, buffer.Length); // turn the string into bytes and write
            }
            else
                Logger.Debug("cannot perform Write() when unconnected");
        }

        /// <summary>
        ///     low-level read function - accumulates characters until the prompt character is seen returns a list of [/r/n]
        ///     delimited strings
        /// </summary>
        private string[] Read()
        {
            var attempts = 2;
            var buffer = new List<byte>();

            if (_port != null)
                while (true)
                {
                    byte? c = null;
                    try
                    {
                        c = (byte) _port.ReadByte();
                    }
                    catch (TimeoutException)
                    {
                    }
                    // if nothing was recieved
                    if (!c.HasValue)
                    {
                        if (attempts <= 0)
                        {
                            Logger.Debug("Failed to read port, giving up");
                            break;
                        }

                        Logger.Debug("Failed to read port, trying again...");
                        attempts -= 1;
                        continue;
                    }
                    // end on chevron (ELM prompt character)
                    if (c == '>')
                        break;

                    // skip null characters (ELM spec page 9)
                    if (c == '\x00')
                        continue;

                    buffer.Add(c.Value); // whatever is left must be part of the response
                }
            else
            {
                Logger.Debug("cannot perform Read() when unconnected");
                return new string[0];
            }

            Logger.Debug($"read: {buffer.Count} bytes");

            // convert bytes into a standard string
            var chars = new char[buffer.Count/sizeof(char)];
            Buffer.BlockCopy(buffer.ToArray(), 0, chars, 0, buffer.Count);
            var raw = new string(chars);

            // splits into lines
            // removes empty lines
            // removes trailing spaces
            return raw.Split(new[] {"\r\n"}, StringSplitOptions.RemoveEmptyEntries);
        }
    }
}