using System;
using System.Collections;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;

namespace Obd2Net
{
    public static class Utils
    {
        private const string HexSeperator = "-";

        private static readonly Regex IsHexMatch = new Regex(@"\A\b[0-9a-fA-F]+\b\Z");

        public static long TwosComp(long val, int numBits)
        {
            if ((val & (1 << (numBits - 1))) != 0)
                val = val - (1 << numBits);
            return val;
        }

        /// <summary>
        ///     converts a big-endian byte array into a single integer
        /// </summary>
        public static int BytesToInt(byte[] bytes)
        {
            return int.Parse(BitConverter.ToString(bytes, 0).Replace(HexSeperator, string.Empty), NumberStyles.AllowHexSpecifier);
        }

        public static int BytesToInt(byte[] bytes, int start, int? length = null)
        {
            length = (length ?? bytes.Length) - start;
            return BytesToInt(bytes.Skip(start).Take(length.Value).ToArray());
        }

        public static int SwapEndianness(int x)
        {
            return (int) (((x & 0x000000ff) << 24) + // First byte
                          ((x & 0x0000ff00) << 8) + // Second byte
                          ((x & 0x00ff0000) >> 8) + // Third byte
                          ((x & 0xff000000) >> 24)); // Fourth byte
        }

        // TODO: Need to prove this logic here
        public static int Unbin(BitArray bitArray, int start = 0, int? end = null)
        {
            var value = 0;

            for (var i = 0; i < bitArray.Count; i++)
            {
                if (i >= start && (end == null || i <= end.Value) && bitArray[i])
                    value += Convert.ToInt16(Math.Pow(2, i - start));
            }

            return value;
        }

        public static int Unhex(string hex, int start, int? length = null)
        {
            var s = length.HasValue ? hex.Substring(start, length.Value) : hex.Substring(start);
            return int.Parse(s, NumberStyles.HexNumber);
        }

        public static byte[] GetBytes(string str)
        {
            return Encoding.ASCII.GetBytes(str);
        }

        public static string GetString(byte[] bytes)
        {
            return Encoding.ASCII.GetString(bytes);
        }

        public static bool IsHex(string str)
        {
            return IsHexMatch.IsMatch(str);
        }
    }
}