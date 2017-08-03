using System;
using System.Linq;

namespace jvmsharp.rtda.heap
{
    class ClassNameHelper
    {
     public  static  string getComponentClassName(string className)
        {
            char[] ch = className.ToCharArray();
            if (ch[0] == '[')
            {
                char[] descriptor = ch.Skip(1).ToArray();
                return DescriptorHelper.getClassName(descriptor.ToString());
            }
            throw new Exception("Not array: " + className);
        }
    }
}
