using System;
using System.Collections.Generic;
using System.Linq;

namespace Hotel.Domain.Extensions;

public static class CollectionExtensions
{
    public static IEnumerable<T> GetUniques<T, T1, T2>(
        this IEnumerable<T> collectionA, 
        IEnumerable<T1> collectionB,
        Func<T, T2> collectionAProp, 
        Func<T1, T2> collectionBProp)
    {
        var enumerable = collectionA as T[] ?? collectionA.ToArray();
        var common = enumerable.GetSame(collectionB, collectionAProp, collectionBProp);
        return enumerable.Except(common);
    }

    public static IEnumerable<T> GetSame<T, T1, T2>(
        this IEnumerable<T> col1, 
        IEnumerable<T1> col2, 
        Func<T, T2> col1Prop, 
        Func<T1, T2> col2Prop)
    {
        return col1.Join(col2, col1Prop, col2Prop, (a, b) => a);
    }

    public static IQueryable<T> Pagging<T>(this IQueryable<T> collection, int page, int limit)
    {
        return collection.Skip((page - 1) * limit).Take(limit);
    }
}