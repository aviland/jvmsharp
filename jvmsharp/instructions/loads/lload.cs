namespace jvmsharp.instructions.loads
{
   unsafe class _LLOAD
    {
        public static void _lload(ref rtda.Frame frame, uint index)
        {
            long val = frame.LocalVars().GetLong(index);
            frame.OperandStack().PushLong(val);
        }
    }

    class LLOAD : Index8Instruction
    {
        public override void Execute(ref rtda.Frame frame)
        {
            _LLOAD._lload(ref frame, Index);
        }
    }

    class LLOAD_0 : NoOperandsInstruction
    {
        public override void Execute(ref rtda.Frame frame)
        {
            _LLOAD._lload(ref frame, 0);
        }
    }

    class LLOAD_1 : NoOperandsInstruction
    {
        public override void Execute(ref rtda.Frame frame)
        {
            _LLOAD._lload(ref frame, 1);
        }
    }

    class LLOAD_2 : NoOperandsInstruction
    {
        public override void Execute(ref rtda.Frame frame)
        {
            _LLOAD._lload(ref frame, 2);
        }
    }

    class LLOAD_3: NoOperandsInstruction
    {
        public override void Execute(ref rtda.Frame frame)
        {
            _LLOAD._lload(ref frame, 3);
        }
    }
}
