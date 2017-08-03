using System;

namespace jvmsharp.rtda.heap
{
    struct PrimitiveType
    {
        public string Descriptor;
        public string ArrayClassName;
        public string Name;
        public string WrapperClassName;

        public PrimitiveType(string Descriptor, string ArrayClassName, string Name, string WrapperClassName)
        {
            this.Descriptor = Descriptor;
            this.ArrayClassName = ArrayClassName;
            this.Name = Name;
            this.WrapperClassName = WrapperClassName;
        }
    }

    class PrimitiveTypes
    {

        public static PrimitiveType[] primitiveTypes = {
            new PrimitiveType("V", "[V", "void", "java/lang/Void"),
             new PrimitiveType("Z", "[Z", "boolean", "java/lang/Boolean"),
              new PrimitiveType("B", "[B", "byte", "java/lang/Byte"),
            new PrimitiveType("C", "[C", "char", "java/lang/Character"),
                          new PrimitiveType("S", "[S", "short", "java/lang/Short"),
                             new PrimitiveType("I", "[I", "int", "java/lang/Integer"),
                                new PrimitiveType("J", "[J", "long", "java/lang/Long"),
                             new PrimitiveType("F", "[F", "float", "java/lang/Float"),
                                   new PrimitiveType("D", "[D", "double", "java/lang/Double")
        };

        static public string getPrimitiveType(string descriptor)
        {
            foreach (PrimitiveType pt in primitiveTypes)
            {
                if (pt.Descriptor == descriptor)
                    return pt.Name;
            }
            throw new Exception("Not primitive type: " + descriptor);
        }
    }
}
