namespace jvmsharp.instructions.comparisons
{
   class LCMP : NoOperandsInstruction
    {
        public  override void Execute(ref rtda.Frame frame)
        {
            rtda.OperandStack stack = frame.OperandStack();
            long v2 = stack.PopLong();
            long v1 = stack.PopLong();
            int i;
            if (v1 > v2)
                i = 1;
            else if (v1 == v2)
                i = 0;
            else i = -1;
           frame.operandStack.PushInt(i);
        }
    }
}
