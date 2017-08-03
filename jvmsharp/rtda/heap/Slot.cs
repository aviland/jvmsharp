using System;

namespace jvmsharp.rtda.heap
{
    struct Slot
    {
        public Int32 num;//num存储数值
        public Object refer;//refer存储方法等引用
    }

    class Slots
    {
        public Slot[] slots;

        public Slots(uint slotCount)
        {
            slots = new Slot[slotCount];
        }

        public Slots(ref Slot[] slots)
        {
            this.slots = slots;
        }

        public void SetInt(uint index, Int32 val)
        {
            slots[index].num = val;
        }

        public Int32 GetInt(uint index)
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

        public void SetLong(uint index, Int64 val)
        {
            slots[index].num = (Int32)val;
            slots[index + 1].num = (Int32)(val >> 32);
        }

        public Int64 GetLong(uint index)
        {
            uint low = (uint)(slots[index].num);
            uint high = (uint)(slots[index + 1].num);
            return (Int64)high << 32 | low;
        }

        public void SetDouble(uint index, double val)
        {
            SetLong(index, BitConverter.ToInt64(BitConverter.GetBytes(val), 0));
        }

        public double GetDouble(uint index)
        {
            Int64 u64 = GetLong(index);
            return BitConverter.ToDouble(BitConverter.GetBytes(u64), 0);
        }

        public void SetRef(uint index, ref heap.Object refer)
        {
            slots[index].refer = refer;
        }

        public heap.Object GetRef(uint index)
        {
            object refs = slots[index];
            return refs == null ? null : slots[index].refer;
        }
    }
}
