using System;
using jvmsharp.rtda.heap;
namespace jvmsharp.rtda
{
    class OperandStack//操作数栈
    {
        internal uint size;//栈容量
        internal object[] slots;//slot为栈元素

        public OperandStack(uint maxStack)
        {
            //初始化操作数栈
            if (maxStack > 0)
                slots = new object[maxStack];
            else slots = null;
            size = 0;
        }

        public void PushInt(int val)
        {
            slots[size] = val;
            size++;
        //   Console.WriteLine("PushInt " + val);
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
      //     Console.WriteLine( "PopInt? " + slots[size].GetType().Name);
            return (int)slots[size]; 
        }

        public void PushFloat(float val)
        {
            //float转换为二进制再转换为int32存储
            slots[size] = BitConverter.ToInt32(BitConverter.GetBytes(val), 0);
            size++;
      //      Console.WriteLine("PushFloat " +val);
        }

        public float PopFloat()
        {
            size--;
            float f =(float) slots[size];
     //       Console.WriteLine(size + "PopFloat " + slots.Length);
            return BitConverter.ToSingle(BitConverter.GetBytes(f), 0);
        }

        public void PushLong(long val)
        {
            slots[size] = (int)val;
            slots[size + 1] = (int)(val >> 32);
            size += 2;
   //         Console.WriteLine( "PushLong " +val);
        }

        public long PopLong()
        {
            size -= 2;
     //       Console.WriteLine(slots[size].GetType().Name);
            uint low = (uint)((int)slots[size]);
            uint high =(uint)((int)slots[size + 1]);
            return (long)high << 32 | low;
        }

        public void PushDouble(double val)
        {
            PushLong( BitConverter.ToInt64(BitConverter.GetBytes(val), 0));
            //   Console.WriteLine( "PushDouble " + val);
        }

        public double PopDouble()
        {
            long u64 = PopLong();
            return BitConverter.ToDouble(BitConverter.GetBytes(u64), 0);
        }

        public void PushRef( heap.Object refer)
        {
            slots[size] = refer;
            size++;
        //    Console.WriteLine( "PushRef "  +refer);
        }

        public heap.Object  PopRef()
        {
            size--;
            var top =  slots[size];
           slots[size] = null;
     //     Console.WriteLine(size + "PopRef " + slots.Length);
            if (top == null)
                return null;
            else return (heap.Object)top;
        }

        public void PushSlot(ref object slot)
        {
            slots[size] = slot;
            size++;
     //    Console.WriteLine( "PushSlot " +slot);
        }

        public object PopSlot()
        {
            size--;
            var top = slots[size];
            slots[size] = null;
     //     Console.WriteLine(size + "PopSlot " + slots.Length);
            return top;
        }

        public heap.Object GetRefFromTop(uint n)
        {
            return (heap.Object)this.slots[size - n-1];// return this.slots[size - n-1].refer;
        }
    }
}
