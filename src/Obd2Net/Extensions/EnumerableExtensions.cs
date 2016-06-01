using System;
using System.Collections.Generic;

namespace Obd2Net.Extensions
{
    internal static class EnumerableExtensions
    {
        internal static IEnumerable<T> Do<T>(this IEnumerable<T> items, Action<T> action)
        {
            foreach (var item in items)
            {
                action(item);
                yield return item;
            }
        }

        internal static void Done<T>(this IEnumerable<T> items)
        {
            // just force enumeration so that any chained .Do(...) calls are executed
            var enumerator = items.GetEnumerator();
            while (enumerator.MoveNext())
            {
            }
        }
    }
}