using System;
using Obd2Net.InfrastructureContracts.Commands;
using Obd2Net.InfrastructureContracts.Enums;
using Obd2Net.InfrastructureContracts.Protocols;
using Obd2Net.InfrastructureContracts.Response;

namespace Obd2Net.Infrastructure
{
    internal class OBDCommand<T> : IOBDCommand<T>
    {
        internal OBDCommand(string name, string description, string command, int bytes, Func<IMessage[], IOBDResponse<T>> decoder, ECU ecu = ECU.All, bool fast = false)
        {
            Name = name;
            Description = description;
            Command = command;
            Bytes = bytes;
            Decoder = decoder;
            Ecu = ecu;
            Fast = fast;
        }

        public string Name { get; set; }
        public string Description { get; set; }
        public string Command { get; set; }
        public int Bytes { get; set; }
        public Func<IMessage[], IOBDResponse<T>> Decoder { get; set; }
        public ECU Ecu { get; set; }
        public bool Fast { get; set; }

        public int Mode => Utils.Unhex(Command, 0, 2);

        public int Pid => Utils.Unhex(Command, 2);

        public IOBDResponse<T> Execute(IMessage[] messages)
        {
            return null;
        }
    }
}