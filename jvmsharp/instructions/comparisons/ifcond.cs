namespace jvmsharp.instructions.comparisons
{
     class IFEQ : BranchInstruction
    {
        public override void Execute(ref rtda.Frame frame)
        {
            int val = frame.OperandStack().PopInt();
            if (val == 0)
            {
                BranchLogic.Branch(ref frame, Offset);
            }
        }
    }

     class IFNE : BranchInstruction
    {
        public override void Execute(ref rtda.Frame frame)
        {
            int val = frame.OperandStack().PopInt();
            if (val != 0)
            {
                BranchLogic.Branch(ref frame, Offset);
            }
        }
    }

     class IFLT : BranchInstruction
    {
        public override void Execute(ref rtda.Frame frame)
        {
            int val = frame.OperandStack().PopInt();
            if (val < 0)
            {
                BranchLogic.Branch(ref frame, Offset);
            }
        }
    }

     class IFLE : BranchInstruction
    {
        public override void Execute(ref rtda.Frame frame)
        {
            int val = frame.OperandStack().PopInt();
            if (val <= 0)
            {
                BranchLogic.Branch(ref frame, Offset);
            }
        }
    }

     class IFGT : BranchInstruction
    {
        public override void Execute(ref rtda.Frame frame)
        {
            int val = frame.OperandStack().PopInt();
            if (val > 0)
            {
                BranchLogic.Branch(ref frame, Offset);
            }
        }
    }

     class IFGE : BranchInstruction
    {
        public override void Execute(ref rtda.Frame frame)
        {
            int val = frame.OperandStack().PopInt();
            if (val >= 0)
            {
                BranchLogic.Branch(ref frame, Offset);
            }
        }
    }
}
