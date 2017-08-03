namespace jvmsharp.rtda.heap
{
    class ConstantClassRef : SymRef
    {
        public ConstantClassRef(ref  ConstantPool cp,classfile.ConstantClassInfo classInfo)
        {
            this.cp = cp;
            className = classInfo.Name();
        }
    }
}
