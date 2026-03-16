using System;
using UnityEngine;

namespace ArthurKnight.Core
{
    [Serializable]
    public class SimpleNaming : INaming
    {
        [SerializeField] private string value;
        public string GetString() => value;

        public SimpleNaming() { }
        public SimpleNaming(string value) => this.value = value;
    }
}