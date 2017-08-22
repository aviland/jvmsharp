using jvmsharp.rtda;
using jvmsharp.rtda.heap;
using System;

namespace jvmsharp.instructions.references
{
    unsafe class ANEW_ARRAY : Index16Instruction
    {
        public override void Execute(ref Frame frame)
        {
            ConstantPool cp = frame.method.Class().constantPool;
            ClassRef classRef = (ClassRef)cp.GetConstant(Index);
            Class componentClass = classRef.ResolvedClass();

            OperandStack stack = frame.OperandStack();
            int count =stack.PopInt();
            if (count < 0)
                throw new Exception("java.lang.NegativeArraySizeException");

            Class arrClass = componentClass.ArrayClass();
            rtda.heap.Object arr = arrClass.NewArray((uint)count);
            frame.OperandStack().PushRef(arr);
        }
    }
}
