namespace jvmsharp.instructions.control
{
    struct TABLE_SWITCH : Instruction
    {
        int defaultOffset;
        int low;
        int high;
        int[] jumpOffsets;

        public void FetchOperands(ref BytecodeReader reader)
        {
            reader.SkipPadding();
            defaultOffset = reader.ReadInt32();
            low = reader.ReadInt32();
            high = reader.ReadInt32();
            int jumpOffsetsCount = high - low + 1;
            jumpOffsets = reader.ReadInt32s(jumpOffsetsCount);
        }

        public void Execute(ref rtda.Frame frame)
        {
            int index = frame.OperandStack().PopInt();
            int offset;
            if (index >= low && index <= high)
                offset = jumpOffsets[index - low];
            else offset = defaultOffset;
            branch_logic.Branch(ref frame, offset);
        }
    }
}
