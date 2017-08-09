namespace jvmsharp.rtda
{
    class Frame
    {
        internal Frame lower;
        internal LocalVars localVars;
        internal OperandStack operandStack;
        internal Thread thread;
        internal int nextPC;
        internal heap.Method method;

        public Frame(Thread thread, heap.Method method)
        {
            this.thread = thread;
            this.method = method;
            localVars = new rtda.LocalVars(method.MaxLocals());
            operandStack = new OperandStack(method.MaxStack());
        }

      /*  public Frame(Thread thread, uint maxLocals, uint maxStack)
        {
            this.thread = thread;
            localVars = new LocalVars(maxLocals);
            operandStack = new OperandStack(maxStack);
        }*/

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

        internal heap.Method Method()
        {
            return method;
        }

        internal void RevertNextPC()
        {
            nextPC = thread.PC();
        }
    }
}
