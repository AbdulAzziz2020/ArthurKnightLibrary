using System;
using UnityEngine;
using UnityEngine.Localization;

namespace ArthurKnight.Core
{
    [Serializable]
    public class LocaleNaming : INaming
    {
        [SerializeField] private LocalizedString value;
        public string GetString() => value?.GetLocalizedString();
    }
}