using System;
using jvmsharp.rtda;

namespace jvmsharp.instructions.references
{
    class INVOKE_VIRTUAL : Index16Instruction
    {
        public override void Execute(ref Frame frame)
        {
            var currentClass = frame.Method().Class();
            var cp = currentClass.ConstantPool();
            var methodRef = (rtda.heap.ConstantMethodRef)cp.GetConstant(Index);
            var resolvedMethod = methodRef.ResolvedMethod();
            if (resolvedMethod.IsStatic())
                throw new Exception("java.lang.IncompatibleClassChangeError");
            var refs = frame.OperandStack().GetRefFromTop(resolvedMethod.ArgSlotCount() - 1);
            if (refs == null)
            {
                if (methodRef.Name() == "println")
                {
                    _println(ref frame.operandStack, methodRef.Descriptor());
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

        void _println(ref OperandStack stack, string descriptor)
        {
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
                default: throw new Exception("println:" + descriptor);
            }
            stack.PopRef();
        }
    }
}
