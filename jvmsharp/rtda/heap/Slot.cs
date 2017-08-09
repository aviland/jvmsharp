using System;
using System.Collections.Generic;

namespace jvmsharp.rtda.heap
{
    class Slot
    {
        public int num;//num存储数值
        public object refer;//refer存储方法等引用

        public Slot()
        {
            num = 0;
            refer = null;
        }
    }

    class Slots
    {
        public Slot[] slots;

        public Slots(uint slotCount)
        {
            List<Slot> ls = new List<Slot>();
            int i = 0;
            while (i < slotCount)
            {
                ls.Add(new Slot());
                i++;
            }
            slots = ls.ToArray();
        }

        public Slots(object slots)
        {
            this.slots = ((Slots)slots).slots;
        }

        public void SetInt(uint index, int val)
        {
            slots[index].num = val;
        }

        public int GetInt(uint index)
        {
            return slots[index].num;
        }

        public void SetFloat(uint index, float val)
        {
            slots[index].num = BitConverter.ToInt32(BitConverter.GetBytes(val), 0);
        }

        public float GetFloat(uint index)
        {
            return BitConverter.ToSingle(BitConverter.GetBytes((float)slots[index].num), 0);
        }

        public void SetLong(uint index, long val)
        {
            slots[index].num = (int)val;
            slots[index + 1].num = (int)(val >> 32);
        }

        public long GetLong(uint index)
        {
            uint low = (uint)(slots[index].num);
            uint high = (uint)(slots[index + 1].num);
            return (long)high << 32 | low;
        }

        public void SetDouble(uint index, double val)
        {
            SetLong(index, BitConverter.ToInt64(BitConverter.GetBytes(val), 0));
        }

        public double GetDouble(uint index)
        {
            long u64 = GetLong(index);
            return BitConverter.ToDouble(BitConverter.GetBytes(u64), 0);
        }

        public void SetRef(uint index, rtda.heap.Object refer)
        {
            slots[index].refer = refer;
        }

        public rtda.heap.Object GetRef(uint index)
        {
            return (rtda.heap.Object)slots[index].refer;
        }
    }
}
