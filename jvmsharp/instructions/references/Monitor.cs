using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using jvmsharp.rtda;

namespace jvmsharp.instructions.references
{
    class MONITOR_ENTER : NoOperandsInstruction
    {
        public override void Execute(ref Frame frame)
        {
            rtda.heap.Object refs = frame.OperandStack().PopRef();
            if (refs == null)
                throw new Exception("java.lang.NullPointerException");
        }
    }

    class MONITOR_EXIT : NoOperandsInstruction
    {
        public override void Execute(ref Frame frame)
        {
            rtda.heap.Object refs = frame.OperandStack().PopRef();
            if (refs == null)
            {
           //     System.Environment.Exit(0);
                throw new Exception("java.lang.NullPointerException");
            }
               
        }
    }
}
