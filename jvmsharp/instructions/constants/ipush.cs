using System;

namespace jvmsharp.instructions.constants
{
    struct BIPUSH:Instruction
    {
        int val;
        public void FetchOperands(ref BytecodeReader reader)
        {
            val = reader.ReadInt8();
        }

        public void Execute(ref rtda.Frame frame)
        {
            frame.OperandStack().PushInt(val);
        }
    }

    struct SIPUSH : Instruction
    {
        Int16 val;

        public void FetchOperands(ref BytecodeReader reader)
        {
            val = reader.ReadInt16();
        }

        public void Execute(ref rtda.Frame frame)
        {
            int i= val;
            frame.OperandStack().PushInt(i);
        }
    }
}
