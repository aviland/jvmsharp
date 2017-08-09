using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace jvmsharp.native.java.lang
{
    class Object
    {
        public static void init()
        {
            Registry.Register("java/lang/Object", "getClass", "()Ljava/lang/Class;", getClass);
        }

       private static void getClass(ref rtda.Frame frame)
        {
            var thi = frame.LocalVars().GetThis();
            var clas = thi.clas.jClass;
            frame.OperandStack().PushRef(clas);
        }
    }
}
