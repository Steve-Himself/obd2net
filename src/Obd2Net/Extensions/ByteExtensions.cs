using System.Linq;

namespace Obd2Net.Extensions
{
    public static class ByteExtensions
    {
        public static int NumberOfSetBits(this byte[] bytes)
        {
            return bytes.Sum(b => NumberOfSetBits((int) b));
        }

        public static int NumberOfSetBits(this byte @byte)
        {
            return NumberOfSetBits((int) @byte);
        }

        public static int NumberOfSetBits(this int i)
        {
            i = i - ((i >> 1) & 0x55555555);
            i = (i & 0x33333333) + ((i >> 2) & 0x33333333);
            return (((i + (i >> 4)) & 0x0F0F0F0F) * 0x01010101) >> 24;
        }
    }
}
