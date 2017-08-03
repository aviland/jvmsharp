using jvmsharp.rtda.heap;
using System;

namespace jvmsharp.rtda
{
    class LocalVars
    {
        public Slot[] localVars;

        public LocalVars(uint maxLocals)
        {
            if (maxLocals > 0)
                localVars = new Slot[maxLocals];
            else localVars = null;
        }

        public void SetInt(uint index, Int32 val)
        {
            localVars[index].num = val;
        }

        public Int32 GetInt(uint index)
        {
            return localVars[index].num;
        }

        public void SetFloat(uint index, float val)
        {
            localVars[index].num = BitConverter.ToInt32(BitConverter.GetBytes(val), 0);
        }

        public float GetFloat(uint index)
        {
            return BitConverter.ToSingle(BitConverter.GetBytes((float)localVars[index].num), 0);
        }

        public void SetLong(uint index, Int64 val)
        {
            localVars[index].num = (Int32)val;
            localVars[index + 1].num = (Int32)(val >> 32);
        }

        public Int64 GetLong(uint index)
        {
            uint low = (uint)(localVars[index].num);
            uint high = (uint)(localVars[index + 1].num);
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

        public void SetRef(uint index, heap.Object refer)
        {
            localVars[index].refer = refer;
        }

        internal heap.Object GetRef(uint index)
        {
            object refs = localVars[index];
            return refs == null ? null : localVars[index].refer;
        }

        internal heap.Object GetThis()
        {
            return GetRef(0);
        }

        internal void SetSlot(uint index,ref Slot slot)
        {
            localVars[index] = slot;
        }
    }
}
