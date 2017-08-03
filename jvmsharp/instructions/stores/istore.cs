namespace jvmsharp.instructions.stores
{
    class _ISTORE
    {
        public static void _istore(ref rtda.Frame frame, uint index)
        {
            int val = frame.OperandStack().PopInt();
            frame.LocalVars().SetInt(index, val);
        }
    }

    class ISTORE : Index8Instruction
    {
        public override void Execute(ref rtda.Frame frame)
        {
            _ISTORE._istore(ref frame, Index);
        }
    }

    class ISTORE_0 : NoOperandsInstruction
    {
        public override void Execute(ref rtda.Frame frame)
        {
            _ISTORE._istore(ref frame, 0);
        }
    }

    class ISTORE_1 : NoOperandsInstruction
    {
        public override void Execute(ref rtda.Frame frame)
        {
            _ISTORE._istore(ref frame, 1);
        }
    }

    class ISTORE_2 : NoOperandsInstruction
    {
        public override void Execute(ref rtda.Frame frame)
        {
            _ISTORE._istore(ref frame, 2);
        }
    }

    class ISTORE_3 : NoOperandsInstruction
    {
        public override void Execute(ref rtda.Frame frame)
        {
            _ISTORE._istore(ref frame, 3);
        }
    }
}
