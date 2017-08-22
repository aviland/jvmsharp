using jvmsharp.rtda.heap;
using System;

namespace jvmsharp.rtda
{
    class LocalVars
    {
        internal Slot[] localVars;

        public LocalVars newLocalVars(uint maxLocals)
        {
            LocalVars lv = new LocalVars();
            if (maxLocals > 0)
                lv. localVars = new Slot[maxLocals];
            else lv.localVars = null;
            return lv;
        }

        public void SetInt(uint index, int val)
        {
            localVars[index].num = val;
        }

        public int GetInt(uint index)
        {
            return localVars[index].num;
        }

        public void SetFloat(uint index, float val)
        {
            localVars[index].num = BitConverter.ToInt32(BitConverter.GetBytes(val), 0);
        }

        public float GetFloat(uint index)
        {
            float f = BitConverter.ToSingle(BitConverter.GetBytes(localVars[index].num), 0);
            return f;
        }

        public void SetLong(uint index, long val)
        {
            //     Console.WriteLine("long set\t" + *val);
            localVars[index].num = (int)val;
            localVars[index + 1].num = (int)(val >> 32);
        }

        public long GetLong(uint index)
        {
            uint low = (uint)(localVars[index].num);
            uint high = (uint)(localVars[(index) + 1].num);
            return ((long)high) << 32 | low;
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
            return BitConverter.ToDouble(BitConverter.GetBytes(l), 0);
        }

        internal heap.Object GetThis()
        {
            return GetRef(0);
        }
        public void SetRef(uint index, ref rtda.heap.Object refer)
        {
            localVars[index].refer = refer;
        }
        public void SetSlot(uint index, Slot slot)
        {
            localVars[index] = slot;
        }

        public Slot GetSlot(uint index)
        {
            return localVars[index];
        }
        internal rtda.heap.Object GetRef(uint index)
        {
            return localVars[index].refer;
        }

        internal bool GetBoolean(uint index)
        {
            return GetInt(index) == 1;
        }
    }
}
