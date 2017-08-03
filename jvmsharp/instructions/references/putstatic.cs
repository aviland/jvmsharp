using System;
using jvmsharp.rtda;
using jvmsharp.rtda.heap;
namespace jvmsharp.instructions.references
{
    class PUT_STATIC : Index16Instruction
    {
        //rtda.heap.Field field;

        public override void Execute(ref Frame frame)
        {
          
            Method currentMethod = frame.Method();
            
            Class currentClass = currentMethod.Class();
          ConstantPool cp = currentClass.ConstantPool();
        //    Console.WriteLine("fieldRef " + cp.GetConstant(Index).GetType().Name);
            ConstantFieldRef fieldRef = (ConstantFieldRef)cp.GetConstant(Index);
          
            //     Console.WriteLine("fieldRef " + fieldRef == null);
            //   Console.WriteLine("fieldRef " + fieldRef.CP().clas.name);
            Field field = fieldRef.ResolvedField();
            Class clas = field.Class();
            if (!clas.InitStarted())
            {
                frame.RevertNextPC();
                classInit_logic.InitClass(ref frame.thread, ref clas);
                return;
            }

            if (!field.IsStatic())
                throw new Exception("java.lang.IncompatibleClassChangeError");
            if (field.IsFinal())
                if (currentClass != clas || currentMethod.Name() != "<clinit>")
                    throw new Exception("java.lang.IllegalAccessError");
            var descriptor = field.Descriptor();
            var slotId = field.slotId;
            Slots slots = clas.staticVars;
            OperandStack stack = frame.OperandStack();
          //  Console.WriteLine("descriptor[0]"+ descriptor[0]+ slots.slots.Length+"\t"+slotId);
            switch (descriptor[0])
            {
                case 'Z':
                case 'B':
                case 'C':
                case 'S':
                case 'I':
                    slots.SetInt(slotId, stack.PopInt());
                    break;
                case 'F':
                    slots.SetFloat(slotId, stack.PopFloat());
                    break;
                case 'J':
                    slots.SetLong(slotId, stack.PopLong());
                    break;
                case 'D':
                    slots.SetDouble(slotId, stack.PopDouble());
                    break;
                case 'L':
                case '[':
                    rtda.heap.Object rho = stack.PopRef();
                    slots.SetRef(slotId, ref rho);
                    break;
            }
        }
    }
}
