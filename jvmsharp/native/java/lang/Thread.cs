using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using jvmsharp.rtda;

namespace jvmsharp.native.java.lang
{
    class Thread : Native
    {
        public void init()
        {
            Registry.Register("java/lang/Thread", "currentThread", "()Ljava/lang/Thread;", currentThread);
            Registry.Register("java/lang/Thread", "setPriority0", "(I)V", setPriority0);
            Registry.Register("java/lang/Thread", "isAlive", "()Z", isAlive);
            Registry.Register("java/lang/Thread", "start0", "()V", start0);
        }

        private void start0(ref Frame frame)
        {
           
        }

        private void isAlive(ref Frame frame)
        {
            OperandStack stack = frame.OperandStack();
            stack.PushBoolean(false);
        }

        private void setPriority0(ref Frame frame)
        {
          
        }

        private void currentThread(ref Frame frame)
        {
            //jThread := frame.Thread().JThread()
            rtda.heap.ClassLoader classLoader = frame.Method().Class().Loader();
            rtda.heap.Class threadClass = classLoader.LoadClass("java/lang/Thread");
            rtda.heap.Object jThread = threadClass.NewObject();
            rtda.heap.Class threadGroupClass = classLoader.LoadClass("java/lang/ThreadGroup");
            rtda.heap.Object jGroup = threadGroupClass.NewObject();
            jThread.SetRefVar("group", "Ljava/lang/ThreadGroup;", ref jGroup);
            jThread.SetIntVar("priority", "I", 1);
            frame.OperandStack().PushRef(jThread);
        }
    }
}
