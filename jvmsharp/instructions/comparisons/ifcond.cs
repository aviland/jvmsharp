namespace jvmsharp.instructions.comparisons
{
    unsafe class IFEQ : BranchInstruction
    {
        public override void Execute(ref rtda.Frame frame)
        {
            int val = frame.OperandStack().PopInt();
            if (val == 0)
            {
                branch_logic.Branch(ref frame, Offset);
            }
        }
    }

    unsafe class IFNE : BranchInstruction
    {
        public override void Execute(ref rtda.Frame frame)
        {
            int val = frame.OperandStack().PopInt();
            if (val != 0)
            {
                branch_logic.Branch(ref frame, Offset);
            }
        }
    }

    unsafe class IFLT : BranchInstruction
    {
        public override void Execute(ref rtda.Frame frame)
        {
            int val = frame.OperandStack().PopInt();
            if (val < 0)
            {
                branch_logic.Branch(ref frame, Offset);
            }
        }
    }

    unsafe class IFLE : BranchInstruction
    {
        public override void Execute(ref rtda.Frame frame)
        {
            int val = frame.OperandStack().PopInt();
            if (val <= 0)
            {
                branch_logic.Branch(ref frame, Offset);
            }
        }
    }

    unsafe class IFGT : BranchInstruction
    {
        public override void Execute(ref rtda.Frame frame)
        {
            int val = frame.OperandStack().PopInt();
            if (val > 0)
            {
                branch_logic.Branch(ref frame, Offset);
            }
        }
    }

    unsafe class IFGE : BranchInstruction
    {
        public override void Execute(ref rtda.Frame frame)
        {
            int val = frame.OperandStack().PopInt();
            if (val >= 0)
            {
                branch_logic.Branch(ref frame, Offset);
            }
        }
    }
}
