namespace jvmsharp.rtda
{
    class Frame
    {
        private  LocalVars localVars;       
        internal  OperandStack operandStack;
        internal Thread thread;
        internal int nextPC;
        internal  heap.Method method;

        public Frame(Thread thread, heap.Method method)
        {
            this.thread = thread;
            this.method = method;
            localVars = new LocalVars(method.MaxLocals());
            operandStack = new OperandStack(method.MaxStack());
        }

        public Frame(Thread thread, uint maxLocals, uint maxStack)
        {
            this.thread = thread;
            localVars = new LocalVars(maxLocals);
            operandStack = new OperandStack(maxStack);
        }

        public Frame( uint maxLocals, uint maxStack)
        {
            localVars = new LocalVars(maxLocals);
            operandStack = new OperandStack(maxStack);
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
            nextPC = thread.PC();
        }
    }
}
