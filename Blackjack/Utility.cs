using System;
using System.Collections.Generic;

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
    }
}