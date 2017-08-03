using System;

namespace jvmsharp.instructions.math
{
    struct IINC : Instruction
    {
        public uint Index;
        public Int32 Const;

        public void FetchOperands(ref BytecodeReader reader)
        {
            Index = reader.ReadUint8();
            Const = reader.ReadInt8();
        }

        public void Execute(ref rtda.Frame frame)
        {
            var localVars = frame.LocalVars();
            int val = localVars.GetInt(Index);
            val += Const;
            localVars.SetInt(Index, val);
        }
    }
}
