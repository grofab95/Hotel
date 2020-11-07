using System;

namespace Hotel.Application.Extensions
{
    public static class StringExtensions
    {
        public static bool IsNotExist(this string value)
            => string.IsNullOrEmpty(value) || string.IsNullOrWhiteSpace(value);

        public static string FirstCharToUpper(this string value)
        {
            if (value.IsNotExist())            
                return string.Empty;            

            return char.ToUpper(value[0]) + value.Substring(1);
        }

        public static string ReductStringLength(this string input, int length)
        {
            if (input.Length <= length)
                return input;

            var reducted = input.Substring(0, length - 3) + "...";
            return reducted;
        }
    }
}
