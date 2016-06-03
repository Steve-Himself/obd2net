using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using Obd2Net.Logging;

namespace Obd2Net.Extensions
{
    public static class StringExtensions
    {
        public static byte[] ToByteArray(this string str)
        {
            var hexindex = new Dictionary<string, byte>();
            for (var i = 0; i <= 255; i++)
                hexindex.Add(i.ToString("X2"), (byte) i);

            var hexres = new List<byte>();
            for (var i = 0; i < str.Length; i += 2)
                hexres.Add(hexindex[str.Substring(i, 2)]);

            return hexres.ToArray();
        }

        private static readonly Regex _regex = new Regex(@"{\S+?}", RegexOptions.Compiled | RegexOptions.ExplicitCapture);

        internal static string NormalizeToStringFormat(this string format)
        {
            var output = format;

            var matchCollection = _regex.Matches(format);
            var matches = matchCollection
                .Cast<Match>()
                .DistinctBy(m => m.Captures[0].Value)
                .ToArray();

            for (var i = 0; i < matches.Length; i++)
            {
                var match = matches[i];
                var replaceWith = $"{{{i}}}";
                output = output.Replace(match.Captures[0].Value, replaceWith);
            }

            return output;
        }
    }
}