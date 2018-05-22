using System;
using System.Collections.Generic;
using System.Linq;

namespace ChattingApp.Repository.Helpers
{
    public static class EnumerableExtensions
    {
        public static IEnumerable<T> Except<T, TKey>(this IEnumerable<T> items, IEnumerable<T> other,
            Func<T, TKey> getKey)
        {
            if (items == null) throw new ArgumentNullException(nameof(items));
            if (other == null) throw new ArgumentNullException(nameof(getKey));
            if (getKey == null) throw new ArgumentNullException(nameof(getKey));

            return items
                .GroupJoin(other, getKey, getKey,
                    (item, tempItems) => new { item, tempItems })
                .SelectMany(t => t.tempItems.DefaultIfEmpty(), (t, temp) => new { t, temp })
                .Where(t => ReferenceEquals(null, t.temp) || t.temp.Equals(default(T)))
                .Select(t => t.t.item);
        }
    }
}