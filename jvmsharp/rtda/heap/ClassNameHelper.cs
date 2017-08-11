using jvmsharp.rtda.heap;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace jvmsharp.rtda.heap
{
    class ClassNameHelper
    {
        public static Dictionary<string, string> primitiveTypes = new Dictionary<string, string> {
            { "void", "V" },
            { "boolean","Z"},
            { "byte","B"},
            { "short","S"},
            { "int","I"},
            { "long","J"},
            { "char","C"},
            { "float","F"},
            { "double","D"}
        };

        public static string getComponentClassName(string className)
        {
            if (className[0] == '[')
            {
                string componetTypeDescriptor = className.Substring(1);
                return toClassName(componetTypeDescriptor);
            }
            throw new Exception("Not array: " + className);
        }
        public static string DotToSlash(string name)  {
            return name.Replace('.', '/');
}
    public static string toClassName(string descriptor)
        {
            char ch = descriptor[0];
            if (ch == '[') return descriptor;
            if (ch == 'L') return descriptor.Substring(1);
            foreach (var s in primitiveTypes)
            {
                if (s.Value == descriptor)
                    return s.Key;
            }
            throw new Exception("Invalid descriptor:" + descriptor);
        }
    }
}
