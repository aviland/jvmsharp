namespace jvmsharp.instructions.comparisons
{
    class dcmp
    {
       public static void _dcmp(ref rtda.Frame frame, bool gFlag)
        {
            rtda.OperandStack stack = frame.OperandStack();
            double v2 = stack.PopDouble();
            double v1 = stack.PopDouble();
            if (v1 > v2) 
                stack.PushInt(1);
            else if (v1 == v2) 
                stack.PushInt(0);
            else if (v1 < v2) 
                stack.PushInt(-1);
            else if (gFlag) 
                stack.PushInt(1);
            else 
                stack.PushInt(-1);
        }
    }

    class DCMPG : NoOperandsInstruction
    {
        public override void Execute(ref rtda.Frame frame)
        {
            dcmp._dcmp(ref frame, true);
        }
    }

    class DCMPL : NoOperandsInstruction
    {
        public override void Execute(ref rtda.Frame frame)
        {
            dcmp._dcmp(ref frame, false);
        }
    }
}
