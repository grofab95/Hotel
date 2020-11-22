using Hotel.Domain.Extensions;
using Hotel.Sql;
using Hotel.Sql.Tools;
using System;
using System.Collections.Generic;
using System.Linq;

namespace ConsoleApp
{
    class Program
    {
        static HotelContext context = new HotelContext();

        static void Main()
        {
            try
            {

                var list1 = new List<int> { 1, 2, 3, 4, 5 };
                var list2 = new List<int> { 3, 4 };

                var unique = list1.GetUnique(list2, x => x, x => x).ToList();
                var same = list1.GetSame(list2, x => x, x => x).ToList();
            }
            catch (System.Exception ex)
            {

            }
        }

        static int Run(Func<int> func)
        {
            try
            {
                return func();
            }
            catch (Exception ex)
            {

            }

            return default;
        }

        //private static IEnumerable<T1> GetDistinct<T1, T, T2>(IEnumerable<T1> setA, IEnumerable<T2> setB, Func<T1, T> a, Func<T2, T> b)
        //{
        //    var guestToDelete = (from newEntry in setA
        //                         join noInsert in setB on a(newEntry) equals b(noInsert)
        //                         into du
        //                         from ud in du.DefaultIfEmpty()
        //                         where ud != null
        //                         select newEntry).ToList();

        //    return guestToDelete;
        //}
    }
}
