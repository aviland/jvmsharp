using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace jvmsharp.native.java.lang
{
    class String
    {
        public static void init()
        {
            Registry.Register("java/lang/String", "intern", "()Ljava/lang/String;", intern);
        }

        static void intern(ref rtda.Frame frame)
        {
            var thi = frame.LocalVars().GetThis();
            var interned = rtda.StringPool.InternString(ref thi);
            frame.OperandStack().PushRef(interned);
        }
    }
}
