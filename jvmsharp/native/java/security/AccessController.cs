using jvmsharp.rtda;

namespace jvmsharp.native.java.security
{
    class AccessController : Native
    {
        public  void init()
        {
            Registry.Register("java/security/AccessController", "doPrivileged", "(Ljava/security/PrivilegedAction;)Ljava/lang/Object;", doPrivileged);
            Registry.Register("java/security/AccessController", "doPrivileged", "(Ljava/security/PrivilegedExceptionAction;)Ljava/lang/Object;", doPrivileged);
            Registry.Register("java/security/AccessController", "getStackAccessControlContext", "()Ljava/security/AccessControlContext;", getStackAccessControlContext);
        }

        private void getStackAccessControlContext(ref Frame frame)
        {
            frame.OperandStack().PushRef(null);
        }

        private void doPrivileged(ref Frame frame)
        {
            var action = frame.LocalVars().GetRef(0);
            var stack = frame.OperandStack();
            stack.PushRef(action);
            var method = action.Class().GetInstanceMethod("run", "()Ljava/lang/Object;"); // todo
            instructions.InvokeLogic.InvokeMethod(ref frame, ref method);
        }
    }
}
