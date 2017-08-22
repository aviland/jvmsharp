using System;
using jvmsharp.rtda;
using jvmsharp.rtda.heap;

namespace jvmsharp.instructions.references
{
    unsafe class PUT_STATIC : Index16Instruction
    {
        public override void Execute(ref Frame frame)
        {
            Method currentMethod = frame.method;
            Class currentClass = currentMethod.Class();
            ConstantPool cp = currentClass.constantPool;
            FieldRef fieldRef = (FieldRef)cp.GetConstant(Index);
            Field field = fieldRef.ResolvedField();
            Class clas = field.clas;
            if (!clas.InitStarted())
            {
                frame.RevertNextPC();
                ClassInitLogic.InitClass(ref frame.thread, ref clas);
                return;
            }

            if (!field.IsStatic())
                throw new Exception("java.lang.IncompatibleClassChangeError");
            if (field.IsFinal())
                if (currentClass != clas || currentMethod.Name() != "<clinit>")
                    throw new Exception("java.lang.IllegalAccessError");
            var descriptor = field.Descriptor();
            uint slotId = field.slotId;
      //        Slots slots = clas.staticVars;
            OperandStack stack = frame.OperandStack();
            switch (descriptor[0])
            {
                case 'Z':
                case 'B':
                case 'C':
                case 'S':
                case 'I':
                    clas.staticVars.SetInt(slotId, stack.PopInt());
                    break;
                case 'F':
                    clas.staticVars.SetFloat(slotId, stack.PopFloat());
                    break;
                case 'J':
                    clas.staticVars.SetLong(slotId, stack.PopLong());
                    break;
                case 'D':
                    clas.staticVars.SetDouble(slotId, stack.PopDouble());
                    break;
                case 'L':
                case '[':
                    clas.staticVars.SetRef(slotId, stack.PopRef());
                    break;
            }
        }
    }
}
