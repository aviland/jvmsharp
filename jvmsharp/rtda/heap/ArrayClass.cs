namespace jvmsharp.rtda.heap
{
    partial class Class
    {
        public bool IsArray()
        {
            return name[0] == '[';
        }

        public bool IsPrimitiveArray()
        {
            return IsArray() && name.Length == 2;
        }

        /*     public Class ComponentClass()
             {
                 string componentClassName = ClassNameHelper.getComponentClassName(this.name);
                 return new ClassLoader().LoadClass(componentClassName);
             }

             Class arrayClass()
             {
                 string arrayClassName = DescriptorHelper.getArrayClassName(this.name);
                 return new ClassLoader().LoadClass(arrayClassName);
             } */
    }
}
