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

        public int PopInt()
        {
            size--;
      //      Console.WriteLine(size + "PopInt " + slots.Length);
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
            slots[size] = val;
            size += 2;
   //         Console.WriteLine( "PushLong " +val);
        }

        public long PopLong()
        {
            size -= 2;
            var top = slots[size];

            slots[size] = null;
            return (long)top;
        }

        public void PushDouble(double val)
        {
            slots[size] = val;
            size += 2;
         //   Console.WriteLine( "PushDouble " + val);
        }

        public double PopDouble()
        {
            size -= 2;
            var top = slots[size];
            slots[size] = null;
     //       Console.WriteLine(size + "PushDouble " + slots.Length);
            return (float)top;
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
