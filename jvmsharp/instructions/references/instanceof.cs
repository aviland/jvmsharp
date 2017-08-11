using jvmsharp.rtda;

namespace jvmsharp.instructions.references
{
    unsafe class INSTANCE_OF : Index16Instruction
    {
        public override void Execute(ref Frame frame)
        {
            int i0 = 0, i1 = 1;
            var stack = frame.OperandStack();
            var refs = stack.PopRef();
            if (refs == null)
            {
                stack.PushInt(i0);
                return;
            }
            var cp = frame.method.Class().constantPool;
            rtda.heap.ConstantClassRef classRef = (rtda.heap.ConstantClassRef)cp.GetConstant(Index);
            var clas = classRef.ResolvedClass();
            if (refs.IsInstanceOf(ref clas))
                stack.PushInt(i1);
            else stack.PushInt(i0);
        }
    }
}
