namespace jvmsharp.instructions.loads
{
    class _ALOAD
    {
        public static void _aload(ref rtda.Frame frame, uint index)
        {
            rtda.heap.Object val = frame.LocalVars().GetRef(index);
            frame.OperandStack().PushRef(ref val);
        }
    }

    class ALOAD : Index8Instruction
    {
        public override void Execute(ref rtda.Frame frame)
        {
            _ALOAD._aload(ref frame, Index);
        }
    }

    class ALOAD_0 : NoOperandsInstruction
    {
        public override void Execute(ref rtda.Frame frame)
        {
            _ALOAD._aload(ref frame, 0);
        }
    }

    class ALOAD_1 : NoOperandsInstruction
    {
        public override void Execute(ref rtda.Frame frame)
        {
            _ALOAD._aload(ref frame, 1);
        }
    }

    class ALOAD_2 : NoOperandsInstruction
    {
        public override void Execute(ref rtda.Frame frame)
        {
            _ALOAD._aload(ref frame, 2);
        }
    }

    class ALOAD_3 : NoOperandsInstruction
    {
        public override void Execute(ref rtda.Frame frame)
        {
            _ALOAD._aload(ref frame, 3);
        }
    }
}
