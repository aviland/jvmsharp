namespace jvmsharp.instructions.extended
{
    class IFNULL:BranchInstruction
    {
        public override void Execute(ref rtda.Frame frame)
        {
            rtda.heap.Object ref1 = frame.OperandStack().PopRef();
            if(ref1==null){
                BranchLogic.Branch(ref frame, Offset);
            }
        }
    }

    class IFNONNULL : BranchInstruction
    {
        public override void Execute(ref rtda.Frame frame)
        {
            rtda.heap.Object ref1 = frame.OperandStack().PopRef();
            if (ref1 != null)
            {
                BranchLogic.Branch(ref frame, Offset);
            }
        }
    }
}
