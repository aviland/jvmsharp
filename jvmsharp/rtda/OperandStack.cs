using System;
using jvmsharp.rtda.heap;
namespace jvmsharp.rtda
{
    unsafe class OperandStack : Slots //操作数栈
    {
        internal uint size;//栈容量，指向栈顶

        public OperandStack(uint maxStack)
        {
            if (maxStack > 0)    //初始化操作数栈
                slots = new Slot[maxStack];
            else slots = null;
            size = 0;
        }

        unsafe public void PushInt(int val)
        {
            SetInt(size, val);
            (size)++;
        }

        public void PushBoolean(bool val)
        {
            int i = 0;
            if (val) i = 1;
            PushInt(i);
        }

        public bool PopBoolean()
        {
                 return PopInt() == 1;
        }

        public int PopInt()
        {
            size--;
            return GetInt(size);
        }

        public void PushFloat(float val)
        {
            SetFloat(size, val); //float转换为二进制再转换为int32存储
            size++;
        }

        public float PopFloat()
        {
            size--;
            float f = GetFloat(size);
            return f;
        }

        public void PushLong(long val)
        {
            SetLong(size, val);
            size += 2;
        }

        public long PopLong()
        {
            size -= 2;
            long l= GetLong(size);
            return l;
        }

        public void PushDouble(double val)
        {
            SetDouble(size, val);
            size += 2;
        }

        public double PopDouble()
        {
            size -= 2;
            double d= GetDouble(size);
            return d;
        }

        public void PushRef(heap.Object refer)
        {
            SetRef(size, refer);
            size++;
            //    Console.WriteLine( "PushRef "  +refer);
        }

        public heap.Object PopRef()
        {
            size--;
            return GetRef(size);
        }

        public void PushSlot(ref Slot slot)
        {
            SetSlot(size, slot);
            size++;
            //    Console.WriteLine( "PushSlot " +slot);
        }

        public Slot PopSlot()
        {
            size--;
            return GetSlot(size);
        }

        public heap.Object GetRefFromTop(uint n)
        {
            return GetRef(size - n - 1);// return this.slots[size - n-1].refer;
        }
    }
}
