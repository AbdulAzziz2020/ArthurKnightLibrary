using System;
using System.Collections.Generic;

namespace ArthurKnight.Core
{
    public abstract class StateMachine<TEntity, TEnum> where TEnum : Enum
    {
        protected BaseState<TEntity, TEnum> current;
        protected Dictionary<TEnum, BaseState<TEntity, TEnum>> states = new();
        protected StateFactory<TEntity, TEnum> factory;
        
        public BaseState<TEntity, TEnum> Current => current;
        
        public abstract void Initialize(TEntity entity, TEnum startType);

        public virtual void ChangeState(TEnum newState)
        {
            current?.Exit();
            current = states[newState];
            current?.Enter();
        }
        
        public virtual void Update(in IFrame frame) => current?.Update(frame);
        public virtual void FixedUpdate(in IFrame frame) => current?.FixedUpdate(frame);
        public virtual void LateUpdate(in IFrame frame) => current?.LateUpdate(frame);
    }
}