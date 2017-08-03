using System;
using jvmsharp.rtda.heap;
namespace jvmsharp.rtda
{
    class OperandStack//操作数栈
    {
        public uint size;//栈容量
        public Slot[] slots;//slot为栈元素

        public OperandStack(uint maxStack)
        {
            //初始化操作数栈
            if (maxStack > 0)
                slots = new Slot[maxStack];
            else slots = null;
        }

        public void PushInt(int val)
        {
            slots[size].num = val;
            size++;
        }

        public int PopInt()
        {
            size--;
            return slots[size].num;
        }

        public void PushFloat(float val)
        {
            //float转换为二进制再转换为int32存储
            slots[size].num = BitConverter.ToInt32(BitConverter.GetBytes(val), 0);
            size++;
        }

        public float PopFloat()
        {
            size--;
            float f = slots[size].num;
            return BitConverter.ToSingle(BitConverter.GetBytes(f), 0);
        }

        public void PushLong(Int64 val)
        {
            slots[size].num = (Int32)val;
            slots[size + 1].num = (Int32)(val >> 32);
            size += 2;
        }

        public Int64 PopLong()
        {
            size -= 2;
            UInt32 low = (UInt32)slots[size].num;
            UInt32 high = (UInt32)slots[size + 1].num;
            return (Int64)high << 32 | low;
        }

        public void PushDouble(double val)
        {
            PushLong(BitConverter.ToInt64(BitConverter.GetBytes(val), 0));
        }

        public double PopDouble()
        {
            return BitConverter.ToDouble(BitConverter.GetBytes(PopLong()), 0);
        }

        public void PushRef(ref  heap.Object refer)
        {
            slots[size].refer = refer;
            size++;
        }

        public heap.Object  PopRef()
        {
            size--;
            object refer = slots[size].refer;
            slots[size].refer = null;
            return (heap.Object)refer;
        }

        public void PushSlot(ref Slot slot)
        {
            slots[size] = slot;
            size++;
        }

        public Slot PopSlot()
        {
            size--;
            return slots[size];
        }

        public heap.Object GetRefFromTop(uint n)
        {
            return this.slots[size - n-1].refer;// return this.slots[size - n-1].refer;
        }
    }
}
