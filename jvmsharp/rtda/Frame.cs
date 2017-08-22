using jvmsharp.rtda.heap;
using System;

namespace jvmsharp.rtda
{
    class Frame
    {
        private  LocalVars localVars;       
        internal  OperandStack operandStack;
        internal Thread thread;
        internal int nextPC;
        internal  Method method;
        internal Frame lower;
        public Frame(Thread thread, heap.Method method)
        {
            this.thread = thread;
            this.method = method;
            this.localVars = new LocalVars().newLocalVars(method.MaxLocals());
            this.operandStack = new rtda.OperandStack().newOperandStack(method.maxStack);
        }

        internal Method Method()
        {
            return this.method;
        }

        internal int NextPC()
        {
            return nextPC;
        }

        internal LocalVars LocalVars()
        {
            return localVars;
        }

        internal OperandStack OperandStack()
        {
            return operandStack;
        }

        internal Thread Thread()
        {
            return thread;
        }

        internal void SetNextPC(int nextPC)
        {
            this.nextPC = nextPC;
        }

        internal void RevertNextPC()
        {
            nextPC = thread.pc;
        }
    }
}
