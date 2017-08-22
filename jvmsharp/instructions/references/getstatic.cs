using jvmsharp.rtda.heap;
using System;

namespace jvmsharp.instructions.references
{
  unsafe  class GET_STATIC : Index16Instruction
    {

        public override void Execute(ref rtda.Frame frame)
        {
            ConstantPool cp = frame.method.Class().constantPool;
            FieldRef fieldRef = (FieldRef)cp.GetConstant(Index);
          //  Console.WriteLine(fieldRef.field == null);
            Field field = fieldRef.ResolvedField();
            Class clas = field.Class();
          //  Console.WriteLine(clas.InitStarted());
            if (!clas.InitStarted())
            {
                frame.RevertNextPC();
                ClassInitLogic.InitClass(ref frame.thread, ref clas);
                return;
            }

            if (!field.IsStatic())
                throw new Exception("java.lang.IncompatibleClassChangeError");

            var descriptor = field.Descriptor();
            var slotId = field.slotId;
            rtda.heap.Slots slots = clas.staticVars;
            var stack = frame.OperandStack();
           // Console.WriteLine(raw[0]);
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
                    stack.PushRef(slots.GetRef(slotId));
                    break;
            }
        }
    }
}
