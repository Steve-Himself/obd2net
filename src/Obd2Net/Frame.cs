using Obd2Net.InfrastructureContracts;

namespace Obd2Net
{
    internal class Frame : IFrame
    {
        internal Frame(string raw)
        {
            Raw = raw;
        }

        public string Raw { get; set; } // raw
        public byte[] Data { get; set; } // bytearray()
        public int Priority { get; set; } // None
        public int AddrMode { get; set; } // None
        public int RxId { get; set; } // None
        public int TxId { get; set; } // None
        public int Type { get; set; } // None
        public int SeqIndex { get; set; } // 0 # only used when type = CF
        public int DataLen { get; set; } // None
    }
}