using System;
using jvmsharp.rtda;
using jvmsharp.rtda.heap;
using System.Linq;

namespace jvmsharp.native.java.lang
{
    struct StackTraceElement
    {
        string fileName;
        string className;
        string methodName;
        int lineNumber;

        string String()
        {
            return className + "." + methodName + "(" + fileName + ":" + lineNumber + ")";
        }

        public StackTraceElement(string fileName, string className, string methodName, int lineNumber)
        {
            this.fileName = fileName;
            this.className = className;
            this.methodName = methodName;
            this.lineNumber = lineNumber;
        }
    }

    class Throwable:Native
    {
        public void init()
        {
            Registry.Register("java/lang/Throwable", "fillInStackTrace", "(I)Ljava/lang/Throwable;", fillInStackTrace);
        }

      internal   void fillInStackTrace(ref rtda.Frame frame)
        {
            rtda.heap.Object thi = frame.LocalVars().GetThis();
            frame.OperandStack().PushRef(thi);
            object stes = createStackTraceElements(ref thi, ref frame.thread);
            thi.SetExtra( stes);
        }

        private  StackTraceElement[] createStackTraceElements(ref rtda.heap.Object tObj, ref rtda.Thread thread)
        {
            var skip = distanceToObject(ref tObj.clas) + 2;
            Frame[] frames = thread.GetFrames().Skip(skip).ToArray();
            var stes = new StackTraceElement[frames.Length];
            for (int i = 0; i < stes.Length; i++)
            {
                stes[i] = createStackTraceElement(ref frames[i]);
            }
            return stes;
        }

        private  StackTraceElement createStackTraceElement(ref Frame frame)
        {
            Method method = frame.Method();
           rtda.heap.Class clas = method.Class();
            return new StackTraceElement(clas.SourceFile(), clas.JavaName(), method.Name(), method.GetLineNumber(frame.NextPC() - 1));
        }

        private  int distanceToObject(ref rtda.heap.Class clas)
        {
            int distance = 0;
            for (rtda.heap.Class c = clas.SuperClass(); c != null; c = c.SuperClass())
            {
                distance++;
            }
            return distance;
        }
    }
}
