using System;

namespace jvmsharp.instructions.conversions
{
   unsafe class I2L : NoOperandsInstruction
    {
        public override void Execute(ref rtda.Frame frame)
        {
            int i = frame.OperandStack().PopInt();
            long l = Convert.ToInt64(i);
            frame.OperandStack().PushLong(l);
        }
    }

    unsafe class I2F : NoOperandsInstruction
    {
        public override void Execute(ref rtda.Frame frame)
        {
            rtda.OperandStack stack = frame.OperandStack();
            int i = stack.PopInt();
            float f = Convert.ToSingle(i);
            frame.OperandStack().PushFloat(f);
        }
    }

    unsafe class I2D : NoOperandsInstruction
    {
        public override void Execute(ref rtda.Frame frame)
        {
            rtda.OperandStack stack = frame.OperandStack();
            int i = stack.PopInt();
            double d = Convert.ToDouble(i);
            frame.OperandStack().PushDouble(d);
        }
    }

 unsafe  class I2B : NoOperandsInstruction
    {
        public override void Execute(ref rtda.Frame frame)
        {
            int i = frame.OperandStack().PopInt();
            int b = Convert.ToInt32(Convert.ToSByte(i));
            frame.OperandStack().PushInt(b);
        }
    }

    unsafe class I2C : NoOperandsInstruction
    {
        public override void Execute(ref rtda.Frame frame)
        {
            var stack = frame.OperandStack();
            var i = stack.PopInt();
            var c = Convert.ToInt32(Convert.ToUInt16(i));
            stack.PushInt(c);
        }
    }

    unsafe class I2S : NoOperandsInstruction
    {
        public override void Execute(ref rtda.Frame frame)
        {
            var stack = frame.OperandStack();
            var i = stack.PopInt();
            var s = Convert.ToInt32(Convert.ToInt16(i));
            frame.OperandStack().PushInt(s);
        }
    }
}