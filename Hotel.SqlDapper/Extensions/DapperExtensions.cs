﻿using System;
using System.Collections.Generic;
using System.Linq;

namespace Hotel.SqlDapper.Extensions
{
    public static class DapperExtensions
    {
        public static IEnumerable<TFirst> Map<TFirst, TSecond, TKey>
            (
            this Dapper.SqlMapper.GridReader reader,
            Func<TFirst, TKey> firstKey,
            Func<TSecond, TKey> secondKey,
            Action<TFirst, IEnumerable<TSecond>> addChildren
            )
        {
            var first = reader.Read<TFirst>().ToList();
            var childMap = reader
                .Read<TSecond>()
                .GroupBy(s => secondKey(s))
                .ToDictionary(g => g.Key, g => g.AsEnumerable());

            foreach (var item in first)
            {
                IEnumerable<TSecond> children;
                if (childMap.TryGetValue(firstKey(item), out children))
                {
                    addChildren(item, children);
                }
            }

            return first;
        }
    }
}
