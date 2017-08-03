using System.Linq;

namespace jvmsharp.rtda.heap
{
    class DescriptorHelper
    {
        public static string getArrayClassName(string className)
        {
            char[] ch = className.ToCharArray();
            if (ch[0] == '[')
            {
                // array
                return "[" + className;
            }
            foreach (PrimitiveType pt in PrimitiveTypes.primitiveTypes)
            {
                if (pt.Name == className)
                {
                    // primitive
                    return pt.ArrayClassName;

                }
            }
            // object
            return "[L" + className + ";";
        }

        public static string getClassName(string descriptor)
        {
            char[] ch = descriptor.ToCharArray();
            switch (ch[0])
            {
                case '[': // array
                    return descriptor;
                case 'L': // object
                    return ch.Skip(1).Take(descriptor.Length - 1).ToString();
                default: // primirive
                    return PrimitiveTypes.getPrimitiveType(descriptor);
            }
        }
    }
}
