using System;
using System.Collections.Generic;
using System.Linq;

namespace Hotel.Domain.Extensions
{
    public static class CollectionExtensions
    {
        public static IEnumerable<T> GetUnique<T, T1, T2>(this IEnumerable<T> col1, IEnumerable<T1> col2, Func<T, T2> col1Prop, Func<T1, T2> col2Prop)
        {
            if (!col2.Any())
                return col1;

            return (from entry1 in col1
                    join entry2 in col2 on col1Prop(entry1) equals col2Prop(entry2)
                    into du
                    from ud in du.DefaultIfEmpty()
                    where ud == null || ud.Equals(default(T2))
                    select entry1);
        }

        public static IEnumerable<T> GetSame<T, T1, T2>(this IEnumerable<T> col1, IEnumerable<T1> col2, Func<T, T2> col1Prop, Func<T1, T2> col2Prop)
        {
            return (from entry1 in col1
                    join entry2 in col2 on col1Prop(entry1) equals col2Prop(entry2)
                    into du
                    from ud in du.DefaultIfEmpty()
                    where ud != null && !ud.Equals(default(T2))
                    select entry1);
        }

        public static IQueryable<T> Pagging<T>(this IQueryable<T> collection, int page, int limit)
        {
            return collection.Skip((page - 1) * limit).Take(limit);
        }
    }
}
