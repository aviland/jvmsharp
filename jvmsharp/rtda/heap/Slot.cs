using System;

namespace jvmsharp.rtda.heap
{
    unsafe struct Slot
    {
        internal int num;//num存储数值
        internal Object refer;//refer存储方法等引用
    }

    unsafe class Slots
    {
        internal Slot[] slots;

        public Slots(uint slotCount)
        {
            if (slotCount > 0)
            {
                slots = new Slot[slotCount];
            }
            else slots = null;
        }
        public Slots() { }
        public Slots(object slots)
        {
            slots = ((Slots)slots);
        }

        public void SetInt(uint index, int val)
        {
            slots[index].num = val;
        }

        public int GetInt(uint index)
        {
            int i= slots[index].num;
            return i;
        }

        public void SetFloat(uint index, float val)
        {
            slots[index].num = BitConverter.ToInt32(BitConverter.GetBytes(val), 0);
        }

        public float GetFloat(uint index)
        {
            float f = BitConverter.ToSingle(BitConverter.GetBytes(slots[index].num), 0);
            return f;
        }

        public void SetLong(uint index, long val)
        {
         //     Console.WriteLine("long set\t" + *val);
            long x = val;
            int i = (int)x;
            int j = (int)(x >> 32);
            slots[index].num = i;
            slots[index + 1].num = j;
        }

        public long GetLong(uint index)
        {
            uint low = (uint)(slots[index].num);
            uint high = (uint)(slots[(index) + 1].num);
            long lon = ((long)high )<< 32 | (long)low;
    //            Console.WriteLine("long get\t" + lon);
            return lon;
        }

        public void SetDouble(uint index, double val)
        {
     //       Console.WriteLine("double set:" + *val);
            long l = BitConverter.ToInt64(BitConverter.GetBytes(val), 0);
            SetLong(index, l);
        }

        public double GetDouble(uint index)
        {
            long l = GetLong(index);
            double d = BitConverter.ToDouble(BitConverter.GetBytes(l), 0);
    //        Console.WriteLine("double get:" + d);
            return d;
        }

        public void SetRef(uint index, rtda.heap.Object refer)
        {
            slots[index].refer = refer;
        }

        public rtda.heap.Object GetRef(uint index)
        {
            return slots[index].refer;
        }

        public void SetSlot(uint index, Slot slot)
        {
            slots[index] = slot;
        }

        public Slot GetSlot(uint index)
        {
            return slots[index];
        }
    }
}
