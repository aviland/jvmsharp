using jvmsharp.instructions.loads;
using jvmsharp.rtda;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace jvmsharp.instructions.stores
{
    unsafe class AASTORE : NoOperandsInstruction
    {
        public override void Execute(ref Frame frame)
        {
            OperandStack stack = frame.OperandStack();
            rtda.heap.Object val = stack.PopRef();
            int index = stack.PopInt();
            var arrRef = stack.PopRef();

            XAX.checkNotNull(arrRef);
            rtda.heap.Object[] refs = arrRef.Refs();
            XAX.checkIndex(refs.Length, index);
            refs[index] = val;
        }
    }

    unsafe class BASTORE : NoOperandsInstruction
    {
        public override void Execute(ref Frame frame)
        {
            OperandStack stack = frame.OperandStack();
            int val = stack.PopInt();
            int index = stack.PopInt();
            var arrRef = stack.PopRef();

            XAX.checkNotNull(arrRef);
            byte[] bytes = arrRef.Bytes();
            XAX.checkIndex(bytes.Length, index);
            bytes[index] = Convert.ToByte(val);
        }
    }

    unsafe class CASTORE : NoOperandsInstruction
    {
        public override void Execute(ref Frame frame)
        {
            OperandStack stack = frame.OperandStack();
            int val = stack.PopInt();
            int index = stack.PopInt();
            var arrRef = stack.PopRef();

            XAX.checkNotNull(arrRef);
            ushort[] chars = arrRef.Chars();
            XAX.checkIndex(chars.Length, index);
            chars[index] = (UInt16)val;
        }
    }

    unsafe class DASTORE : NoOperandsInstruction
    {
        public override void Execute(ref Frame frame)
        {
            OperandStack stack = frame.OperandStack();
            double val = stack.PopDouble();
            int index = stack.PopInt();
            var arrRef = stack.PopRef();

            XAX.checkNotNull(arrRef);
            double[] doubles = arrRef.Doubles();
            XAX.checkIndex(doubles.Length, index);
            doubles[index] = val;
        }
    }

    unsafe class FASTORE : NoOperandsInstruction
    {
        public override void Execute(ref Frame frame)
        {
            OperandStack stack = frame.OperandStack();
            float val = stack.PopFloat();
            int index = stack.PopInt();
            var arrRef = stack.PopRef();

            XAX.checkNotNull(arrRef);
            float[] floats = arrRef.Floats();
            XAX.checkIndex(floats.Length, index);
            floats[index] = val;
        }
    }

    unsafe class IASTORE : NoOperandsInstruction
    {
        public override void Execute(ref Frame frame)
        {
            OperandStack stack = frame.OperandStack();
            int val = stack.PopInt();
            int index = stack.PopInt();
            //      Console.WriteLine("val" + val + "index" + index);
            rtda.heap.Object arrRef = stack.PopRef();
            //  Console.WriteLine(arrRef.data.GetType().Name);

            XAX.checkNotNull(arrRef);
            int[] ints = arrRef.Ints();
            XAX.checkIndex(ints.Length, index);
            ints[index] = val;
        }
    }

    unsafe class LASTORE : NoOperandsInstruction
    {
        public override void Execute(ref Frame frame)
        {
            OperandStack stack = frame.OperandStack();
            long val = stack.PopLong();
            int index = stack.PopInt();
            var arrRef = stack.PopRef();

            XAX.checkNotNull(arrRef);
            long[] longs = arrRef.Longs();
            XAX.checkIndex(longs.Length, index);
            longs[index] = val;
        }
    }

    unsafe class SASTORE : NoOperandsInstruction
    {
        public override void Execute(ref Frame frame)
        {
            OperandStack stack = frame.OperandStack();
            int val = stack.PopInt();
            int index = stack.PopInt();
            var arrRef = stack.PopRef();

            XAX.checkNotNull(arrRef);
            short[] shorts = arrRef.Shorts();
            XAX.checkIndex(shorts.Length, index);
            shorts[index] = Convert.ToInt16(val);
        }
    }
}
