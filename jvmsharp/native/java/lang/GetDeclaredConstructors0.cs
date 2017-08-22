using jvmsharp.rtda;
using jvmsharp.rtda.heap;

namespace jvmsharp.native.java.lang
{
    partial class Class : Native
    {
        private void getDeclaredConstructors0(ref Frame frame)
        {
            LocalVars vars = frame.LocalVars();
            rtda.heap.Object classObj = vars.GetThis();
            bool publicOnly = vars.GetBoolean(1);

            rtda.heap.Class clas = (rtda.heap.Class)classObj.Extra();
            Method[] constructors = clas.GetConstructors(publicOnly);
            uint constructorCount = (uint)constructors.Length;

            rtda.heap.ClassLoader classLoader = frame.Method().Class().Loader();
            rtda.heap.Class constructorClass = classLoader.LoadClass("java/lang/reflect/Constructor");
            rtda.heap.Object constructorArr = constructorClass.ArrayClass().NewArray(constructorCount);

            OperandStack stack = frame.OperandStack();
            stack.PushRef(constructorArr);

            if (constructorCount > 0)
            {
                rtda.Thread thread = frame.Thread();
                rtda.heap.Object[] constructorObjs = constructorArr.Refs();
                Method constructorInitMethod = constructorClass.GetConstructor(constructorConstructorDescriptor);
                for (int i = 0; i < constructors.Length; i++)
                {
                    rtda.heap.Object constructorObj = constructorClass.NewObject();
                    constructorObj.SetExtra(constructors[i]);
                    constructorObjs[i] = constructorObj;
                    OperandStack ops = new rtda.OperandStack().newOperandStack(9);
                    ops.PushRef(constructorObj);                        // this
                    ops.PushRef(classObj);                               // declaringClass
                    ops.PushRef(ClassHelper.toClassArr(ref classLoader, constructors[i].ParameterTypes()));        // parameterTypes
                    ops.PushRef(ClassHelper.toClassArr(ref classLoader, constructors[i].ExceptionTypes()));     // checkedExceptions
                    ops.PushInt(constructors[i].accessFlags);              // modifiers
                    ops.PushInt(0);                                 // todo slot
                    ops.PushRef(ClassHelper.getSignatureStr(ref classLoader, constructors[i].Signature()));      // signature
                    ops.PushRef(ClassHelper.toByteArr(ref classLoader, constructors[i].AnnotationData()));      // annotations
                    ops.PushRef(ClassHelper.toByteArr(ref classLoader, constructors[i].ParameterAnnotationData())); // parameterAnnotations
                    Frame shimFrame = rtda.ShimFrame.NewShimFrame(thread, ops);
                    thread.PushFrame(shimFrame);

                    // init constructorObj
                    instructions.InvokeLogic.InvokeMethod(ref shimFrame, ref constructorInitMethod);
                }
            }
        }

    }
}
