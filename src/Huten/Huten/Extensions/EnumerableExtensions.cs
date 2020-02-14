namespace Huten.Extensions
{
    using System.Collections.Generic;
    using System.Linq;

    internal static class EnumerableExtensions
    {
        public static bool IsEmpty<T>(this IEnumerable<T> enumerable)
            => enumerable?.Any() != true;

        public static bool IsNotEmpty<T>(this IEnumerable<T> enumerable)
            => enumerable?.Any() == true;
    }
}