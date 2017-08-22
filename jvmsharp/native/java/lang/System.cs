using System;
using System.Collections.Generic;
using jvmsharp.rtda;
using jvmsharp.instructions;

namespace jvmsharp.native.java.lang
{
    class System
    {
        static Dictionary<string, string> _sysProps = new Dictionary<string, string>{
            { "java.version","1.8.0"},
            { "java.vendor","jvm.go"},
            {"java.vendor.url",  "https://github.com/aviland/jvmsharp"},
            {"java.home",  "todo"},
            { "java.awt.graphicsenv","sun.awt.CGraphicsEnvironment" },
            //      {  "os.name",              runtime.GOOS },   // todo
            //	 {  "os.arch",              runtime.GOARCH}, // todo
            {"os.version","" },             // todo
            {"file.separator", "/"},            // todo os.PathSeparator
            {  "path.separator",  ":"},            // todo os.PathListSeparator
            {  "line.separator",       "\n"},           // todo
            {  "user.name",        ""},             // todo
            {  "user.home",           ""},             // todo
            {  "user.dir",            "."},            // todo
            {  "user.country",         "CN" },           // todo
            { "file.encoding",   "UTF-8" },
            {  "sun.stdout.encoding","UTF-8" },
            { "sun.stderr.encoding","UTF-8"}
        };

        public static void init()
        {
            Registry.Register("java/lang/System", "arraycopy", "(Ljava/lang/Object;ILjava/lang/Object;II)V", arraycopy);
            Registry.Register("java/lang/System", "setOut0", "(Ljava/io/PrintStream;)V", setOut0);
            Registry.Register("java/lang/System", "setIn0", "(Ljava/io/InputStream;)V", setIn0);
            Registry.Register("java/lang/System", "initProperties", "(Ljava/util/Properties;)Ljava/util/Properties;", initProperties); 
                  Registry.Register("java/lang/System", "setErr0", "(Ljava/io/PrintStream;)V", setErr0);
        }

        private static void initProperties(ref Frame frame)
        {
            var vars = frame.LocalVars();
            var props = vars.GetRef(0);
            var stack = frame.OperandStack();
            stack.PushRef(props);
            // public synchronized Object setProperty(String key, String value)
            var setPropMethod = props.Class().GetInstanceMethod("setProperty", "(Ljava/lang/String;Ljava/lang/String;)Ljava/lang/Object;");
            var thread = frame.Thread();

            foreach (string key in _sysProps.Keys)
            {
                rtda.heap.Object jKey = StringPool.JString( frame.Method().Class().loader, key);
                rtda.heap.Object jVal = StringPool.JString( frame.Method().Class().loader, _sysProps[key]);
                var ops = new rtda.OperandStack().newOperandStack(3);
                ops.PushRef(props);
                ops.PushRef(jKey);
                ops.PushRef(jVal);
                var shimFrame = rtda.ShimFrame.NewShimFrame(thread, ops);
                thread.PushFrame( shimFrame);
                InvokeLogic.InvokeMethod(ref shimFrame, ref setPropMethod);
            }
        }

        private static void setIn0(ref Frame frame)
        {
            var ins = frame.LocalVars().GetRef(0);
            rtda.heap.Class sysClass = frame.Method().Class();
            sysClass.SetRefVar("in", "Ljava/io/InputStream;", ins);
        }

        private static void setOut0(ref Frame frame)
        {
            var outs = frame.LocalVars().GetRef(0);
            rtda.heap.Class sysClass = frame.Method().Class();
            sysClass.SetRefVar("out", "Ljava/io/PrintStream;", outs);
        }

        private static void setErr0(ref Frame frame)
        {
            rtda.heap.Object err = frame.LocalVars().GetRef(0);
            rtda.heap.Class sysClass = frame.Method().Class();
            sysClass.SetRefVar("err", "Ljava/io/PrintStream;", err);
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
            if (srcPos < 0 || destPos < 0 || length < 0 || srcPos + length > src.ArrayLength() || destPos + length > dest.ArrayLength())
                throw new Exception("java.lang.IndexOutOfBoundsException"); // todo
            rtda.heap.Object.ArrayCopy(ref src, ref dest, ref srcPos, ref destPos, ref length);
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
