using System;

namespace jvmsharp.rtda.heap
{
    partial class Class
    {

        internal heap.Object NewArray(uint count)
        {
            if (!this.IsArray())
                throw new Exception("Not array class:" + name);
            //    Console.WriteLine("adsafsd2222222222222222222"+ name);
            switch (name)
            {
                case "[Z":
                case "[B":
                    return new rtda.heap.Object(this, new byte[count],null);
                case "[C": return new heap.Object(this, new UInt16[count], null);
                case "[S": return new heap.Object(this, new short[count], null);
                case "[I": return new heap.Object(this, new Int32[count], null);
                case "[J": return new heap.Object(this, new long[count], null);
                case "[F": return new heap.Object(this, new float[count], null);
                case "[D": return new heap.Object(this, new double[count], null);
                default: return new heap.Object(this, new heap.Object[count], null);
            }
        }

        public bool IsArray()
        {
            return name[0] == '[';
        }


        public bool IsPrimitiveArray()
        {
            return IsArray() && name.Length == 2;
        }

        public Class ComponentClass()
        {
            string componentClassName = new ClassNameHelper().getComponentClassName(this.name);
            return loader.LoadClass(componentClassName);
        }



        internal Object JClass()
        {
            return this.jClass;
        }

        internal static Object NewByteArray(ClassLoader loader, sbyte[] bytes)
        {
            return new rtda.heap.Object(loader.LoadClass("[B"), bytes, null);
        }

        internal Class[] Interfaces()
        {
            return this.interfaces;
        }
    }
}
