using System;

namespace ArthurKnight.Core
{
    public abstract class StateFactory<TEntity, TEnum>  where TEnum : Enum
    {
        public abstract BaseState<TEntity, TEnum> Create(TEntity entity, StateMachine<TEntity, TEnum> machine, TEnum type);
    }
}