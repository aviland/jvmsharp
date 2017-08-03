using System;
using jvmsharp.rtda;
using jvmsharp.rtda.heap;

namespace jvmsharp.instructions.references
{
    class CHECK_CAST : Index16Instruction
    {
        public override void Execute(ref Frame frame)
        {
            var stack = frame.OperandStack();
            var refs = stack.PopRef();
            stack.PushRef(ref refs);
            if (refs == null) return;
            var cp = frame.Method().Class().ConstantPool();
            var classRef = (ConstantClassRef)cp.GetConstant(Index);
            var clas = classRef.ResolvedClass();
            if (!refs.IsInstanceOf(ref clas))
                throw new Exception("java.lang.ClassCastExcption");
        }
    }
}
