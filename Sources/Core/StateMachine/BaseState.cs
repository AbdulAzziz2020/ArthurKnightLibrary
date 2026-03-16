using System;
using UnityEngine;

namespace ArthurKnight.Core
{
    public abstract class BaseState<TEntity, TEnum> where TEnum : Enum
    {
        public TEntity Entity;
        public StateMachine<TEntity, TEnum> StateMachine;

        public BaseState(TEntity entity, StateMachine<TEntity, TEnum> stateMachine)
        {
            Entity = entity;
            StateMachine = stateMachine;
        }

        public virtual void Enter()
        {
            Debug.Log($"Enter {nameof(BaseState<TEntity, TEnum>)} {GetType().Name}");
        }
        
        public virtual void Exit(){ }
        
        public virtual void Update(in IFrame frame){ }
        public virtual void FixedUpdate(in IFrame frame){ }
        public virtual void LateUpdate(in IFrame frame){ }
    }
}