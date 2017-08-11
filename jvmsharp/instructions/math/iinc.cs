using System;

namespace jvmsharp.instructions.math
{
   unsafe struct IINC : Instruction
    {
        internal uint Index;
        internal int Const;

        public void FetchOperands(ref BytecodeReader reader)
        {
            Index = Convert.ToUInt32(reader.ReadUint8());
            Const = Convert.ToInt32(reader.ReadInt8());
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
