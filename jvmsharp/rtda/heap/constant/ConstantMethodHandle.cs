using System;
using jvmsharp.classfile;

namespace jvmsharp.rtda.heap
{
    struct ConstantMethodHandle
    {
        byte referenceKind;
        UInt16 referenceIndex;

        public ConstantMethodHandle(ConstantMethodHandleInfo mhInfo)
        {
            referenceKind = mhInfo.referenceKind;
            referenceIndex = mhInfo.referenceIndex;
        }
    }
}
