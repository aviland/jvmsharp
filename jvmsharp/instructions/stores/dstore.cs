namespace jvmsharp.instructions.stores
{
    unsafe class _DSTORE
    {
        public static void _dstore(ref rtda.Frame frame, uint index)
        {
            double val = frame.OperandStack().PopDouble();
            frame.LocalVars().SetDouble(index, val);
        }
    }

    class DSTORE : Index8Instruction
    {
        public override void Execute(ref rtda.Frame frame)
        {
            _DSTORE._dstore(ref frame, Index);
        }
    }

    class DSTORE_0 : NoOperandsInstruction
    {
        public override void Execute(ref rtda.Frame frame)
        {
            _DSTORE._dstore(ref frame, 0);
        }
    }

    class DSTORE_1 : NoOperandsInstruction
    {
        public override void Execute(ref rtda.Frame frame)
        {
            _DSTORE._dstore(ref frame, 1);
        }
    }

    class DSTORE_2 : NoOperandsInstruction
    {
        public override void Execute(ref rtda.Frame frame)
        {
            _DSTORE._dstore(ref frame, 2);
        }
    }

    class DSTORE_3 : NoOperandsInstruction
    {
        public override void Execute(ref rtda.Frame frame)
        {
            _DSTORE._dstore(ref frame, 3);
        }
    }
}
