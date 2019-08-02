using System;
using System.Collections.Generic;

namespace TuiEmulator.Providers.Extensions
{
    public static class EnumerableExtensions
    {
        public static IEnumerable<TType> Uniq<TType, TProperty>(this IEnumerable<TType> collection,
            Func<TType, TProperty> getProperty)
        {
            var hs = new HashSet<TProperty>();

            foreach (var item in collection)
                if (hs.Add(getProperty(item)))
                    yield return item;
        }
    }
}