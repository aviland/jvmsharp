using System;

namespace jvmsharp.instructions.conversions
{
    class I2L : NoOperandsInstruction
    {
        public override void Execute(ref rtda.Frame frame)
        {
            rtda.OperandStack stack = frame.OperandStack();
            int i = stack.PopInt();
            long l = Convert.ToInt64(i);
            stack.PushLong(l);
        }
    }

    class I2F : NoOperandsInstruction
    {
        public override void Execute(ref rtda.Frame frame)
        {
            rtda.OperandStack stack = frame.OperandStack();
            int i = stack.PopInt();
            float f = Convert.ToSingle(i);
            stack.PushFloat(f);
        }
    }

    class I2D : NoOperandsInstruction
    {
        public override void Execute(ref rtda.Frame frame)
        {
            rtda.OperandStack stack = frame.OperandStack();
            int i = stack.PopInt();
            double d = Convert.ToDouble(i);
            stack.PushDouble(d);
        }
    }

    class I2B : NoOperandsInstruction
    {
        public override void Execute(ref rtda.Frame frame)
        {
            var stack = frame.OperandStack();
            var i = stack.PopInt();
            var b = Convert.ToInt32(Convert.ToSByte(i));
            stack.PushInt(b);
        }
    }

    class I2C : NoOperandsInstruction
    {
        public override void Execute(ref rtda.Frame frame)
        {
            var stack = frame.OperandStack();
            var i = stack.PopInt();
            var c = Convert.ToInt32(Convert.ToUInt16(i));
            stack.PushInt(c);
        }
    }

    class I2S : NoOperandsInstruction
    {
        public override void Execute(ref rtda.Frame frame)
        {
            var stack = frame.OperandStack();
            var i = stack.PopInt();
            var s = Convert.ToInt32(Convert.ToInt16(i));
            stack.PushInt(s);
        }
    }
}