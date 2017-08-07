using jvmsharp.rtda;
using jvmsharp.rtda.heap;

namespace jvmsharp.instructions.references
{
    class INVOKE_STATIC : Index16Instruction
    {
  //      Method method;

        public override void Execute(ref Frame frame)
        {
            var cp = frame.Method().Class().ConstantPool();
            var k = cp.GetConstant(Index);
            var methodRef = (ConstantMethodRef)k;
            var resolvedMethod = methodRef.ResolvedMethod();
            if (!resolvedMethod.IsStatic())
                throw new System.Exception("java.lang.IncompatibleClassChangeError");

            Class clas = resolvedMethod.Class();
            if (!clas.InitStarted())
            {
                frame.RevertNextPC();
                classInit_logic.InitClass(ref frame.thread, ref clas);
                return;
            }

            invoke_logic.InvokeMethod(ref frame, ref resolvedMethod);
        }
    }
}
