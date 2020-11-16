using System;

namespace Hotel.Domain.Extensions
{
    public static class StringExtensions
    {
        public static bool IsNotExist(this String value)
            => string.IsNullOrEmpty(value) || string.IsNullOrWhiteSpace(value);

        public static bool IsLike(this String value, string comparer)
            => value.ToLower().Trim() == comparer.ToLower().Trim();

        public static bool IsNotLike(this String value, string comparer)
            => !IsLike(value, comparer);
    }
}
