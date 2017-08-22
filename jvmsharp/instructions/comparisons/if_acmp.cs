using System;

namespace jvmsharp.instructions.comparisons
{
    class IF_ACMPEQ:BranchInstruction
    {
        public override void Execute(ref rtda.Frame frame)
        {
            rtda.OperandStack stack = frame.OperandStack();
            rtda.heap.Object ref2 = stack.PopRef();
            rtda.heap.Object ref1 = stack.PopRef();
            if (ref1 == ref2)
                BranchLogic.Branch(ref frame, Offset);
        }
    }

    class IF_ACMPNE : BranchInstruction
    {
        public override void Execute(ref rtda.Frame frame)
        {
            rtda.OperandStack stack = frame.OperandStack();
            rtda.heap.Object ref2 = stack.PopRef();
            rtda.heap.Object ref1 = stack.PopRef();
            if (ref1 != ref2)
                BranchLogic.Branch(ref frame, Offset);
        }
    }
}
