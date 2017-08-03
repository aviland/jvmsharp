namespace jvmsharp.instructions
{
    class branch_logic
    {
        internal static void Branch(ref rtda.Frame frame, int offset)
        {
            int pc = frame.Thread().PC();
            int nextPC = pc + offset;
            frame.SetNextPC(nextPC);
        }
    }
}