using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using jvmsharp.rtda;

namespace jvmsharp.native.sun.reflect
{
    class Reflection : Native
    {
        public void init()
        {
            Registry.Register("sun/reflect/Reflection", "getCallerClass", "()Ljava/lang/Class;", getCallerClass);
            Registry.Register("sun/reflect/Reflection", "getClassAccessFlags", "(Ljava/lang/Class;)I", getClassAccessFlags);
        }

        private void getClassAccessFlags(ref Frame frame)
        {
            var vars = frame.LocalVars();
            var type = vars.GetRef(0);
            var goClass = (rtda.heap.Class)type.Extra();
            var flags = goClass.accessFlags;
            var stack = frame.OperandStack();
            stack.PushInt(flags);
        }

        private void getCallerClass(ref Frame frame)
        {
            Frame callerFrame = frame.Thread().GetFrames()[2];// todo
            rtda.heap.Object callerClass = callerFrame.Method().Class().jClass;
            frame.OperandStack().PushRef(callerClass);
        }
    }
}
