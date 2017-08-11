using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace jvmsharp.native.sun.misc
{
    class VM
    {
        public static void init()
        {
            Registry.Register("sun/misc/VM", "initialize", "()V", initialize);
        }
        static void initialize(ref rtda.Frame frame)
        {
            rtda.heap.Class vmClass = frame.method.Class();
            rtda.heap.Object savedProps = vmClass.GetRefVar("savedProps", "Ljava/util/Properties;");
            var key = rtda.StringPool.JString(ref vmClass.loader, "foo");
            var val = rtda.StringPool.JString(ref vmClass.loader, "bar");
            frame.OperandStack().PushRef(savedProps);
            frame.OperandStack().PushRef(key);
            frame.OperandStack().PushRef(val);
            var propsClass = vmClass.Loader().LoadClass("java/util/Properties");
            var setPropMethod = propsClass.GetInstanceMethod("setProperty", "(Ljava/lang/String;Ljava/lang/String;)Ljava/lang/Object;");
            instructions.invoke_logic.InvokeMethod(ref frame, ref setPropMethod);
        }
    }
}
