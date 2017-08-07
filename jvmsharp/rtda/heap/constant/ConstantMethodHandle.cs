using jvmsharp.classfile;

namespace jvmsharp.rtda.heap
{
    struct ConstantMethodHandle
    {
        byte referenceKind;
        ushort referenceIndex;

        public ConstantMethodHandle(ConstantMethodHandleInfo mhInfo)
        {
            referenceKind = mhInfo.referenceKind;
            referenceIndex = mhInfo.referenceIndex;
        }
    }
}
