using jvmsharp.rtda.heap;

namespace jvmsharp.instructions.stack
{
    class DUP : NoOperandsInstruction
    {
        public override void Execute(ref rtda.Frame frame)
        {
            Slot slot = frame.OperandStack().PopSlot();
            frame.OperandStack().PushSlot( slot);
            frame.OperandStack().PushSlot( slot);
        }
    }

    class DUP_X1 : NoOperandsInstruction
    {
        public override void Execute(ref rtda.Frame frame)
        {
            rtda.OperandStack stack = frame.OperandStack();
            Slot val1 = stack.PopSlot();
            Slot val2 = stack.PopSlot();
            stack.PushSlot(val1);
            stack.PushSlot(val2);
            stack.PushSlot(val1);
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
            stack.PushSlot(val1);
            stack.PushSlot( val3);
            stack.PushSlot( val2);
            stack.PushSlot( val1);
        }
    }

    class DUP2 : NoOperandsInstruction
    {
        public override void Execute(ref rtda.Frame frame)
        {
            rtda.OperandStack stack = frame.OperandStack();
            Slot val1 = stack.PopSlot();
            Slot val2 = stack.PopSlot();
            stack.PushSlot( val2);
            stack.PushSlot( val1);
            stack.PushSlot( val2);
            stack.PushSlot( val1);
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
            stack.PushSlot( val2);
            stack.PushSlot( val1);
            stack.PushSlot( val3);
            stack.PushSlot( val2);
            stack.PushSlot( val1);
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
            stack.PushSlot( val2);
            stack.PushSlot( val1);
            stack.PushSlot( val4);
            stack.PushSlot( val3);
            stack.PushSlot( val2);
            stack.PushSlot( val1);
        }
    }
}
