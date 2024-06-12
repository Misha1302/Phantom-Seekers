namespace Game.Scripts.Extensions
{
    using System;
    using System.Collections;
    using System.Collections.Generic;

    public static class CollectionsExtensions
    {
        public static void ForAll<T>(this IReadOnlyList<T> lst, Action<T> act)
        {
            // ReSharper disable once ForCanBeConvertedToForeach
            for (var index = 0; index < lst.Count; index++)
                act(lst[index]);
        }

        public static void ForAll<T>(this IEnumerable<T> lst, Action<T> act)
        {
            foreach (var item in lst)
                act(item);
        }

        public static void ForAll<T>(this IEnumerable lst, Action<T> act)
        {
            foreach (T item in lst)
                act(item);
        }

        public static T GetBy<T>(this IEnumerable<T> enumerable, Func<T, T, bool> predicate)
        {
            using var enumerator = enumerable.GetEnumerator();

            if (!enumerator.MoveNext()) return default;

            var result = enumerator.Current;

            while (enumerator.MoveNext())
            {
                var current = enumerator.Current;
                if (predicate(current, result))
                    result = current;
            }

            return result;
        }

        public static ArraySegment<T> Slice<T>(this T[] arr, int start, int count) => new(arr, start, count);
    }
}