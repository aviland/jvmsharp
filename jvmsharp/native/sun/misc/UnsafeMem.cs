using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using jvmsharp.rtda;

namespace jvmsharp.native.sun.misc
{
    class UnsafeMem : Native
    {
        public void init()
        {
            Registry.Register("sun/misc/Unsafe", "allocateMemory", "(J)J", allocateMemory);
            Registry.Register("sun/misc/Unsafe", "reallocateMemory", "(JJ)J", reallocateMemory);
            Registry.Register("sun/misc/Unsafe", "freeMemory", "(J)V", freeMemory);
            Registry.Register("sun/misc/Unsafe", "putLong", "(JJ)V", putLong);
            Registry.Register("sun/misc/Unsafe", "getByte", "(J)B", getByte);
        }

        private void getByte(ref Frame frame)
        {
            Tuple<rtda.OperandStack, byte[]>  tuple = get(ref frame);
    //  Console.WriteLine("+++++++++++++++++++++"+(int)Int8(tuple.Item2));
            tuple.Item1.PushInt(Convert.ToInt32(Int8(tuple.Item2)));
        }

        sbyte Int8(byte[] s)
        {
            return (sbyte)(s[0]);
        }

       Tuple<rtda.OperandStack, byte[]> get(ref rtda.Frame frame)
        {
            LocalVars vars = frame.LocalVars();
            // vars.GetRef(0) // this
            long address = vars.GetLong(1);
         //   Console.WriteLine(address);
            byte[] mem =  Malloc.memoryAt(address);
       //    foreach (byte b in mem)
        //        Console.Write(b + " ");
            return Tuple.Create(frame.OperandStack(),mem);
        }

        private  void putLong(ref Frame frame)
        {
            LocalVars vars = frame.LocalVars();
            // vars.GetRef(0) // this
            long address = vars.GetLong(1);
            long value = vars.GetLong(3);
            byte[] mem =Malloc.memoryAt(address);
            PutInt64(mem, ref value);
            Malloc.setMemory(mem, address);
     //       foreach (byte b in Malloc.memoryAt(address))
        //        Console.Write(b + " ");
        }

        private  void PutInt64(byte[] s, ref long val)
        {
            s[0] = (byte)(val >> 56);
            s[1] = (byte)(val>>48);
            s[2] = (byte)(val >> 40);
            s[3] = (byte)(val >> 32);
            s[4] = (byte)(val >> 24);
            s[5] = (byte)(val >> 16);
            s[6] = (byte)(val >> 8);
            s[7] = (byte)val;
        }

        private void freeMemory(ref Frame frame)
        {
            LocalVars vars = frame.LocalVars();
            // vars.GetRef(0) // this
            long address = vars.GetLong(1);
             Malloc.free(address);
          //  Console.WriteLine(frame.method.name);
        }

        private void reallocateMemory(ref Frame frame)
        {
            LocalVars vars = frame.LocalVars();
            // vars.GetRef(0) // this
            long address = vars.GetLong(1);
            long bytes = vars.GetLong(3);
            long newAddress =  new Malloc().reallocate(address, bytes);
            OperandStack stack = frame.OperandStack();
            stack.PushLong(newAddress);
        }

        private void allocateMemory(ref Frame frame)
        {
            LocalVars vars = frame.LocalVars();
            // vars.GetRef(0) // this
            long bytes = vars.GetLong(1);
            long address =new  Malloc().allocate(bytes);
            OperandStack stack = frame.OperandStack();
            stack.PushLong(address);
        }
    }
}
