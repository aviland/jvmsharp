using jvmsharp.rtda.heap;

namespace jvmsharp.instructions.stack
{
    class SWAP : NoOperandsInstruction
    {
        public override void Execute(ref rtda.Frame frame)
        {
            rtda.OperandStack stack = frame.OperandStack();
            object slot1 = stack.PopSlot();
            object slot2 = stack.PopSlot();
            stack.PushSlot(ref slot1);
            stack.PushSlot(ref slot2);
        }
    }
}
