using jvmsharp.classpath;
using jvmsharp.rtda;
using jvmsharp.rtda.heap;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace jvmsharp
{
    class Jvm
    {
        Cmd cmd;
        ClassLoader classLoader;
        Thread mainThread;

        public Jvm(Cmd cmd)
        {
            RegistNative();//注册本地方法
            Classpath cp = new Classpath().Parse(cmd.XjreOption, cmd.cpOption);
            ClassLoader classLoader = new ClassLoader().newClassLoader(ref cp, cmd.verboseClassFlag);
            this.cmd = cmd;
            this.classLoader = classLoader;
            this.mainThread = new rtda.Thread().newThread();
        }

        /*    static void startJVM()
            {
                rtda.Frame frame = new rtda.Frame(100, 100);
                rtda.LocalVars localVars = frame.LocalVars();
                rtda.OperandStack operandStack = frame.OperandStack();
                testOperandStack(ref frame);
                Console.WriteLine(frame.operandStack.PopRef());
                Console.WriteLine(frame.operandStack.PopDouble());
                Console.WriteLine(frame.operandStack.PopFloat());
                Console.WriteLine(frame.operandStack.PopLong());
                Console.WriteLine(frame.operandStack.PopLong());
                Console.WriteLine(frame.operandStack.PopInt());
                Console.WriteLine(frame.operandStack.PopInt());
            }

            static void testOperandStack(ref rtda.Frame frame)
            {
                OperandStack ops = frame.operandStack;
                ops.PushInt(100);
                ops.PushInt(-100);
                ops.PushLong(299887789);
                ops.PushLong(-299887789);
                ops.PushFloat(3.1415926f);
                ops.PushDouble(2.71822432523);
                ops.PushRef(null);

            }*/

        static void RegistNative()
        {
            new native.java.lang.Class().init();
            native.java.lang.Object.init();
            native.java.lang.System.init();
            native.java.lang.Float.init();
            native.java.lang.Double.init();
            native.java.lang.String.init();
            new native.java.lang.Thread().init();
            new native.java.lang.Throwable().init();

            native.java.io.FileOutputStream.init();
            native.java.io.FileDescriptor.init();
            native.java.io.FileInputStream.init();
            new native.sun.io.Win32ErrorMode().init();
            new native.sun.misc.VM().init();
            new native.sun.misc.Unsafe().init();
            new native.sun.misc.UnsafeMem().init();
            new native.sun.misc.Signal().init();
            new native.sun.reflect.Reflection().init();
            new native.sun.reflect.NativeConstructorAccessorImpl().init();
            new native.java.security.AccessController().init();


        }

        internal void start()
        {
            //   startJVM();
            initVM();
            execMain();
        }

        private void initVM()
        {
            Class vmClass = classLoader.LoadClass("sun/misc/VM");
            instructions.ClassInitLogic.InitClass(ref mainThread, ref vmClass);
            new interpreter().interpret(ref mainThread, cmd.verboseInstFlag);
        }

        private void execMain()
        {
            var className = cmd.classes.Replace('.', '/');
            var mainClass = classLoader.LoadClass(className);
            var mainMethod = mainClass.GetMainMethod();
            if (mainMethod == null)
            {
                Console.WriteLine("Main method not found in class " + cmd.classes);
                return;
            }

            var argsArr = createArgsArray();
            var frame = mainThread.newFrame(ref mainMethod);
            frame.LocalVars().SetRef(0, ref argsArr);
            mainThread.PushFrame(frame);
            new interpreter().interpret(ref mainThread, cmd.verboseInstFlag);
        }

        private rtda.heap.Object createArgsArray()
        {
            Class stringClass = classLoader.LoadClass("java/lang/String");
            uint argsLen = (uint)(cmd.args.Length);
            rtda.heap.Object argsArr = stringClass.ArrayClass().NewArray(argsLen);
            rtda.heap.Object[] jArgs = argsArr.Refs();
            for (int i = 0; i < cmd.args.Length; i++)
            {
                jArgs[i] = rtda.StringPool.JString(classLoader, cmd.args[i]);
            }
            return argsArr;
        }
    }
}
