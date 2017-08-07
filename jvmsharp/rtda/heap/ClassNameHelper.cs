﻿using jvmsharp.rtda.heap;
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
                string componetTypeDescriptor = className.Skip(1).ToString();
                return toClassName(componetTypeDescriptor);
            }
            throw new Exception("Not array: " + className);
        }

        public static string toClassName(string descriptor)
        {
            char ch = descriptor[0];
            if (ch == '[') return descriptor;
            if (ch == 'L') return descriptor.Skip(1).Take(descriptor.Length - 1).ToString();
            foreach (var s in primitiveTypes)
            {
                if (s.Value == descriptor)
                    return s.Key;
            }
            throw new Exception("Invalid descriptor:" + descriptor);
        }
    }
}
