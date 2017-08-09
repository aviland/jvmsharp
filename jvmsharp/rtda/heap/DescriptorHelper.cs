using System.Linq;

namespace jvmsharp.rtda.heap
{
    class DescriptorHelper
    {
        public static string getArrayClassName(string className)
        {
            return "[" + toDescriptor(className);
        }

        static string toDescriptor(string className)
        {
            if (className[0] == '[')
                return className;
            string d = null;
            if (ClassNameHelper.primitiveTypes.ContainsKey(className))
                d = ClassNameHelper.primitiveTypes[className];
            if (d != null)
                return d;
            return 'L' + className + ";";
        }
    }
}
