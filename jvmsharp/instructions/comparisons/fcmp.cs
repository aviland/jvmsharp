namespace jvmsharp.instructions.comparisons
{
    class fcmp
    {
       public   static void _fcmp(ref rtda.Frame frame, bool gFlag)
        {
            rtda.OperandStack stack = frame.OperandStack();
            float v2 = stack.PopFloat();
            float v1 = stack.PopFloat();
            int i ;
            if (v1 > v2)
                i = 1;
            else if (v1 == v2)
                i = 0;
            else if (v1 < v2)
                i = -1;
            else if (gFlag)
                i = 1;
            else
                i = -1;
            frame.operandStack.PushInt(i);
        }
    }

    class FCMPG : NoOperandsInstruction
    {
        public override void Execute(ref rtda.Frame frame)
        {
            fcmp._fcmp(ref frame, true);
        }
    }

    class FCMPL : NoOperandsInstruction
    {
        public override void Execute(ref rtda.Frame frame)
        {
            fcmp._fcmp(ref frame, false);
        }
    }
}
