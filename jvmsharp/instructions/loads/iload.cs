namespace jvmsharp.instructions.loads
{
    class _ILOAD
    {
        public static void _iload(ref rtda.Frame frame, uint index)
        {
            int val = frame.LocalVars().GetInt(index);
            frame.OperandStack().PushInt(val);
        }
    }

     class ILOAD : Index8Instruction
    {
        public override void Execute(ref rtda.Frame frame)
        {
            _ILOAD._iload(ref frame, Index);
        }
    }

    class ILOAD_0 : NoOperandsInstruction
    {
        public override void Execute(ref rtda.Frame frame)
        {
            _ILOAD._iload(ref frame, 0);
        }
    }

    class ILOAD_1 : NoOperandsInstruction
    {
        public override void Execute(ref rtda.Frame frame)
        {
            _ILOAD._iload(ref frame, 1);
        }
    }

    class ILOAD_2 : NoOperandsInstruction
    {
        public override void Execute(ref rtda.Frame frame)
        {
            _ILOAD._iload(ref frame, 2);
        }
    }

    class ILOAD_3 : NoOperandsInstruction
    {
        public override void Execute(ref rtda.Frame frame)
        {
            _ILOAD._iload(ref frame, 3);
        }
    }
}
