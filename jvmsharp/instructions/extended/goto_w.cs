namespace jvmsharp.instructions.extended
{
    class GOTO_W:Instruction
    {
        int Offset;

        public void FetchOperands(ref BytecodeReader reader)
        {
            Offset = reader.ReadInt32();
        }

        public void Execute(ref rtda.Frame frame)
        {
            BranchLogic.Branch(ref frame, Offset);
        }
    }
}
