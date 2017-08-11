using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace jvmsharp.native.java.lang
{
 unsafe   class System
    {
        public static void init()
        {
            Registry.Register("java/lang/System", "arraycopy", "(Ljava/lang/Object;ILjava/lang/Object;II)V", arraycopy);
        }

        static void arraycopy(ref rtda.Frame frame)
        {
            var vars = frame.LocalVars();
            var src = vars.GetRef(0);
            var srcPos = vars.GetInt(1);
            var dest = vars.GetRef(2);
            var destPos = vars.GetInt(3);
            var length = vars.GetInt(4);

            // NullPointerException
            if (src == null || dest == null)
                throw new Exception("java.lang.NullPointerException"); // todo
            // ArrayStoreException
            if (!checkArrayCopy(src, dest))
                throw new Exception("java.lang.ArrayStoreException");
            // IndexOutOfBoundsException
            if (srcPos < 0 || destPos < 0 || length < 0 || srcPos + length >src.ArrayLength() || destPos + length > dest.ArrayLength())
                throw new Exception("java.lang.IndexOutOfBoundsException"); // todo
            rtda.heap.Array.ArrayCopy(ref src, ref dest, ref srcPos,ref  destPos, ref length);
        }

        static bool checkArrayCopy(rtda.heap.Object src, rtda.heap.Object dest)
        {
            rtda.heap.Class srcClass = src.Class();
            rtda.heap.Class destClass = dest.Class();
            if (!srcClass.IsArray() || !destClass.IsArray())
                return false;
            if (srcClass.ComponentClass().IsPrimitive() || destClass.ComponentClass().IsPrimitive())
                return srcClass == destClass;
            return true;
        }
    }
}
