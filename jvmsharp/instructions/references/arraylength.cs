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
            rtda.heap.Object arrRef = frame.operandStack.PopRef();
            if (arrRef == null)
                throw new Exception("java.lang.NullPointerException");
            int arrLen = arrRef.ArrayLength();
      //   Console.WriteLine("ARRAY_LENGTH"+arrLen);
            frame.operandStack.PushInt(arrLen);
        }
    }
}
