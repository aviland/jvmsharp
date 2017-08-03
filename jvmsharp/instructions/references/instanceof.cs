using jvmsharp.rtda;

namespace jvmsharp.instructions.references
{
    class INSTANCE_OF : Index16Instruction
    {
        public override void Execute(ref Frame frame)
        {
            var stack = frame.OperandStack();
            var refs = stack.PopRef();
            if (refs == null)
            {
                stack.PushInt(0);
                return;
            }
            var cp = frame.Method().Class().ConstantPool();
            var classRef = (rtda.heap.ConstantClassRef)cp.GetConstant(Index);
            var clas = classRef.ResolvedClass();
            if (refs.IsInstanceOf(ref clas))
                stack.PushInt(1);
            else stack.PushInt(0);
        }
    }
}
