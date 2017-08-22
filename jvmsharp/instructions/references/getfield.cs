using System;
using jvmsharp.rtda;
using jvmsharp.rtda.heap;

namespace jvmsharp.instructions.references
{
    unsafe class GET_FIELD : Index16Instruction
    {
        public override void Execute(ref Frame frame)
        {
            ConstantPool cp = frame.method.Class().constantPool;
            FieldRef fieldRef = (FieldRef)cp.GetConstant(Index);
            var field = fieldRef.ResolvedField();
            if (field.IsStatic())
                throw new Exception("java.lang.IncompatibleClassChangeError");

            OperandStack stack = frame.OperandStack();
            var refs = frame.OperandStack().PopRef();
            if (refs == null)
                throw new Exception("java.lang.NullPointerException");

            var descriptor = field.Descriptor();
            var slotId = field.slotId;
            Slots slots = refs.Fields();
            //Console.Write(raw[0]);
            switch (descriptor[0])
            {
                case 'Z':
                case 'B':
                case 'C':
                case 'S':
                case 'I':
                    frame.OperandStack().PushInt(slots.GetInt(slotId));
                    break;
                case 'F':
                    frame.OperandStack().PushFloat(slots.GetFloat(slotId));
                    break;
                case 'J':
                    frame.OperandStack().PushLong(slots.GetLong(slotId));
                    break;
                case 'D':
                    frame.OperandStack().PushDouble(slots.GetDouble(slotId));
                    break;
                case 'L':
                case '[':
                    frame.OperandStack().PushRef(slots.GetRef(slotId));
                    break;
            }

        }
    }
}
