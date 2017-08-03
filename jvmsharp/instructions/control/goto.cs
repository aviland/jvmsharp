namespace jvmsharp.instructions.control
{
    class  GOTO:BranchInstruction
    {
        public override void Execute(ref rtda.Frame frame)
        {
            branch_logic.Branch(ref frame, Offset);
        }
    }
}
