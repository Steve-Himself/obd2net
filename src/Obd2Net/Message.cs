using System;
using System.Collections.Generic;
using System.Linq;

namespace Obd2Net
{
    public interface IMessage
    {
        ECU Ecu { get; set; }
        IFrame[] Frames { get; set; }
        byte[] Data { get; set; }
        string Hex { get; }
        string Raw { get; }
        int? TxId { get; }
        bool Parsed { get; }
    }

    internal class Message : IMessage, IEquatable<Message>
    {
        public Message(params IFrame[] frames)
            : this(ECU.Unknown, new byte[0], frames)
        {
        }

        public Message(ECU ecu, byte[] data, params IFrame[] frames)
        {
            Frames = frames;
            Ecu = ecu;
            Data = data;
        }

        internal static IMessage FromHexString(string str)
        {
            var hexindex = new Dictionary<string, byte>();
            for (var i = 0; i <= 255; i++)
                hexindex.Add(i.ToString("X2"), (byte)i);

            var hexres = new List<byte>();
            for (var i = 0; i < str.Length; i += 2)
                hexres.Add(hexindex[str.Substring(i, 2)]);

            return new Message(ECU.Unknown, hexres.ToArray());
        }

        public ECU Ecu { get; set; }
        public IFrame[] Frames { get; set; }
        public byte[] Data { get; set; }

        public string Hex { get { return BitConverter.ToString(Data).Replace("-", string.Empty); } }
        public string Raw { get { return string.Join("\n", Frames.Select(f => f.Raw).ToArray()); } }
        public int? TxId
        {
            get
            {
                if (Frames == null || !Frames.Any())
                    return null;
                return Frames[0].TxId;
            }
        }
        public bool Parsed { get { return Data != null; } }

        public bool Equals(Message other)
        {
            if (ReferenceEquals(null, other)) return false;
            if (ReferenceEquals(this, other)) return true;
            return Ecu == other.Ecu && Equals(Frames, other.Frames) && Equals(Data, other.Data);
        }

        public override bool Equals(object obj)
        {
            if (ReferenceEquals(null, obj)) return false;
            if (ReferenceEquals(this, obj)) return true;
            if (obj.GetType() != this.GetType()) return false;
            return Equals((Message) obj);
        }

        public override int GetHashCode()
        {
            unchecked
            {
                var hashCode = (int) Ecu;
                hashCode = (hashCode*397) ^ (Frames != null ? Frames.GetHashCode() : 0);
                hashCode = (hashCode*397) ^ (Data != null ? Data.GetHashCode() : 0);
                return hashCode;
            }
        }
    }
}
