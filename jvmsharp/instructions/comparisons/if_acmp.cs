using System;

namespace jvmsharp.instructions.comparisons
{
    class IF_ACMPEQ:BranchInstruction
    {
        public override void Execute(ref rtda.Frame frame)
        {
            rtda.OperandStack stack = frame.OperandStack();
            Object ref2 = stack.PopRef();
            Object ref1 = stack.PopRef();
            if (ref1 == ref2)
                branch_logic.Branch(ref frame, Offset);
        }
    }

    class IF_ACMPNE : BranchInstruction
    {
        public override void Execute(ref rtda.Frame frame)
        {
            rtda.OperandStack stack = frame.OperandStack();
            Object ref2 = stack.PopRef();
            Object ref1 = stack.PopRef();
            if (ref1 != ref2)
                branch_logic.Branch(ref frame, Offset);
        }
    }
}
