using jvmsharp.rtda;
using jvmsharp.rtda.heap;

namespace jvmsharp.instructions.references
{
    class INVOKE_STATIC : Index16Instruction
    {
        //      Method method;
        public override void Execute(ref Frame frame)
        {
            ConstantPool cp = frame.method.Class().constantPool;
            MethodRef methodRef = (MethodRef)cp.GetConstant(Index);
            Method resolvedMethod = methodRef.ResolvedMethod();
            if (!resolvedMethod.IsStatic())
                throw new System.Exception("java.lang.IncompatibleClassChangeError");

            Class clas = resolvedMethod.Class();
            if (!clas.InitStarted())
            {
                frame.RevertNextPC();
                ClassInitLogic.InitClass(ref frame.thread, ref clas);
                return;
            }

            InvokeLogic.InvokeMethod(ref frame, ref resolvedMethod);
        }
    }
}
