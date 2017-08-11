using System;
using jvmsharp.rtda;

namespace jvmsharp.instructions.reserved
{
    class INVOKE_NATIVE : NoOperandsInstruction
    {
        public override void Execute(ref Frame frame)
        {

            var method = frame.method;
            var className = method.Class().name;
            var methodName = method.Name();
            var methodDescriptor = method.Descriptor();

            var nativeMethod = native.Registry.FindNativeMethod(className, methodName, methodDescriptor);
            if (nativeMethod == null)
            {
                var methodInfo = className + "." + methodName + methodDescriptor;
                throw new Exception("java.lang.UnsatisfiedLinkError: " + methodInfo);
            }

            nativeMethod(ref frame);
        }
    }
}
