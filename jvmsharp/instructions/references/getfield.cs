using System;
using jvmsharp.rtda;
using jvmsharp.rtda.heap;

namespace jvmsharp.instructions.references
{
    class GET_FIELD : Index16Instruction
    {
        public override void Execute(ref Frame frame)
        {
            var cp = frame.Method().Class().ConstantPool();
            var fieldRef = (ConstantFieldRef)cp.GetConstant(Index);
            var field = fieldRef.ResolvedField();
            if (field.IsStatic())
                throw new Exception("java.lang.IncompatibleClassChangeError");

            OperandStack stack = frame.OperandStack();
            var refs = stack.PopRef();
            if (refs == null)
                throw new Exception("java.lang.NullPointerException");

            var descriptor = field.Descriptor();
            var slotId = field.slotId;
            Slots slots = refs.Fields();

            switch (descriptor[0])
            {
                case 'Z':
                case 'B':
                case 'C':
                case 'S':
                case 'I':
                    stack.PushInt(slots.GetInt(slotId));
                    break;
                case 'F':
                    stack.PushFloat(slots.GetFloat(slotId));
                    break;
                case 'J':
                    stack.PushLong(slots.GetLong(slotId));
                    break;
                case 'D':
                    stack.PushDouble(slots.GetDouble(slotId));
                    break;
                case 'L':
                case '[':
                    rtda.heap.Object rho = slots.GetRef(slotId);
                    stack.PushRef(rho);
                    break;
            }

        }
    }
}
