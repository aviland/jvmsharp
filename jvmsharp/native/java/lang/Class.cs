using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using jvmsharp.rtda;
using jvmsharp.rtda.heap;

namespace jvmsharp.native.java.lang
{
  partial  class Class : Native
    {
        const string fieldConstructorDescriptor = "" + "(Ljava/lang/Class;" + "Ljava/lang/String;" + "Ljava/lang/Class;" + "II" + "Ljava/lang/String;" + "[B)V";
        const string constructorConstructorDescriptor = "" + "(Ljava/lang/Class;" + "[Ljava/lang/Class;" + "[Ljava/lang/Class;" + "II" + "Ljava/lang/String;" + "[B[B)V";

        public void init()
        {
            Registry.Register("java/lang/Class", "getPrimitiveClass", "(Ljava/lang/String;)Ljava/lang/Class;", getPrimitiveClass);
            Registry.Register("java/lang/Class", "getName0", "()Ljava/lang/String;", getName0);
            Registry.Register("java/lang/Class", "desiredAssertionStatus0", "(Ljava/lang/Class;)Z", desiredAssertionStatus0);
            Registry.Register("java/lang/Class", "getDeclaredFields0", "(Z)[Ljava/lang/reflect/Field;", getDeclaredFields0);
            Registry.Register("java/lang/Class", "isPrimitive", "()Z", isPrimitive);
            Registry.Register("java/lang/Class", "forName0", "(Ljava/lang/String;ZLjava/lang/ClassLoader;Ljava/lang/Class;)Ljava/lang/Class;", forName0);
            Registry.Register("java/lang/Class", "isInterface", "()Z", isInterface);
            Registry.Register("java/lang/Class", "getDeclaredConstructors0", "(Z)[Ljava/lang/reflect/Constructor;", getDeclaredConstructors0);
            Registry.Register("java/lang/Class", "getModifiers", "()I", getModifiers);
            Registry.Register("java/lang/Class", "getSuperclass", "()Ljava/lang/Class;", getSuperclass); 
                Registry.Register("java/lang/Class", "getInterfaces0", "()[Ljava/lang/Class;", getInterfaces0);
        }

        private void getInterfaces0(ref Frame frame)
        {
            LocalVars vars = frame.LocalVars();
            rtda.heap.Object thi = vars.GetThis();
            rtda.heap.Class clas = (rtda.heap.Class)thi.Extra();
            rtda.heap.Class[] interfaces = clas.Interfaces();
            rtda.heap.Object classArr = ClassHelper.toClassArr(ref clas.loader, interfaces);

            OperandStack stack = frame.OperandStack();
            stack.PushRef(classArr);
        }

        private void getSuperclass(ref Frame frame)
        {
            LocalVars vars = frame.LocalVars();
            rtda.heap.Object thi = vars.GetThis();
            rtda.heap.Class clas = (rtda.heap.Class)thi.Extra();
            rtda.heap.Class superClass = clas.SuperClass();

            OperandStack stack = frame.OperandStack();
            if (superClass != null)
                stack.PushRef(superClass.JClass());
            else
                stack.PushRef(null);
        }

        private void getModifiers(ref Frame frame)
        {
            LocalVars vars = frame.LocalVars();
            rtda.heap.Object thi = vars.GetThis();
            rtda.heap.Class clas = (rtda.heap.Class)thi.Extra();
            ushort modifiers = clas.accessFlags;
            OperandStack stack = frame.OperandStack();
            stack.PushInt(modifiers);
        }

        private void isInterface(ref Frame frame)
        {
            LocalVars vars = frame.LocalVars();
            rtda.heap.Object thi = vars.GetThis();
            rtda.heap.Class clas = (rtda.heap.Class)thi.Extra();
            OperandStack stack = frame.OperandStack();
            stack.PushBoolean(clas.IsInterface());
        }

        private void forName0(ref Frame frame)
        {
            LocalVars vars = frame.LocalVars();
            rtda.heap.Object jName = vars.GetRef(0);
            bool initialize = vars.GetBoolean(1);
            //jLoader := vars.GetRef(2)
            string goName = rtda.StringPool.GoString(ref jName);
            goName = goName.Replace('.', '/');
            rtda.heap.Class goClass = frame.Method().Class().Loader().LoadClass(goName);
            rtda.heap.Object jClass = goClass.JClass();
            if (initialize && !goClass.InitStarted())
            {
                // undo forName0
                rtda.Thread thread = frame.Thread();
                frame.SetNextPC(thread.PC());
                // init class
                instructions.ClassInitLogic.InitClass(ref thread, ref goClass);
            }
            else frame.OperandStack().PushRef(jClass);
        }

        private void isPrimitive(ref Frame frame)
        {
            LocalVars vars = frame.LocalVars();
            rtda.heap.Object thi = vars.GetThis();
            rtda.heap.Class clas = (rtda.heap.Class)thi.Extra();
            OperandStack stack = frame.OperandStack();
            stack.PushBoolean(clas.IsPrimitive());
        }

        rtda.heap.Class popClass(ref rtda.Frame frame)
        {
            var vars = frame.LocalVars();
            var thi = vars.GetThis();
            return (rtda.heap.Class)thi.Extra();
        }

        private void getPrimitiveClass(ref Frame frame)
        {
            rtda.heap.Object nameObj = frame.LocalVars().GetRef(0);
            string name = StringPool.GoString(ref nameObj);
            rtda.heap.ClassLoader loader = frame.method.Class().loader;
            rtda.heap.Object clas = loader.LoadClass(name).jClass;
            frame.OperandStack().PushRef(clas);
        }

        private void getName0(ref Frame frame)
        {
            var thi = frame.LocalVars().GetThis();
            var clas = (rtda.heap.Class)thi.extra;

            var name = clas.JavaName();
            var nameObj = StringPool.JString(clas.loader, name);
            frame.operandStack.PushRef(nameObj);
        }

        private void desiredAssertionStatus0(ref Frame frame)
        {
            frame.operandStack.PushBoolean(false);
        }
    }
}