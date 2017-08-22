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
            stack.PushRef(refs);
            if (refs == null) return;
            var cp = frame.method.Class().constantPool;
            var classRef = (ClassRef)cp.GetConstant(Index);
            var clas = classRef.ResolvedClass();
            if (!refs.IsInstanceOf(ref clas))
                throw new Exception("java.lang.ClassCastExcption");
        }
    }
}
