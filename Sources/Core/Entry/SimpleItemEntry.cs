using System;
using UnityEngine;

namespace ArthurKnight.Core
{
    [Serializable]
    public class SimpleItemEntry : ISupportLookup<string>, IItemEntry
    {
        [SerializeField] private string id;
        public string LookupKey => id;
        public string ID => id;
        
#if UNITY_EDITOR
        [SerializeField] private string name;
        [SerializeField, HideInInspector]
        private string editorGuid;
#endif
        
        public static implicit operator string(SimpleItemEntry entry) => entry.ID;
    }
}