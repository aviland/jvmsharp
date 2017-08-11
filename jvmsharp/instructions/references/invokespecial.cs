using jvmsharp.rtda;
using jvmsharp.rtda.heap;
using System;

namespace jvmsharp.instructions.references
{
    class INVOKE_SPECIAL : Index16Instruction
    {
        unsafe public override void Execute(ref Frame frame)
        {
            Class currentClass = frame.method.Class();
            var cp = currentClass.constantPool;
            ConstantMethodRef methodRef = (ConstantMethodRef)cp.GetConstant(Index);
     
            var resolvedClass = methodRef.ResolvedClass();
            Method resolvedMethod = methodRef.ResolvedMethod();
            if (resolvedMethod.Name() == "<init>" && resolvedMethod.Class() != resolvedClass)
                throw new Exception("java.lang.NoSuchMethodError");
            if (resolvedMethod.IsStatic())
                throw new System.Exception("java.lang.IncompatibleClassChangeError");
            rtda.heap.Object refs = frame.OperandStack().GetRefFromTop(resolvedMethod.ArgSlotCount() - 1);
            if (refs == null)
                throw new Exception("java.lang.NullPointerException");
            if (resolvedMethod.IsProtected() && resolvedMethod.Class().isSuperClassOf(currentClass) && resolvedMethod.Class().getPackageName() != currentClass.getPackageName() && refs.clas != currentClass && !refs.clas.isSubClassOf(currentClass))
                throw new Exception("java.lang.IllegalAccessError");
 
            Method methodToBeInvoked = resolvedMethod;
            if (currentClass.IsSuper() && resolvedClass.isSuperClassOf(currentClass) && resolvedMethod.Name() != "<init>")
                methodToBeInvoked = new ConstantMethodRef().lookupMethodInClass(ref currentClass.superClass, methodRef.Name(), methodRef.Descriptor());

            if (methodToBeInvoked==null||methodToBeInvoked.IsAbstract())
                throw new Exception("java.lang.AbstractMethodError");
            invoke_logic.InvokeMethod(ref frame, ref methodToBeInvoked);

        }
    }
}
