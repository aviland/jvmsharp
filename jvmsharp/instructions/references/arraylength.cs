using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using jvmsharp.rtda;

namespace jvmsharp.instructions.references
{
 unsafe   class ARRAY_LENGTH : NoOperandsInstruction
    {
        public override void Execute(ref Frame frame)
        {
            OperandStack stack = frame.OperandStack();
            rtda.heap.Object arrRef = stack.PopRef();
            if (arrRef == null)
                throw new Exception("java.lang.NullPointerException");
            int arrLen = arrRef.ArrayLength();
            stack.PushInt(arrLen);
        }
    }
}
