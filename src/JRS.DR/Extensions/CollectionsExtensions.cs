using System;
using System.Collections.Generic;
using System.Linq;

namespace JRS.DR.Extensions
{
    public static class CollectionsExtensions
    {
        public static void ForEach<T>(this IEnumerable<T> collection, Action<T> action)
        {
            collection.ToList().ForEach(action);
        }
    }
}