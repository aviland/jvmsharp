namespace jvmsharp.rtda.heap
{
    struct ConstantUtf8
    {
        string str;

        public ConstantUtf8(classfile.ConstantUtf8Info utf8Info)
        {
            str = utf8Info.str;
        }

        string Str()
        {
            return str;
        }
    }
}
