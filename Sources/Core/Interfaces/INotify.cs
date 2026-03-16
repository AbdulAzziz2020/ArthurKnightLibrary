using System;

namespace ArthurKnight.Core
{
    public interface INotify<T>
    {
        event Action<T> OnChanged;
        void Notify();
    }
}

