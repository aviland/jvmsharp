using jvmsharp.rtda.heap;
using System;

namespace jvmsharp.rtda
{
    class LocalVars
    {
        public object[] localVars;

        public LocalVars(uint maxLocals)
        {
            if (maxLocals > 0)
                localVars = new object[maxLocals];
            else localVars = null;
        }

        public void SetInt(uint index, int val)
        {
            localVars[index] = val;
        }

        public int GetInt(uint index)
        {
            return(int)localVars[index];
        }

        public void SetFloat(uint index, float val)
        {
            localVars[index] = val;
        }

        public float GetFloat(uint index)
        {
            return Convert.ToSingle(localVars[index]);
        }

        public void SetLong(uint index, long val)
        {
            localVars[index] = (int)val;
            localVars[index + 1] = (int)(val >> 32);
        }

        public long GetLong(uint index)
        {
         //   Console.WriteLine(localVars[index].GetType().Name);
            uint low =Convert.ToUInt32((localVars[index]));
            uint high = Convert.ToUInt32((localVars[index + 1]));
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

        public void SetRef(uint index, heap.Object refer)
        {
            localVars[index] = refer;
        }

        internal heap.Object GetRef(uint index)
        {
            object refs = localVars[index];
            return refs == null ? null :(heap.Object) localVars[index];
        }

        internal heap.Object GetThis()
        {
            return GetRef(0);
        }

        internal void SetSlot(uint index, object slot)
        {
            localVars[index] = slot;
        }
    }
}
