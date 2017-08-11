namespace jvmsharp.instructions.stores
{
 unsafe   class _ASTORE
    {
        public static void _astore(ref rtda.Frame frame, uint index)
        {
            rtda.heap.Object val = frame.OperandStack().PopRef();
            frame.LocalVars().SetRef(index, val);
        }
    }

    class ASTORE : Index8Instruction
    {
        public override void Execute(ref rtda.Frame frame)
        {
            _ASTORE._astore(ref frame, Index);
        }
    }

    class ASTORE_0 : NoOperandsInstruction
    {
        public override void Execute(ref rtda.Frame frame)
        {
            _ASTORE._astore(ref frame, 0);
        }
    }

    class ASTORE_1 : NoOperandsInstruction
    {
        public override void Execute(ref rtda.Frame frame)
        {
            _ASTORE._astore(ref frame, 1);
        }
    }

    class ASTORE_2 : NoOperandsInstruction
    {
        public override void Execute(ref rtda.Frame frame)
        {
            _ASTORE._astore(ref frame, 2);
        }
    }

    class ASTORE_3 : NoOperandsInstruction
    {
        public override void Execute(ref rtda.Frame frame)
        {
            _ASTORE._astore(ref frame, 3);
        }
    }
}
