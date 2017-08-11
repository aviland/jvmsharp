using System;
using jvmsharp.rtda;
using jvmsharp.rtda.heap;

namespace jvmsharp.instructions.references
{
    unsafe class INVOKE_VIRTUAL : Index16Instruction
    {
        public override void Execute(ref Frame frame)
        {
            Class currentClass = frame.method.Class();
            ConstantPool cp = currentClass.constantPool;
            ConstantMethodRef methodRef = (ConstantMethodRef)cp.GetConstant(Index);
            Method resolvedMethod = methodRef.ResolvedMethod();
            if (resolvedMethod.IsStatic())
                throw new Exception("java.lang.IncompatibleClassChangeError");
            rtda.heap.Object refs = frame.OperandStack().GetRefFromTop(resolvedMethod.ArgSlotCount() - 1);
            if (refs == null)
            {
           //     Console.WriteLine("*************************"+methodRef.Name());
                if (methodRef.Name() == "println")
                {
                    println(ref frame.operandStack, methodRef.Descriptor());
                    return;
                }
                if (methodRef.Name() == "print")
                {
                    print(ref frame.operandStack, methodRef.Descriptor());
                    return;
                }
                throw new Exception("java.lang.NullPointerException");
            }

            if (resolvedMethod.IsProtected() && resolvedMethod.Class().isSuperClassOf(currentClass) && resolvedMethod.Class().getPackageName() != currentClass.getPackageName() && refs.clas != currentClass && !refs.clas.isSubClassOf(currentClass))
                throw new Exception("java.lang.IllegalAccessError");
            var methodToBeInvoked = new rtda.heap.ConstantMethodRef().lookupMethodInClass(ref refs.clas, methodRef.Name(), methodRef.Descriptor());
            if (methodToBeInvoked == null || methodToBeInvoked.IsAbstract())
                throw new Exception("java.lang.AbstractMethodError");
            invoke_logic.InvokeMethod(ref frame, ref methodToBeInvoked);
        }

        void println(ref OperandStack stack, string descriptor)
        {
      //     Console.WriteLine("_println:" + descriptor);
            switch (descriptor)
            {
                case "(Z)V": Console.WriteLine(stack.PopInt() != 0); break;
                case "(C)V": Console.WriteLine(stack.PopInt()); break;
                case "(B)V": Console.WriteLine(stack.PopInt()); break;
                case "(S)V": Console.WriteLine(stack.PopInt()); break;
                case "(I)V": Console.WriteLine(stack.PopInt()); break;
                case "(J)V": Console.WriteLine(stack.PopLong()); break;
                case "(F)V": Console.WriteLine(stack.PopFloat()); break;
                case "(D)V": Console.WriteLine(stack.PopDouble()); break;
                case "(Ljava/lang/String;)V":
                    var jStr = stack.PopRef();
                    var goStr = StringPool.GoString(ref jStr);
              //      Console.WriteLine(goStr);
                    break;
                default: throw new Exception("println:" + descriptor);
            }
            stack.PopRef();
        }

        void print(ref OperandStack stack, string descriptor)
        {
            // Console.WriteLine("_println:" + descriptor);
            switch (descriptor)
            {
                case "(Z)V": Console.Write(stack.PopInt() != 0); break;
                case "(C)V": Console.Write(stack.PopInt()); break;
                case "(B)V": Console.Write(stack.PopInt()); break;
                case "(S)V": Console.Write(stack.PopInt()); break;
                case "(I)V": Console.Write(stack.PopInt()); break;
                case "(J)V": Console.Write(stack.PopLong()); break;
                case "(F)V": Console.Write(stack.PopFloat()); break;
                case "(D)V": Console.Write(stack.PopDouble()); break;
                case "(Ljava/lang/String;)V":
                    var jStr = stack.PopRef();
                    var goStr = StringPool.GoString(ref jStr);
                    Console.Write(goStr);
                    break;
                default: throw new Exception("println:" + descriptor);
            }
            stack.PopRef();
        }
    }
}
