using System;
using System.Collections.Generic;

namespace MP.Framework.Extensions
{
    public static class EnumerableExtensions
    {
        public static T GetSingleItem<T>(this IList<T> list, bool returnDefaultIfEmpty)
        {
            if (list.Count == 1)
            {
                return list[0];
            }
            else if (returnDefaultIfEmpty && list.Count == 0)
            {
                return default(T);
            }
            else
            {
                throw new ArgumentException($"Expected single {typeof(T)} in list but got {list.Count}.");
            }
        }
    }
}
