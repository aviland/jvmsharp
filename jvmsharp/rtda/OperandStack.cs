using System;
using jvmsharp.rtda.heap;
namespace jvmsharp.rtda
{
    class OperandStack  //操作数栈
    {
        internal uint size;//栈容量，指向栈顶
        internal Slot[] slots;
        internal uint Size()
        {
            return size;
        }

        public OperandStack newOperandStack(uint maxStack)
        {
            OperandStack os = new OperandStack();
            os.size = 0;
            if (maxStack > 0) //初始化操作数栈
            {
                os.slots = new Slot[maxStack];
                return os;
            }
            return null;
        }

        public void PushInt(int val)
        {
            slots[size].num = val;
            size++;
        }

        public void PushBoolean(bool val)
        {
            if (val) PushInt(1);
            else PushInt(0);
        }

        public bool PopBoolean()
        {
            return PopInt() == 1;
        }

        public int PopInt()
        {
            size--;
            return slots[size].num;
        }

        public void PushFloat(float val)
        {
            slots[size].num = BitConverter.ToInt32(BitConverter.GetBytes(val), 0); //float转换为二进制再转换为int32存储
            size++;
        }

        public float PopFloat()
        {
            size--;
            return BitConverter.ToSingle(BitConverter.GetBytes(slots[size].num), 0);
        }

        public void PushLong(long val)
        {
            slots[size].num = (int)val;
            slots[size + 1].num = (int)(val >> 32);
            size += 2;
        }

        public long PopLong()
        {
            size -= 2;
            uint low = (uint)(slots[size].num);
            uint high = (uint)(slots[size + 1].num);
            return ((long)high) << 32 | low;
        }

        public void PushDouble(double val)
        {
            long l = BitConverter.ToInt64(BitConverter.GetBytes(val), 0);
            PushLong(l);
        }

        public double PopDouble()
        {
            long l = PopLong();
            return BitConverter.ToDouble(BitConverter.GetBytes(l), 0);
        }

        public void PushRef(heap.Object refer)
        {
            slots[size].refer = refer;
            size++;
            //    Console.WriteLine( "PushRef "  +refer);
        }

        public heap.Object PopRef()
        {
            size--;
            var refs = slots[size].refer;
            slots[size].refer = null;
            return refs;
        }

        public void PushSlot(Slot slot)
        {
            slots[size] = slot;
            size++;
            //    Console.WriteLine( "PushSlot " +slot);
        }

        public Slot PopSlot()
        {
            size--;
            return slots[size];
        }

        internal heap.Object GetRefFromTop(uint n)
        {
            return slots[size - 1 - n].refer;// return this.slots[size - n-1].refer;
        }

        internal void Clear()
        {
            size = 0;
            for (int i = 0; i < slots.Length; i++)
            {
                slots[i].refer = null;
            }
        }
    }
}
