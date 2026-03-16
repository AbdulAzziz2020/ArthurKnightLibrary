using System.Collections.Generic;
using UnityEngine;

namespace ArthurKnight.Core
{
    public static class IEnumerableExtensions
    {
        public static T SafeGet<T>(this IList<T> list, int index)
        {
            list ??= new List<T>();
            if (list.Count == 0)
                return default;

            int clampedIndex = Mathf.Clamp(index, 0, list.Count - 1);
            return list[clampedIndex];
        }

        public static T GetRandom<T>(this IReadOnlyList<T> list)
        {
            if (list == null || list.Count == 0)
                return default;

            int index = Random.Range(0, list.Count);
            return list[index];
        }
    }
}