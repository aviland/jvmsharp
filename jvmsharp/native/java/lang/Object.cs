using System;

namespace jvmsharp.native.java.lang
{
  unsafe  class Object
    {
        public static void init()
        {
            Registry.Register("java/lang/Object", "getClass", "()Ljava/lang/Class;", getClass);
            Registry.Register("java/lang/Object", "hashCode", "()I", hashCode);
            Registry.Register("java/lang/Object", "clone", "()Ljava/lang/Object;", clone);
        }

        private static void getClass(ref rtda.Frame frame)
        {
            var thi = frame.LocalVars().GetThis();
            var clas = thi.clas.jClass;
            frame.OperandStack().PushRef(clas);
        }

        private static void hashCode(ref rtda.Frame frame)
        {
            var thi = frame.LocalVars().GetThis();
            var clas = thi.GetHashCode();
            frame.OperandStack().PushInt(clas);
        }

        private static void clone(ref rtda.Frame frame)
        {
            var thi = frame.LocalVars().GetThis();
            var cloneable = thi.Class().loader.LoadClass("java/lang/Cloneable");
            if (!thi.Class().isImplements(cloneable))
                throw new Exception("java.lang.CloneNotSupportedException");
            frame.OperandStack().PushRef(thi.Clone());
        }
    }
}
