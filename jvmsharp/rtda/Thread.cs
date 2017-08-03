namespace jvmsharp.rtda
{
    class Thread
    {
        private int pc;//程序计数器
        private rtda.Stack stack;//虚拟机栈

        public Thread() { }

        public Thread(int deep)
        {
            stack = new rtda.Stack((uint)deep);
        }

        public Thread newThread()
        {
            return new Thread(1024);
        }

        internal bool isStackEmpty()
        {
            return this.stack.isStackEmpty();
        }


        internal void InitClass(ref heap.Class clas)
        {

            //   initClass(this, clas);
        }


        public void PushFrame(ref Frame frame)
        {
            stack.push(ref frame);
        }

        public Frame PopFrame()
        {
            return stack.pop();
        }

        public Frame CurrentFrame()
        {
            return stack.top();
        }

        public Frame newFrame(uint maxLocals, uint maxStack)
        {
            return new Frame(this, maxLocals, maxStack);
        }

        public Frame newFrame(ref heap.Method method)
        {
            return new Frame(this, ref method);
        }

        public Frame TopFrame()
        {
            return stack.top();
        }

        public int PC()
        {
            return pc;
        }

        public void SetPC(int pc)
        {
            this.pc = pc;
        }
    }
}
