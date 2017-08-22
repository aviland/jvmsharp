namespace jvmsharp.rtda.heap
{
    class ClassRef : SymRef
    {
        public ClassRef newClassRef(ref ConstantPool cp, classfile.ConstantClassInfo classInfo)
        {
            var refs = new ClassRef();
            refs.cp = cp;
            refs.className = classInfo.Name();
            return refs;
        }
    }
}
