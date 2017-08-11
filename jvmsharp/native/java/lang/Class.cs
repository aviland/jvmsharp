using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using jvmsharp.rtda;
using jvmsharp.rtda.heap;

namespace jvmsharp.native.java.lang
{
    class Class
    {
        public static void init()
        {
            Registry.Register("java/lang/Class", "getPrimitiveClass", "(Ljava/lang/String;)Ljava/lang/Class;", getPrimitiveClass);
            Registry.Register("java/lang/Class", "getName0", "()Ljava/lang/String;", getName0);
            Registry.Register("java/lang/Class", "desiredAssertionStatus0", "(Ljava/lang/Class;)Z", desiredAssertionStatus0);
        }
      /*  void getClassLoader0(ref rtda.Frame frame)
        {

            rtda.heap.Class clas = popClass(ref frame);

            var from = clas.LoadedFrom();


            var stack = frame.OperandStack();
            if (classpath.Classpath.IsBootClassPath(from))
            {
                stack.PushRef(null);
                return;
            }

            var clClass = ClassLoader.bootLoader.LoadClass("java/lang/ClassLoader");

            var getSysCl = clClass.GetStaticMethod("getSystemClassLoader", "()Ljava/lang/ClassLoader;");
            //      frame.Thread().InvokeMethod(getSysCl);
        }*/

        rtda.heap.Class popClass(ref rtda.Frame frame)
        {
            var vars = frame.LocalVars();
            var thi = vars.GetThis();
            return (rtda.heap.Class)thi.Extra();
        }

      unsafe  private static void getPrimitiveClass(ref Frame frame)
        {
            rtda.heap.Object nameObj = frame.LocalVars().GetRef(0);
            string name = StringPool.GoString(ref nameObj);

           rtda.heap.ClassLoader loader = frame.method.Class().loader;
            rtda.heap.Object clas = loader.LoadClass(name).jClass;

            frame.OperandStack().PushRef(clas);
        }

        private static void getName0(ref Frame frame)
        {
            var thi = frame.LocalVars().GetThis();
            var clas = (rtda.heap.Class)thi.extra;

            var name = clas.JavaName();
            var nameObj = StringPool.JString(ref clas.loader, name);
            frame.OperandStack().PushRef(nameObj);
        }

        unsafe private static void desiredAssertionStatus0(ref Frame frame)
        {
            bool b = false;
            frame.OperandStack().PushBoolean(b);
        }
    }
}