using jvmsharp.rtda;
using jvmsharp.rtda.heap;

namespace jvmsharp.native.sun.reflect
{
    class NativeConstructorAccessorImpl : Native
    {
        public void init()
        {
            Registry.Register("sun/reflect/NativeConstructorAccessorImpl", "newInstance0", "(Ljava/lang/reflect/Constructor;[Ljava/lang/Object;)Ljava/lang/Object;", newInstance0);
        }

        private void newInstance0(ref Frame frame)
        {
            LocalVars vars = frame.LocalVars();
            Object constructorObj = vars.GetRef(0);
            Object argArrObj = vars.GetRef(1);
            Method goConstructor = getGoConstructor(constructorObj);
            Class goClass = goConstructor.Class();

            if (!goClass.InitStarted())
            {
                frame.RevertNextPC();
                instructions.ClassInitLogic.InitClass(ref frame.thread, ref goClass);
                return;
            }
            Object obj = goClass.NewObject();
            OperandStack stack = frame.OperandStack();
            stack.PushRef(obj);
            // call <init>
            OperandStack ops = convertArgs(obj, argArrObj, goConstructor);
            Frame shimFrame = ShimFrame.NewShimFrame(frame.Thread(), ops);
            frame.Thread().PushFrame(shimFrame);
            instructions.InvokeLogic.InvokeMethod(ref shimFrame, ref goConstructor);
        }

        private Method getGoConstructor(Object constructorObj)
        {
            return getGoMethod(constructorObj, true);
        }

        private OperandStack convertArgs(Object thi, Object argArrObj, Method method)
        {
            if (method.ArgSlotCount() == 0)
                return null;
            OperandStack ops = new rtda.OperandStack().newOperandStack(method.ArgSlotCount());

            if (!method.IsStatic())
                ops.PushRef(thi);
            if (method.ArgSlotCount() == 1 && !method.IsStatic())
                return ops;
            return ops;
        }

        Method getGoMethod(Object methodObj, bool isConstructor)
        {
            object extra = methodObj.Extra();
            if (extra != null)
                return (Method)extra;
            if (isConstructor)
            {
                Object root = methodObj.GetRefVar("root", "Ljava/lang/reflect/Constructor;");
                return (Method)root.Extra();
            }
            else
            {
                Object root = methodObj.GetRefVar("root", "Ljava/lang/reflect/Method;");
                return (Method)root.Extra();
            }
        }
    }
}
