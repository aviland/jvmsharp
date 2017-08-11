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
            Thread thread = frame.Thread();
            Frame currentFrame = thread.PopFrame();
            var invokerFrame = thread.TopFrame();
            var retVal = currentFrame.OperandStack().PopRef();
            invokerFrame.OperandStack().PushRef(retVal);
        }
    }

   unsafe class DRETURN : NoOperandsInstruction
    {
        public override void Execute(ref Frame frame)
        {
            var thread = frame.Thread();
            var currentFrame = thread.PopFrame();
            var invokerFrame = thread.TopFrame();
            var retVal = currentFrame.OperandStack().PopDouble();
            invokerFrame.OperandStack().PushDouble(retVal);
        }
    }
 unsafe   class FRETURN : NoOperandsInstruction
    {
        public override void Execute(ref Frame frame)
        {
            var thread = frame.Thread();
            var currentFrame = thread.PopFrame();
            var invokerFrame = thread.TopFrame();
            var retVal = currentFrame.OperandStack().PopFloat();
            invokerFrame.OperandStack().PushFloat(retVal);
        }
    }
    unsafe class IRETURN : NoOperandsInstruction
    {
        public override void Execute(ref Frame frame)
        {
            var thread = frame.Thread();
            var currentFrame = thread.PopFrame();
            var invokerFrame = thread.TopFrame();
            var retVal = currentFrame.OperandStack().PopInt();
            invokerFrame.OperandStack().PushInt(retVal);
        }
    }

    unsafe class LRETURN : NoOperandsInstruction
    {
        public override void Execute(ref Frame frame)
        {
            var thread = frame.Thread();
            var currentFrame = thread.PopFrame();
            var invokerFrame = thread.TopFrame();
            long retVal = currentFrame.OperandStack().PopLong();
            invokerFrame.OperandStack().PushLong(retVal);
        }
    }
}
