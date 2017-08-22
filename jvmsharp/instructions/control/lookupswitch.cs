using System;

namespace jvmsharp.instructions.control
{
    class LOOKUP_SWITCH : Instruction
    {
        int defaultOffset;
        int npairs;
        int[] matchOffsets;

        public void FetchOperands(ref BytecodeReader reader)
        {
            reader.SkipPadding();
            defaultOffset = reader.ReadInt32();
            npairs = reader.ReadInt32();
            matchOffsets = reader.ReadInt32s(npairs * 2);
        }

        public void Execute(ref rtda.Frame frame)
        {
            int key = frame.OperandStack().PopInt();
            for (int i = 0; i < npairs * 2; i += 2)
            {
                if (matchOffsets[i] == key)
                {
                    int offset = matchOffsets[i + 1];
                    BranchLogic.Branch(ref frame, offset);
                    return;
                }
            }
            BranchLogic.Branch(ref frame, defaultOffset);
        }
    }
}
