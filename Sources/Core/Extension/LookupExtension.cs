using System.Collections.Generic;
using UnityEngine;

namespace ArthurKnight.Core
{
    public static class LookupExtension
    {
        public static void BuildLookup<TKey, TSupport>(
            this Dictionary<TKey, TSupport> dictionary,
            IReadOnlyList<TSupport> list,
            bool exposeDuplicate = false)
            where TSupport : ISupportLookup<TKey>
        {
            dictionary ??= new Dictionary<TKey, TSupport>();
            
            if (dictionary.Count > 0)
                return;

            for (int i = 0; i < list.Count; i++)
            {
                var item = list[i];
                var key = item.LookupKey;

                if (exposeDuplicate && dictionary.ContainsKey(key))
                {
                    Debug.LogWarning(
                        $"[BuildLookup<{typeof(TKey).Name}>] Duplicate key {key}, overridden.");
                }

                dictionary[key] = item;
            }
        }

        public static void BuildLookup<TKey, TData, TSupport>(this Dictionary<TKey, TData> dictionary,
            IReadOnlyList<TSupport> list, bool exposeDuplicate = false)
            where TSupport : ISupportLookup<TKey, TData>
        {
            if (dictionary.Count == 0)
            {
                foreach (var item in list)
                {
                    if (exposeDuplicate && dictionary.ContainsKey(item.LookupKey))
                    {
                        Debug.LogWarning($"[BuildLookup] Dictionary contains duplicate key {item.LookupKey}, has been overriden");
                    }
                    
                    dictionary[item.LookupKey] = item.Data;
                }
            }
        }
        
        public static void BuildLookupGroup<TKey, TSupport>(this Dictionary<TKey, List<TSupport>> dictionary,
            IReadOnlyList<TSupport> list, bool exposeDuplicate = false)
            where TSupport : ISupportGroupLookup<TKey>
        {
            if (dictionary.Count == 0)
            {
                foreach (var item in list)
                {
                    if (!dictionary.TryGetValue(item.LookupGroupKey, out var group))
                    {
                        group = new List<TSupport>();
                        dictionary.Add(item.LookupGroupKey, group);
                    }
                    else if (exposeDuplicate)
                    {
                        Debug.LogWarning($"[BuildLookupGroup] Duplicate key detected: {item.LookupGroupKey}");
                    }

                    group.Add(item);
                }
            }
        }
    }
}