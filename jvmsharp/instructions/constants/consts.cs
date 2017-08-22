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

    class DCONST_0 : NoOperandsInstruction
    {
        public override void Execute(ref rtda.Frame frame)
        {
            frame.OperandStack().PushDouble(0.0D);
        }
    }

     class DCONST_1 : NoOperandsInstruction
    {
        public override void Execute(ref rtda.Frame frame)
        {
            frame.OperandStack().PushDouble(1.0D);
        }
    }

     class FCONST_0 : NoOperandsInstruction
    {
        public override void Execute(ref rtda.Frame frame)
        {
            frame.OperandStack().PushFloat(0);
        }
    }

     class FCONST_1 : NoOperandsInstruction
    {
        public override void Execute(ref rtda.Frame frame)
        {
            frame.OperandStack().PushFloat(1f);
        }
    }

     class FCONST_2 : NoOperandsInstruction
    {
        public override void Execute(ref rtda.Frame frame)
        {
            frame.OperandStack().PushFloat(2f);
        }
    }

     class ICONST_M1 : NoOperandsInstruction
    {
        public override void Execute(ref rtda.Frame frame)
        {
            frame.OperandStack().PushInt(-1);
        }
    }

     class ICONST_0 : NoOperandsInstruction
    {
        public override void Execute(ref rtda.Frame frame)
        {
            frame.OperandStack().PushInt(0);
        }
    }

     class ICONST_1 : NoOperandsInstruction
    {
        public override void Execute(ref rtda.Frame frame)
        {
            frame.OperandStack().PushInt(1);
        }
    }

     class ICONST_2 : NoOperandsInstruction
    {
        public override void Execute(ref rtda.Frame frame)
        {
            frame.OperandStack().PushInt(2);
        }
    }

     class ICONST_3 : NoOperandsInstruction
    {
        public override void Execute(ref rtda.Frame frame)
        {
            frame.OperandStack().PushInt(3);
        }
    }

     class ICONST_4 : NoOperandsInstruction
    {
        public override void Execute(ref rtda.Frame frame)
        {
            frame.OperandStack().PushInt(4);
        }
    }

     class ICONST_5 : NoOperandsInstruction
    {
        public override void Execute(ref rtda.Frame frame)
        {
            frame.OperandStack().PushInt(5);
        }
    }

     class LCONST_0 : NoOperandsInstruction
    {
        public override void Execute(ref rtda.Frame frame)
        {
            frame.OperandStack().PushLong(0L);
        }
    }

     class LCONST_1 : NoOperandsInstruction
    {
        public override void Execute(ref rtda.Frame frame)
        {
            frame.OperandStack().PushLong(1L);
        }
    }
}
