using System;
using System.Collections.Generic;
using System.Diagnostics;

namespace ConsoleApplication7
{
    public static class Utility
    {
        public static void ForEach<T>(this IEnumerable<T> ienumerable, Action<T> action)
        {
            foreach (T item in ienumerable)
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