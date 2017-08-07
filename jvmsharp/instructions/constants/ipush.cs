namespace jvmsharp.instructions.constants
{
    struct BIPUSH:Instruction
    {
        sbyte val;
        public void FetchOperands(ref BytecodeReader reader)
        {
            val = reader.ReadInt8();
        }

        public void Execute(ref rtda.Frame frame)
        {
            frame.OperandStack().PushInt((int)val);
        }
    }

    struct SIPUSH : Instruction
    {
        short val;

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
