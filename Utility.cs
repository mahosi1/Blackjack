using System;
using System.Collections.Generic;
using System.Diagnostics;

namespace Blackjack
{
    public static class Utility
    {
        public static void ForEach<T>(this IEnumerable<T> ienumerable, Action<T> action)
        {
            foreach (var item in ienumerable)
            {
                action(item);
            }
        }

        public static void WriteLine(string msg, params object[] vals)
        {
            Trace.WriteLine(string.Format(msg, vals));
        }
    }
}