namespace jvmsharp.instructions.comparisons
{
    class IF_ICMPEQ : BranchInstruction
    {
        public override void Execute(ref rtda.Frame frame)
        {
            rtda.OperandStack stack = frame.OperandStack();
            int val2 = stack.PopInt();
            int val1 = stack.PopInt();
            if (val1 == val2)
                branch_logic.Branch(ref frame, Offset);
        }
    }

     class IF_ICMPNE : BranchInstruction
    {
        public override void Execute(ref rtda.Frame frame)
        {
            rtda.OperandStack stack = frame.OperandStack();
            int val2 = stack.PopInt();
            int val1 = stack.PopInt();
            if (val1 != val2)
                branch_logic.Branch(ref frame, Offset);
        }
    }

    class IF_ICMPLT : BranchInstruction
    {
        public override void Execute(ref rtda.Frame frame)
        {
            rtda.OperandStack stack = frame.OperandStack();
            int val2 = stack.PopInt();
            int val1 = stack.PopInt();
            if (val1 < val2)
                branch_logic.Branch(ref frame, Offset);
        }
    }

    class IF_ICMPLE : BranchInstruction
    {
        public override void Execute(ref rtda.Frame frame)
        {
            rtda.OperandStack stack = frame.OperandStack();
            int val2 = stack.PopInt();
            int val1 = stack.PopInt();
            if (val1 <= val2)
                branch_logic.Branch(ref frame, Offset);
        }
    }

  unsafe class IF_ICMPGT : BranchInstruction
    {
        public override void Execute(ref rtda.Frame frame)
        {
            rtda.OperandStack stack = frame.OperandStack();
            int val2 = stack.PopInt();
            int val1 = stack.PopInt();
            if (val1 > val2)
                branch_logic.Branch(ref frame, Offset);
        }
    }

  unsafe  class IF_ICMPGE : BranchInstruction
    {
        public override void Execute(ref rtda.Frame frame)
        {
            rtda.OperandStack stack = frame.OperandStack();
            int val2 = stack.PopInt();
            int val1 = stack.PopInt();
            if (val1 >= val2)
                branch_logic.Branch(ref frame, Offset);
        }
    }
}
