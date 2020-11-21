using System;
using System.Collections.Generic;
using System.Linq;

namespace ConsoleApp
{
    public static class CollectionExtensions
    {
        public static IEnumerable<T1> GetDistinct<T1, T, T2>(this IEnumerable<T1> setA, IEnumerable<T2> setB, Func<T1, T> a, Func<T2, T> b)
        {
            var guestToDelete = (from newEntry in setA
                                 join noInsert in setB on a(newEntry) equals b(noInsert)
                                 into du
                                 from ud in du.DefaultIfEmpty()
                                 where ud != null
                                 select newEntry).ToList();

            return guestToDelete;
        }
    }
}
