using System;

namespace Obd2Net
{
    public interface IOBDCommand
    {
    }

    public class OBDCommand<T> : IOBDCommand
    {
        public OBDCommand(string name, string description, string command, int bytes, Func<IMessage[], DecoderValue<T>> decoder, ECU ecu = ECU.All, bool fast = false)
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
        public Func<IMessage[], DecoderValue<T>> Decoder { get; set; }
        public ECU Ecu { get; set; }
        public bool Fast { get; set; }
    }
}