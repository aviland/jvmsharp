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
                    return new rtda.heap.Object(this, new byte[count]);
                case "[C": return new heap.Object(this, new UInt16[count]);
                case "[S": return new heap.Object(this, new short[count]);
                case "[I": return new heap.Object(this, new Int32[count]);
                case "[J": return new heap.Object(this, new long[count]);
                case "[F": return new heap.Object(this, new float[count]);
                case "[D": return new heap.Object(this, new double[count]);
                default: return new heap.Object(this, new heap.Object[count]);
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
            string componentClassName = ClassNameHelper.getComponentClassName(this.name);
            return loader.LoadClass(componentClassName);
        }

        internal Class ArrayClass()
        {
            string arrayClassName = DescriptorHelper.getArrayClassName(this.name);
            return this.loader.LoadClass(arrayClassName);
        }
    }
}
