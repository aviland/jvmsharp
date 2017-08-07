using System;

namespace jvmsharp.rtda.heap
{
    struct Slot
    {
        public int num;//num存储数值
        public Object refer;//refer存储方法等引用
    }

    class Slots
    {
        public Slot[] slots;

        public Slots(uint slotCount)
        {
            slots = new Slot[slotCount];
        }

        public Slots(object slots)
        {
            int[] sl = (int[])slots;
            this.slots = new Slot[sl.Length];
            for(int i = 0; i < sl.Length; i++)
            {
                this.slots[i].num = sl[i];
            }
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

        public void SetRef(uint index, Object refer)
        {
            slots[index].refer = refer;
        }

        public Object GetRef(uint index)
        {
            Object refs = (Object)slots[index].refer;
            return refs == null ? null : refs;
        }
    }
}
