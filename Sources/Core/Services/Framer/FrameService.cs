using System;
using System.Collections.Generic;
using UnityEngine;

namespace ArthurKnight.Core
{
    public class FrameService : MonoBehaviour, IFrame, IFrameDispatcher
    {
        [Serializable]
        public struct RunnerSlot
        {
            public IFrameRunner Runner;
            public float NextUpdate;
            public bool Active;

            public void Reset()
            {
                Runner = null;
                NextUpdate = 0;
                Active = false;
            }
        }
        
        [field:SerializeField] public float DeltaTime { get; private set; }
        [field:SerializeField] public float Tick { get; private set; }
        [field:SerializeField] public float FixedDeltaTime { get; private set; }
        [field:SerializeField] public float UnscaledDeltaTime { get; private set; }
        [field:SerializeField] public int FrameCount { get; private set; }

        [SerializeField] private int capacity;
        [SerializeField] private RunnerSlot[] slots;
        private Stack<int> freeSlots;

        private const int DEFAULT_CAPACITY = 256;

        private void Awake()
        {
            capacity = DEFAULT_CAPACITY;
            slots = new RunnerSlot[capacity];
            freeSlots = new Stack<int>(capacity);

            for (int i = capacity - 1; i >= 0; i--)
                freeSlots.Push(i);
        }

        public void Patch(IFrameRunner runner)
        {
            if (runner == null)
                return;

            if (freeSlots.Count == 0)
                ExpandPool();

            int index = freeSlots.Pop();

            slots[index] = new RunnerSlot()
            {
                Runner = runner,
                NextUpdate = 0f,
                Active = true
            };
        }

        public void Dispatch(IFrameRunner runner)
        {
            if (runner == null)
                return;

            for (int i = 0; i < capacity; i++)
            {
                if (slots[i].Active && slots[i].Runner == runner)
                {
                    slots[i].Reset();
                    freeSlots.Push(i);
                    return;
                }
            }
        }

        private void Update()
        {
            DeltaTime         = Time.deltaTime;
            UnscaledDeltaTime = Time.unscaledDeltaTime;
            FrameCount        = Time.frameCount;
            Tick              = Time.time;

            float now = Tick;

            for (int i = 0; i < capacity; i++)
            {
                ref RunnerSlot slot = ref slots[i];

                if (!slot.Active)
                    continue;

                var runner = slot.Runner;
                if(runner == null || !runner.IsActive)
                    continue;

                float interval = runner.UpdateInterval;
                if (interval > 0 && now < slot.NextUpdate)
                    continue;
                
                runner.Framer(this);
                if (interval > 0)
                {
                    slot.NextUpdate += interval;

                    if (slot.NextUpdate < now)
                        slot.NextUpdate = now + interval;
                }
                else
                {
                    slot.NextUpdate = now;
                }
            }
        }

        private void FixedUpdate()
        {
            FixedDeltaTime = Time.fixedDeltaTime;

            for (int i = 0; i < capacity; i++)
            {
                ref var slot = ref slots[i];

                if (!slot.Active || slot.Runner == null)
                    continue;
                
                if(!slot.Runner.IsActive)
                    continue;
                
                slot.Runner.FixedFramer(this);
            }
        }

        private void LateUpdate()
        {
            for (int i = 0; i < capacity; i++)
            {
                ref var slot = ref slots[i];

                if (!slot.Active || slot.Runner == null)
                    continue;
                
                if(!slot.Runner.IsActive)
                    continue;
                
                slot.Runner.LateFramer(this);
            }
        }

        private void ExpandPool()
        {
            int newCapacity = capacity * 2;
            var newSlots    = new RunnerSlot[newCapacity];
            
            Array.Copy(slots, newSlots, capacity);
            for (int i = newCapacity - 1; i >= capacity; i--)
                freeSlots.Push(i);
            
            slots    = newSlots;
            capacity = newCapacity;
        }

        private void OnDestroy()
        {
            slots = null;
            freeSlots.Clear();
        }
    }
}