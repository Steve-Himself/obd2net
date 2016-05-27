namespace Obd2Net
{
    public interface IFrame
    {
        string Raw { get; set; }
        byte[] Data { get; set; }
        int Priority { get; set; }
        int AddrMode { get; set; }
        int RxId { get; set; }
        int TxId { get; set; }
        int Type { get; set; }
        int SeqIndex { get; set; }
        int DataLen { get; set; }
    }

    internal class Frame : IFrame
    {
        public Frame(string raw)
        {
            Raw = raw;
        }

        public string Raw       {get;set;}// raw
        public byte[] Data      {get;set;}// bytearray()
        public int Priority { get; set; }// None
        public int AddrMode { get; set; }// None
        public int RxId { get; set; }// None
        public int TxId { get; set; }// None
        public int Type { get; set; }// None
        public int SeqIndex { get; set; }// 0 # only used when type = CF
        public int DataLen { get; set; }// None
    }
}
