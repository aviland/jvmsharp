using jvmsharp.rtda;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace jvmsharp.native.java.lang
{
    unsafe class ClassLoader
    {
        public static void init()
        {
            Registry.Register("java/lang/ClassLoader", "defineClass1", "(Ljava/lang/String;[BIILjava/security/ProtectionDomain;Ljava/lang/String;)Ljava/lang/Class;", defineClass1);
            Registry.Register("java/lang/ClassLoader", "findBootstrapClass", "(Ljava/lang/String;)Ljava/lang/Class;", findBootstrapClass);
            Registry.Register("java/lang/ClassLoader", "findLoadedClass0", "(Ljava/lang/String;)Ljava/lang/Class;", findLoadedClass0);
        }

        static void defineClass1(ref rtda.Frame frame)
        {
            var vars = frame.LocalVars();
            var thi = vars.GetThis();
            var name = vars.GetRef(1);
            rtda.heap.Object byteArr = vars.GetRef(2);
            var off = vars.GetInt(3);
            var len = vars.GetInt(4);
            var goBytes = byteArr.GoBytes();
            goBytes = goBytes.Skip(off).Take(len).ToArray();
            throw new Exception(rtda.StringPool.GoString(ref name));
        }

        static void findBootstrapClass(ref rtda.Frame frame)
        {
            try
            {
                var vars = frame.LocalVars();
                var name = vars.GetRef(1);
                string className = rtda.heap.ClassNameHelper.DotToSlash(rtda.StringPool.GoString(ref name));
                var clas = rtda.heap.ClassLoader.bootLoader.LoadClass(className);
                var stack = frame.OperandStack();
                stack.PushRef(clas.jClass);
            }
            catch (Exception)
            {
                frame.OperandStack().PushRef(null);
            }
        }
        static void findLoadedClass0(ref rtda.Frame frame)
        {
            LocalVars vars = frame.LocalVars();
            rtda.heap.Object thi = vars.GetThis();
            rtda.heap.Object name = vars.GetRef(1);
            var className = rtda.heap.ClassNameHelper.DotToSlash(rtda.StringPool.GoString(ref name));
            if (isAppClassLoader(thi))
            {
                rtda.heap.Class clas = rtda.heap.ClassLoader.bootLoader.FindLoadedClass(className);
                if (clas != null)
                    frame.OperandStack().PushRef(clas.jClass);
                else frame.OperandStack().PushRef(null);
                return;
            }
            frame.OperandStack().PushRef(null);
        }
        static bool isAppClassLoader(rtda.heap.Object loader)
        {
            return loader.Class().name == "sun/misc/Launcher$AppClassLoader";
        }
    }
}
