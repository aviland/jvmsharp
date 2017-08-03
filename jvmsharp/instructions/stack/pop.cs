namespace jvmsharp.instructions.stack
{
    class POP:NoOperandsInstruction
    {
        public override void Execute(ref rtda.Frame frame)
        {
            frame.OperandStack().PopSlot();
        }
    }

    class POP2 : NoOperandsInstruction
    {
        public override void Execute(ref rtda.Frame frame)
        {
            frame.OperandStack().PopSlot();
            frame.OperandStack().PopSlot();
        }
    }
}
