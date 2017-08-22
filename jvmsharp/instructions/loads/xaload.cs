using System;
using jvmsharp.rtda;

namespace jvmsharp.instructions.loads
{
    class XAX
    {
        internal static void checkNotNull(rtda.heap.Object refs)
        {
            if (refs == null)
                throw new Exception("java.lang.NullPointerException");
        }

        internal static void checkIndex(int arrLen, int index)
        {
            if (index < 0 || index >= arrLen)
            {
                throw new Exception("ArrayIndexOutOfBoundsException");
            }

        }
    }

     class AALOAD : NoOperandsInstruction
    {
        public override void Execute(ref Frame frame)
        {
            OperandStack stack = frame.OperandStack();
            int index = stack.PopInt();
            rtda.heap.Object arrRef = stack.PopRef();
            XAX.checkNotNull(arrRef);
            rtda.heap.Object[] refs = arrRef.Refs();
            XAX.checkIndex(refs.Length, index);
            frame.operandStack.PushRef(refs[index]);
        }
    }

     class BALOAD : NoOperandsInstruction
    {
        public override void Execute(ref Frame frame)
        {
            OperandStack stack = frame.OperandStack();
            int index = stack.PopInt();
            rtda.heap.Object arrRef = stack.PopRef();
            XAX.checkNotNull(arrRef);
            byte[] bytes = arrRef.Bytes();
            XAX.checkIndex(bytes.Length, index);
            int x = bytes[index];
            frame.operandStack.PushInt(x);
        }
    }

    unsafe class CALOAD : NoOperandsInstruction
    {
        public override void Execute(ref Frame frame)
        {
            OperandStack stack = frame.OperandStack();
            int index = stack.PopInt();
            rtda.heap.Object arrRef = stack.PopRef();
            XAX.checkNotNull(arrRef);
            ushort[] chars = arrRef.Chars();
            XAX.checkIndex(chars.Length, index);
            int x = chars[index];
            frame.OperandStack().PushInt(x);
        }
    }

     class DALOAD : NoOperandsInstruction
    {
        public override void Execute(ref Frame frame)
        {
            OperandStack stack = frame.OperandStack();
            int index = stack.PopInt();
            rtda.heap.Object arrRef = stack.PopRef();
            XAX.checkNotNull(arrRef);
            double[] doubles = arrRef.Doubles();
            XAX.checkIndex(doubles.Length, index);
            frame.OperandStack().PushDouble(doubles[index]);
        }
    }

     class FALOAD : NoOperandsInstruction
    {
        public override void Execute(ref Frame frame)
        {
            OperandStack stack = frame.OperandStack();
            int index = stack.PopInt();
            rtda.heap.Object arrRef = stack.PopRef();
            XAX.checkNotNull(arrRef);
            float[] floats = arrRef.Floats();
            XAX.checkIndex(floats.Length, index);
            frame.OperandStack().PushFloat(floats[index]);
        }
    }

    class IALOAD : NoOperandsInstruction
    {
        public override void Execute(ref Frame frame)
        {
            OperandStack stack = frame.OperandStack();
            int index = stack.PopInt();
            rtda.heap.Object arrRef = stack.PopRef();
            XAX.checkNotNull(arrRef);
            int[] ints = arrRef.Ints();
            XAX.checkIndex(ints.Length, index);
            stack.PushInt(ints[index]);
        }
    }

     class LALOAD : NoOperandsInstruction
    {
        public override void Execute(ref Frame frame)
        {
            OperandStack stack = frame.OperandStack();
            int index = stack.PopInt();
            rtda.heap.Object arrRef = stack.PopRef();
            XAX.checkNotNull(arrRef);
            long[] longs = arrRef.Longs();
            XAX.checkIndex(longs.Length, index);
            stack.PushLong(longs[index]);
        }
    }

     class SALOAD : NoOperandsInstruction
    {
        public override void Execute(ref Frame frame)
        {
            OperandStack stack = frame.OperandStack();
            int index = stack.PopInt();
            rtda.heap.Object arrRef = stack.PopRef();
            XAX.checkNotNull(arrRef);

            short[] shorts = arrRef.Shorts();
            XAX.checkIndex(shorts.Length, index);
            frame.OperandStack().PushInt(shorts[index]);
        }
    }
}
