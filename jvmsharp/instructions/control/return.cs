using jvmsharp.rtda;

namespace jvmsharp.instructions.control
{
    class RETURN : NoOperandsInstruction
    {
        public override void Execute(ref Frame frame)
        {
            frame.Thread().PopFrame();
        }
    }

    class ARETURN : NoOperandsInstruction
    {
        public override void Execute(ref Frame frame)
        {
            var thread = frame.Thread();
            var currentFrame = thread.PopFrame();
            var invokerFrame = thread.TopFrame();
            var refs = currentFrame.OperandStack().PopRef();
            invokerFrame.OperandStack().PushRef(refs);
        }
    }

     class DRETURN : NoOperandsInstruction
    {
        public override void Execute(ref Frame frame)
        {
            double retVal = frame.Thread().PopFrame().OperandStack().PopDouble();
            frame.Thread().TopFrame().OperandStack().PushDouble(retVal);
        }
    }
    class FRETURN : NoOperandsInstruction
    {
        public override void Execute(ref Frame frame)
        {
            float retVal = frame.Thread().PopFrame().OperandStack().PopFloat();
            frame.Thread().TopFrame().OperandStack().PushFloat(retVal);
        }
    }
     class IRETURN : NoOperandsInstruction
    {
        public override void Execute(ref Frame frame)
        {
            int retVal = frame.Thread().PopFrame().OperandStack().PopInt();
            frame.Thread().TopFrame().OperandStack().PushInt(retVal);
        }
    }

     class LRETURN : NoOperandsInstruction
    {
        public override void Execute(ref Frame frame)
        {
            long retVal = frame.Thread().PopFrame().OperandStack().PopLong();
            frame.Thread().TopFrame().OperandStack().PushLong(retVal);
        }
    }
}
