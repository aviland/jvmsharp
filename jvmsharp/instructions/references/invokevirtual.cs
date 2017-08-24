using System;
using jvmsharp.rtda;
using jvmsharp.rtda.heap;

namespace jvmsharp.instructions.references
{
     class INVOKE_VIRTUAL : Index16Instruction
    {
        public override void Execute(ref Frame frame)
        {
            Class currentClass = frame.method.Class();
            ConstantPool cp = currentClass.constantPool;
            MethodRef methodRef = (MethodRef)cp.GetConstant(Index);
            Method resolvedMethod = methodRef.ResolvedMethod();
            if (resolvedMethod.IsStatic())
                throw new Exception("java.lang.IncompatibleClassChangeError");
            rtda.heap.Object refs = frame.OperandStack().GetRefFromTop(resolvedMethod.ArgSlotCount() - 1);
            if (refs == null)
                throw new Exception("java.lang.NullPointerException");
            if (resolvedMethod.IsProtected() && resolvedMethod.Class().IsSuperClassOf(currentClass) && resolvedMethod.Class().getPackageName() != currentClass.getPackageName() && refs.clas != currentClass && !refs.clas.IsSubClassOf(currentClass))
            {
                if (!(refs.clas.IsArray() && resolvedMethod.Name() == "clone"))
                    throw new Exception("java.lang.IllegalAccessError");
            }
            Method methodToBeInvoked = MethodLookup.lookupMethodInClass(ref refs.clas, methodRef.Name(), methodRef.Descriptor());
            if (methodToBeInvoked == null || methodToBeInvoked.IsAbstract())
                throw new Exception("java.lang.AbstractMethodError");
            InvokeLogic.InvokeMethod(ref frame, ref methodToBeInvoked);
        }
    }
}
