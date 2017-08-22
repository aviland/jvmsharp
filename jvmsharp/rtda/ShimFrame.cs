using jvmsharp.rtda.heap;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace jvmsharp.rtda
{
    class ShimFrame
    {
        internal static Frame NewShimFrame(Thread thread, OperandStack ops)
        {
            ShimMethod sf = new heap.ShimMethod();
            Method f = sf.ShimReturnMethod();
             Frame frame = new Frame(thread, f);
            frame.operandStack = ops;
            return frame;
        }
    }
}
