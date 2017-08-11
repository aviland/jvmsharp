namespace jvmsharp.instructions.loads
{
    unsafe class _FLOAD
    {
        public static void _fload(ref rtda.Frame frame, uint index)
        {
            float val = frame.LocalVars().GetFloat(index);
            frame.OperandStack().PushFloat(val);
        }
    }

    class FLOAD : Index8Instruction
    {
        public override void Execute(ref rtda.Frame frame)
        {
            _FLOAD._fload(ref frame, Index);
        }
    }

    class FLOAD_0 : NoOperandsInstruction
    {
        public override void Execute(ref rtda.Frame frame)
        {
            _FLOAD._fload(ref frame, 0);
        }
    }
    class FLOAD_1 : NoOperandsInstruction
    {
        public override void Execute(ref rtda.Frame frame)
        {
            _FLOAD._fload(ref frame, 1);
        }
    }
    class FLOAD_2 : NoOperandsInstruction
    {
        public override void Execute(ref rtda.Frame frame)
        {
            _FLOAD._fload(ref frame, 2);
        }
    }
    class FLOAD_3 : NoOperandsInstruction
    {
        public override void Execute(ref rtda.Frame frame)
        {
            _FLOAD._fload(ref frame, 3);
        }
    }
}
