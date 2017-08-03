namespace jvmsharp.instructions.stores
{
    class _FSTORE
    {
        public static void _fstore(ref rtda.Frame frame, uint index)
        {
            float val = frame.OperandStack().PopFloat();
            frame.LocalVars().SetFloat(index, val);
        }
    }

    class FSTORE : Index8Instruction
    {
        public override void Execute(ref rtda.Frame frame)
        {
            _FSTORE._fstore(ref frame, Index);
        }
    }

    class FSTORE_0 : NoOperandsInstruction
    {
        public override void Execute(ref rtda.Frame frame)
        {
            _FSTORE._fstore(ref frame, 0);
        }
    }

    class FSTORE_1 : NoOperandsInstruction
    {
        public override void Execute(ref rtda.Frame frame)
        {
            _FSTORE._fstore(ref frame, 1);
        }
    }

    class FSTORE_2 : NoOperandsInstruction
    {
        public override void Execute(ref rtda.Frame frame)
        {
            _FSTORE._fstore(ref frame, 2);
        }
    }

    class FSTORE_3 : NoOperandsInstruction
    {
        public override void Execute(ref rtda.Frame frame)
        {
            _FSTORE._fstore(ref frame, 3);
        }
    }
}
