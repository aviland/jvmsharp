using System;
using jvmsharp.rtda;

namespace jvmsharp.native.java.lang
{
    class Object
    {
        public static void init()
        {
            Registry.Register("java/lang/Object", "getClass", "()Ljava/lang/Class;", getClass);
            Registry.Register("java/lang/Object", "hashCode", "()I", hashCode);
            Registry.Register("java/lang/Object", "clone", "()Ljava/lang/Object;", clone);
            Registry.Register("java/lang/Object", "notifyAll", "()V", notifyAll);
        }

        private static void notifyAll(ref Frame frame)
        {
            // do nothing
        }

        private static void getClass(ref rtda.Frame frame)
        {
            rtda.heap.Object thi = frame.LocalVars().GetThis();
            rtda.heap.Object clas = thi.clas.jClass;
            frame.OperandStack().PushRef(clas);
        }

        private static void hashCode(ref rtda.Frame frame)
        {
            rtda.heap.Object thi = frame.LocalVars().GetThis();
            int hash = thi.GetHashCode();
            frame.OperandStack().PushInt(hash);
        }

        private static void clone(ref rtda.Frame frame)
        {
            rtda.heap.Object thi = frame.LocalVars().GetThis();
            var cloneable = thi.clas.loader.LoadClass("java/lang/Cloneable");
            if (!thi.clas.isImplements(cloneable))
                throw new Exception("java.lang.CloneNotSupportedException");
            frame.OperandStack().PushRef(thi.Clone());
        }
    }
}
