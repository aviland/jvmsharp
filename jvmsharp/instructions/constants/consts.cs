using jvmsharp.rtda;
using System;

namespace jvmsharp.instructions.constants
{
    class ACONST_NULL : NoOperandsInstruction
    {
        public override void Execute(ref Frame frame)
        {
            rtda.heap.Object v = null;
            frame.OperandStack().PushRef(v);
        }
    }

  unsafe  class DCONST_0 : NoOperandsInstruction
    {
        public override void Execute(ref rtda.Frame frame)
        {
            double d = 0;
            frame.OperandStack().PushDouble(d);
        }
    }

    unsafe class DCONST_1 : NoOperandsInstruction
    {
        public override void Execute(ref rtda.Frame frame)
        {
            double d =1;
            frame.OperandStack().PushDouble(d);
        }
    }

    unsafe class FCONST_0 : NoOperandsInstruction
    {
        public override void Execute(ref rtda.Frame frame)
        {
            frame.OperandStack().PushFloat(0);
        }
    }

    unsafe class FCONST_1 : NoOperandsInstruction
    {
        public override void Execute(ref rtda.Frame frame)
        {
            float f = 1;
            frame.OperandStack().PushFloat(f);
        }
    }

    unsafe class FCONST_2 : NoOperandsInstruction
    {
        public override void Execute(ref rtda.Frame frame)
        {
            float f = 2;
            frame.OperandStack().PushFloat(f);
        }
    }

    unsafe class ICONST_M1 : NoOperandsInstruction
    {
        public override void Execute(ref rtda.Frame frame)
        {
            int i = -1;
            frame.OperandStack().PushInt(i);
        }
    }

    unsafe class ICONST_0 : NoOperandsInstruction
    {
        public override void Execute(ref rtda.Frame frame)
        {
            int i = 0;
            frame.OperandStack().PushInt(i);
        }
    }

    unsafe class ICONST_1 : NoOperandsInstruction
    {
        public override void Execute(ref rtda.Frame frame)
        {
            int i = 1;
            frame.OperandStack().PushInt(i);
        }
    }

    unsafe class ICONST_2 : NoOperandsInstruction
    {
        public override void Execute(ref rtda.Frame frame)
        {
            int i = 2;
            frame.OperandStack().PushInt(i);
        }
    }

    unsafe class ICONST_3 : NoOperandsInstruction
    {
        public override void Execute(ref rtda.Frame frame)
        {
            int i =3;
            frame.OperandStack().PushInt(i);
        }
    }

    unsafe class ICONST_4 : NoOperandsInstruction
    {
        public override void Execute(ref rtda.Frame frame)
        {
            int i =4;
            frame.OperandStack().PushInt(i);
        }
    }

    unsafe class ICONST_5 : NoOperandsInstruction
    {
        public override void Execute(ref rtda.Frame frame)
        {
            int i = 5;
            frame.OperandStack().PushInt(i);
        }
    }

    unsafe class LCONST_0 : NoOperandsInstruction
    {
        public override void Execute(ref rtda.Frame frame)
        {
            long l = 0;
            frame.OperandStack().PushLong(l);
        }
    }

    unsafe class LCONST_1 : NoOperandsInstruction
    {
        public override void Execute(ref rtda.Frame frame)
        {
            long l = 1;
            frame.OperandStack().PushLong(l);
        }
    }
}
