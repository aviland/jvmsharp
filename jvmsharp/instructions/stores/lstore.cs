namespace jvmsharp.instructions.stores
{
    class _LSTORE
    {
        public static void _lstore(ref rtda.Frame frame, uint index)
        {
            long val = frame.OperandStack().PopLong();
            frame.LocalVars().SetLong(index, val);
        }
    }

    class LSTORE : Index8Instruction
    {
        public override void Execute(ref rtda.Frame frame)
        {
            _LSTORE._lstore(ref frame, Index);
        }
    }

    class LSTORE_0 : NoOperandsInstruction
    {
        public override void Execute(ref rtda.Frame frame)
        {
            _LSTORE._lstore(ref frame, 0);
        }
    }

    class LSTORE_1 : NoOperandsInstruction
    {
        public override void Execute(ref rtda.Frame frame)
        {
            _LSTORE._lstore(ref frame, 1);
        }
    }

    class LSTORE_2 : NoOperandsInstruction
    {
        public override void Execute(ref rtda.Frame frame)
        {
            _LSTORE._lstore(ref frame, 2);
        }
    }

    class LSTORE_3 : NoOperandsInstruction
    {
        public override void Execute(ref rtda.Frame frame)
        {
            _LSTORE._lstore(ref frame, 3);
        }
    }
}
