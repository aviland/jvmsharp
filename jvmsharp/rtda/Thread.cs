using System.Collections.Generic;

namespace jvmsharp.rtda
{
    class Thread
    {
        private int pc;//程序计数器
        public Stack<Frame> stack;//虚拟机栈

        public Thread() { }

        public Thread(int deep)
        {
            stack = new Stack<Frame>(deep);
        }

        public Thread newThread()
        {
            return new Thread(1024);
        }

        internal bool isStackEmpty()
        {
            return this.stack.Count==0;
        }


        internal void InitClass(ref heap.Class clas)
        {

            //   initClass(this, clas);
        }


        public void PushFrame(ref Frame frame)
        {
            stack.Push(frame);
        }

        public Frame PopFrame()
        {
            return stack.Pop();
        }

        public Frame CurrentFrame()
        {
            return stack.Peek();
        }

     /*   public Frame newFrame(uint maxLocals, uint maxStack)
        {
            return new Frame(this, maxLocals, maxStack);
        }*/

        public Frame newFrame(ref heap.Method method)
        {
            return new Frame(this,  method);
        }

        public Frame TopFrame()
        {
            return stack.Peek();
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
