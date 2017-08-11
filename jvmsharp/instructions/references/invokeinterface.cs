using System;
using jvmsharp.rtda;
using jvmsharp.rtda.heap;

namespace jvmsharp.instructions.references
{
    class INVOKE_INTERFACE : Instruction
    {
        uint index;

        public void FetchOperands(ref BytecodeReader reader)
        {
            index = reader.ReadUint16();
            reader.ReadUint8();//count
            reader.ReadUint8();//must be 0
        }

        public void Execute(ref Frame frame)
        {
            ConstantPool cp = frame.method.Class().constantPool;
            ConstantInterfaceMethodref methodRef = (ConstantInterfaceMethodref)cp.GetConstant(index);
            Method resolvedMethod = methodRef.ResolvedInterfaceMethod();
            if (resolvedMethod.IsStatic() || resolvedMethod.IsPrivate())
                throw new Exception("java.lang.IncompatibleClassChangeError");
            rtda.heap.Object refs = frame.OperandStack().GetRefFromTop(resolvedMethod.ArgSlotCount() - 1);
            if (refs == null)
                throw new Exception("java.lang.NullPointerException");
            if (!refs.clas.isImplements(methodRef.ResolvedClass()))
                throw new Exception("java.lang.IncompatibleClassChangeError");
            Method methodToBeInvoked = new ConstantMethodRef().lookupMethodInClass(ref refs.clas, methodRef.Name(), methodRef.Descriptor());
            if (methodToBeInvoked == null || methodToBeInvoked.IsAbstract())
                throw new Exception("java.lang.AbstractMethodError");
            if (methodToBeInvoked.IsPublic())
                throw new Exception("java.lang.IllegalAccessError");
            invoke_logic.InvokeMethod(ref frame, ref methodToBeInvoked);
        }
    }
}
