using System;

namespace jvmsharp.instructions.control
{
    class LOOKUP_SWITCH:Instruction
    {
        Int32 defaultOffset;
        Int32 npairs;
        Int32[] matchOffsets;

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
            for (Int32 i = 0; i < npairs * 2; i += 2)
            {
                if (matchOffsets[i] == key)
                {
                    Int32 offset = matchOffsets[i + 1];
                    branch_logic.Branch(ref frame, offset);
                    return;
                }
            }
            branch_logic.Branch(ref frame, defaultOffset);
        }
    }
}
