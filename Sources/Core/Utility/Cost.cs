using System;
using UnityEngine;

namespace ArthurKnight.Core
{
    [Serializable]    
    public struct CostInt<T>
    {
        [SerializeField] private T type;
        [SerializeField] private int amount;

        public T Type => type;
        public int Amount => amount;
        
        public CostInt(T type, int amount)
        {
            this.type = type;
            this.amount = amount;
        }
        
        public static implicit operator T(CostInt<T> costInt) => costInt.Type;
    }
    
    [Serializable]    
    public struct CostIntMinMax<T>
    {
        [SerializeField] private T type;
        [SerializeField] private int min;
        [SerializeField] private int max;

        public T Type => type;
        public int Random => UnityEngine.Random.Range(min, max + 1);
        public int Min => min;
        public int Max => max;
        
        public CostIntMinMax(T type, int min, int max)
        {
            this.type = type;
            this.min = min;
            this.max = max;
        }

        public static implicit operator CostInt<T>(CostIntMinMax<T> costIntMinMax)
        {
            return new CostInt<T>(costIntMinMax.Type, costIntMinMax.Random);
        }
    }
    
    [Serializable]
    public struct Cost<T>
    {
        [SerializeField] private T type;
        [SerializeField] private float amount;
        
        public T Type => type;
        public float Amount => amount;

        public Cost(T type, float amount)
        {
            this.type = type;
            this.amount = amount;
        }
        
        public static implicit operator T(Cost<T> cost) => cost.Type;
    }
    
    [Serializable]    
    public struct CostMinMax<T>
    {
        [SerializeField] private T type;
        [SerializeField] private float min;
        [SerializeField] private float max;

        public T Type => type;
        public float Random => UnityEngine.Random.Range(min, max + 1);
        public float Min => min;
        public float Max => max;
        
        public CostMinMax(T type, float min, float max)
        {
            this.type = type;
            this.min = min;
            this.max = max;
        }
        
        public static implicit operator Cost<T>(CostMinMax<T> costMinMax)
        {
            return new Cost<T>(costMinMax.Type, costMinMax.Random);
        }
    }
}