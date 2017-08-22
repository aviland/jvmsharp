using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using jvmsharp.rtda;
using jvmsharp.rtda.heap;
using System.Reflection;

namespace jvmsharp.instructions.references
{
    class ATHROW : NoOperandsInstruction
    {
        public override void Execute(ref Frame frame)
        {
            var ex = frame.OperandStack().PopRef();
            if (ex == null)
                throw new Exception("java.lang.NullPointerException");
            var thread = frame.Thread();
            if (!findAndGotoExceptionHandler(ref thread, ref ex))
                handleUncaughtException(ref thread, ref ex);
        }

        private void handleUncaughtException(ref Thread thread, ref rtda.heap.Object ex)
        {
            thread.ClearStack();
            var jMsg = ex.GetRefVar("detailMessage", "Ljava/lang/String;");
            var goMsg = StringPool.GoString(ref jMsg);
            Console.WriteLine(ex.clas.JavaName() + ": " + goMsg);
            var stes = ex.Extra().GetType().GetEnumValues();

            for (int i = 0; i < stes.Length; i++)
            {
                var ste = stes.GetValue(i).ToString();
                Console.WriteLine("\tat " + ste);
            }
        }

        private bool findAndGotoExceptionHandler(ref Thread thread, ref rtda.heap.Object ex)
        {
            for (;;)
            {
                Frame frame = thread.CurrentFrame();
                int pc = frame.NextPC() - 1;
             
                int handlerPC = frame.Method().FindExceptionHandler(ex.Class(), pc);

                if (handlerPC > 0)
                {
                    OperandStack stack = frame.OperandStack();
                    stack.Clear();
                    stack.PushRef(ex);
                    frame.SetNextPC(handlerPC);
                    return true;
                }
                thread.PopFrame();
                if (thread.IsStackEmpty())
                    break;
            }
            return false;
        }
    }
}
