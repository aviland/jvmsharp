using jvmsharp.rtda.heap;
using System;

namespace jvmsharp.rtda
{
    class LocalVars : Slots
    {
        public LocalVars(uint maxLocals)
        {
            if (maxLocals > 0)
                slots = new Slot[maxLocals];
            else slots = null;
        }

        unsafe internal heap.Object GetThis()
        {
            return GetRef(0);
        }
    }
}
