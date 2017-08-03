namespace jvmsharp.instructions.extended
{
    class GOTO_W:Instruction
    {
        int offset;

        public void FetchOperands(ref BytecodeReader reader)
        {
            offset = reader.ReadInt32();
        }

        public void Execute(ref rtda.Frame frame)
        {
            branch_logic.Branch(ref frame, offset);
        }
    }
}
