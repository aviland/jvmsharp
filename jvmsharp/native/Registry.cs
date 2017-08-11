using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace jvmsharp.native
{
    class Registry
    {
        public delegate void NativeMethod(ref rtda.Frame frame);
        static Dictionary<string, NativeMethod> registry = new Dictionary<string, NativeMethod>();

        public static void Register(string className, string methodName, string methodDescriptor, NativeMethod method)
        {
            var key = className + "~" + methodName + "~" + methodDescriptor;
            registry[key] = method;
        }

        public static NativeMethod FindNativeMethod(string className, string methodName, string methodDescriptor)
        {
            string key = className + "~" + methodName + "~" + methodDescriptor;
         //   Console.WriteLine(key);
            if (registry.ContainsKey(key))
                return registry[key];
            if (methodDescriptor == "()V" && methodName == "registerNatives")
            {
                return emptyNativeMethod;
            }
            return null;
        }
        private static void emptyNativeMethod(ref rtda.Frame frame)
        {
        }

    }
}
