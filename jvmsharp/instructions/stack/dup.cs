using jvmsharp.rtda.heap;

namespace jvmsharp.instructions.stack
{
    class DUP : NoOperandsInstruction
    {
        public override void Execute(ref rtda.Frame frame)
        {
            Slot slot = frame.OperandStack().PopSlot();
            frame.OperandStack().PushSlot(ref slot);
            frame.OperandStack().PushSlot(ref slot);
        }
    }

    class DUP_X1 : NoOperandsInstruction
    {
        public override void Execute(ref rtda.Frame frame)
        {
            rtda.OperandStack stack = frame.OperandStack();
            Slot val1 = stack.PopSlot();
            Slot val2 = stack.PopSlot();
            stack.PushSlot(ref val1);
            stack.PushSlot(ref val2);
            stack.PushSlot(ref val1);
        }
    }

    class DUP_X2 : NoOperandsInstruction
    {
        public override void Execute(ref rtda.Frame frame)
        {
            rtda.OperandStack stack = frame.OperandStack();
            Slot val1 = stack.PopSlot();
            Slot val2 = stack.PopSlot();
            Slot val3 = stack.PopSlot();
            stack.PushSlot(ref val1);
            stack.PushSlot(ref val3);
            stack.PushSlot(ref val2);
            stack.PushSlot(ref val1);
        }
    }

    class DUP2 : NoOperandsInstruction
    {
        public override void Execute(ref rtda.Frame frame)
        {
            rtda.OperandStack stack = frame.OperandStack();
            Slot val1 = stack.PopSlot();
            Slot val2 = stack.PopSlot();
            stack.PushSlot(ref val2);
            stack.PushSlot(ref val1);
            stack.PushSlot(ref val2);
            stack.PushSlot(ref val1);
        }
    }

    class DUP2_X1 : NoOperandsInstruction
    {
        public override void Execute(ref rtda.Frame frame)
        {
            rtda.OperandStack stack = frame.OperandStack();
            Slot val1 = stack.PopSlot();
            Slot val2 = stack.PopSlot();
            Slot val3 = stack.PopSlot();
            stack.PushSlot(ref val2);
            stack.PushSlot(ref val1);
            stack.PushSlot(ref val3);
            stack.PushSlot(ref val2);
            stack.PushSlot(ref val1);
        }
    }

    class DUP2_X2 : NoOperandsInstruction
    {
        public override void Execute(ref rtda.Frame frame)
        {
            rtda.OperandStack stack = frame.OperandStack();
            Slot val1 = stack.PopSlot();
            Slot val2 = stack.PopSlot();
            Slot val3 = stack.PopSlot();
            Slot val4 = stack.PopSlot();
            stack.PushSlot(ref val2);
            stack.PushSlot(ref val1);
            stack.PushSlot(ref val4);
            stack.PushSlot(ref val3);
            stack.PushSlot(ref val2);
            stack.PushSlot(ref val1);
        }
    }
}
