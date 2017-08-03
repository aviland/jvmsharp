using System;

namespace jvmsharp.instructions.math
{
    class ISHL : NoOperandsInstruction
    {
        public override void Execute(ref rtda.Frame frame)
        {
            rtda.OperandStack stack = frame.OperandStack();
            int v2 = stack.PopInt();
            int v1 = stack.PopInt();
            int s = v2 & 0x1f;
            int result = v1 << s;
            stack.PushInt(result);
        }
    }

    class ISHR: NoOperandsInstruction
    {
        public override void Execute(ref rtda.Frame frame)
        {
            var stack = frame.OperandStack();
            int v2 = stack.PopInt();
            int v1 = stack.PopInt();
            int s = v2 & 0x1f;
            int result = v1 >> s;
            stack.PushInt(result);
        }
    }

    class IUSHR : NoOperandsInstruction
    {
        public override void Execute(ref rtda.Frame frame)
        {
            var stack = frame.OperandStack();
            int v2 = stack.PopInt();
            int v1 = stack.PopInt();
            int s = v2 & 0x1f;
            int result = (int)(((uint)v1) >> s);
            stack.PushInt(result);
        }
    }
    class LSHL : NoOperandsInstruction
    {
        public override void Execute(ref rtda.Frame frame)
        {
            var stack = frame.OperandStack();
            int v2 = stack.PopInt();
            long v1 = stack.PopLong();
            int s = v2 & 0x3f;
            long result = v1<< s;
            stack.PushLong(result);
        }
    }

    class LSHR : NoOperandsInstruction
    {
        public override void Execute(ref rtda.Frame frame)
        {
            var stack = frame.OperandStack();
            int v2 = stack.PopInt();
            long v1 = stack.PopLong();
            int s = v2 & 0x3f;
            long result = v1>> s;
            stack.PushLong(result);
        }
    }

    class LUSHR : NoOperandsInstruction
    {
        public override void Execute(ref rtda.Frame frame)
        {
            var stack = frame.OperandStack();
            int v2 = stack.PopInt();
            long v1 = stack.PopLong();
            int s = v2 & 0x3f;
            long result = (long)(((UInt64)v1) >> s);
            stack.PushLong(result);
        }
    }
}
