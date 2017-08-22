using System;
using System.Collections.Generic;

namespace jvmsharp.rtda
{
    class Thread
    {
        internal  int pc;//程序计数器
        public  JvmStack stack;//虚拟机栈

        public Thread() { }

        public Thread(int deep)
        {
            stack = new JvmStack().newStack((uint)deep);
        }

        public Thread newThread()
        {
            return new Thread(1024);
        }

        internal bool IsStackEmpty()
        {
            return stack.isEmpty();
        }

        internal void InitClass(ref heap.Class clas)
        {
            //   initClass(this, clas);
        }


        public  void PushFrame(Frame frame)
        {
            stack.push(frame);
        }

        public Frame PopFrame()
        {
            return stack.pop();
        }

        internal void ClearStack()
        {
            stack.clear();
        }

        public Frame CurrentFrame()
        {
            return stack.top();
        }

        internal Frame[] GetFrames()
        {
            return stack.getFrames();
        }

        public Frame newFrame(ref heap.Method method)
        {
            return new Frame(this,  method);
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
