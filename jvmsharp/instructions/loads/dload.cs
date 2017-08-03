namespace jvmsharp.instructions.loads
{
    class _DLOAD
    {
        public static void _dload(ref rtda.Frame frame, uint index)
        {
            double val = frame.LocalVars().GetDouble(index);
            frame.OperandStack().PushDouble(val);
        }
    }

    class DLOAD : Index8Instruction
    {
        public override void Execute(ref rtda.Frame frame)
        {
            _DLOAD._dload(ref frame, Index);
        }
    }

    class DLOAD_0 : NoOperandsInstruction
    {
        public override void Execute(ref rtda.Frame frame)
        {
            _DLOAD._dload(ref frame, 0);
        }
    }

    class DLOAD_1 : NoOperandsInstruction
    {
        public override void Execute(ref rtda.Frame frame)
        {
            _DLOAD._dload(ref frame, 1);
        }
    }

    class DLOAD_2 : NoOperandsInstruction
    {
        public override void Execute(ref rtda.Frame frame)
        {
            _DLOAD._dload(ref frame, 2);
        }
    }

    class DLOAD_3 : NoOperandsInstruction
    {
        public override void Execute(ref rtda.Frame frame)
        {
            _DLOAD._dload(ref frame, 3);
        }
    }
}
