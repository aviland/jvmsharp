namespace jvmsharp.instructions.math
{
    unsafe class IXOR : NoOperandsInstruction
    {
        public override void Execute(ref rtda.Frame frame)
        {
            rtda.OperandStack stack = frame.OperandStack();
            int v2 = stack.PopInt();
            int v1 = stack.PopInt();
            int result = v1 ^ v2;
            stack.PushInt(result);
        }
    }

    unsafe class LXOR : NoOperandsInstruction
    {
        public override void Execute(ref rtda.Frame frame)
        {
            var stack = frame.OperandStack();
            long v2 = stack.PopLong();
            long v1 = stack.PopLong();
            long result = v1 ^ v2;
            stack.PushLong(result);
        }
    }
}
