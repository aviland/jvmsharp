using jvmsharp.rtda;
using jvmsharp.rtda.heap;

namespace jvmsharp.native.java.lang
{
    partial    class Class : Native
    {
        private void getDeclaredFields0(ref Frame frame)
        {
            LocalVars vars = frame.LocalVars();//取得当前帧的局部变量表
            rtda.heap.Object classObj = vars.GetThis();//取得当前对象
            bool publicOnly = vars.GetBoolean(1);
            rtda.heap.Class clas = (rtda.heap.Class)classObj.Extra();

            Field[] fields = clas.GetFields(publicOnly);

            uint fieldCount = (uint)fields.Length;
            rtda.heap.ClassLoader classLoader = frame.Method().Class().Loader();
            rtda.heap.Class fieldClass = classLoader.LoadClass("java/lang/reflect/Field");
            rtda.heap.Object fieldArr = fieldClass.ArrayClass().NewArray(fieldCount);
            OperandStack stack = frame.OperandStack();
            stack.PushRef(fieldArr);
            if (fieldCount > 0)
            {
                rtda.Thread thread = frame.Thread();
                rtda.heap.Object[] fieldObjs = fieldArr.Refs();
                Method fieldConstructor = fieldClass.GetConstructor(fieldConstructorDescriptor);
                for (int i = 0; i < fields.Length; i++)
                {
                    Field goField = fields[i];

                    rtda.heap.Object fieldObj = fieldClass.NewObject();
                    object goObject = fields[i];
                    fieldObj.SetExtra(goObject);
                    fieldObjs[i] = fieldObj;
                    OperandStack ops = new rtda.OperandStack().newOperandStack(8);
                    ops.PushRef(fieldObj);                   // this
                    ops.PushRef(classObj);                    // declaringClass
                    ops.PushRef(StringPool.JString(classLoader, fields[i].Name()));         // name

                    ops.PushRef(fields[i].Type().JClass());                 // type
                    ops.PushInt(fields[i].accessFlags);                // modifiers
                    ops.PushInt((int)fields[i].SlotId());                    // slot
                    ops.PushRef(ClassHelper.getSignatureStr(ref classLoader, goField.Signature())); // signature
                    ops.PushRef(ClassHelper.toByteArr(ref classLoader, goField.AnnotationData()));  // annotations
                    Frame shimFrame = rtda.ShimFrame.NewShimFrame(thread, ops);
                    thread.PushFrame(shimFrame);
                    // init fieldObj
                    instructions.InvokeLogic.InvokeMethod(ref shimFrame, ref fieldConstructor);
                }
            }
        }
    }
}
