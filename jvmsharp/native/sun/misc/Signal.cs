using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using jvmsharp.rtda;

namespace jvmsharp.native.sun.misc
{
    class Signal : Native
    {
        public void init()
        {
            Registry.Register("sun/misc/Signal", "findSignal", "(Ljava/lang/String;)I", findSignal);
            Registry.Register("sun/misc/Signal", "handle0", "(IJ)J", handle0);
        }

        private void handle0(ref Frame frame)
        {
            var vars = frame.LocalVars();
            vars.GetInt(0);
            vars.GetLong(1);
            var stack = frame.OperandStack();
            stack.PushLong(0);
        }

        private void findSignal(ref Frame frame)
        {
            var vars = frame.LocalVars();
            vars.GetRef(0);// name
            var stack = frame.OperandStack();
            stack.PushInt(0); // todo
        }
    }
}
