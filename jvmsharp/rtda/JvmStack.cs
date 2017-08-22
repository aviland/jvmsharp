using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace jvmsharp.rtda
{
    struct JvmStack
    {
        uint maxSize;
        uint size;
        Frame _top;

        internal JvmStack newStack(uint maxSize)
        {
            JvmStack js = new JvmStack();
            js.maxSize = maxSize;
            return js;
        }

        internal void push(Frame frame)
        {
            if (size >= maxSize)
                throw new Exception("java.lang.StackOverflowError");
            if (_top != null)
                frame.lower = _top;
            _top = frame;
            size++;
        }

        internal Frame pop()
        {
            if (_top == null)
                throw new Exception("jvm stack is empty!");

            var top = _top;
            _top = top.lower;
            top.lower = null;
            size--;
            return top;
        }
        internal Frame top()
        {
            if (_top == null)
            {
                throw new Exception("jvm stack is empty!");
            }
            return _top;
        }
        internal Frame[] getFrames()
        {
            var frames = new List<Frame>();
            for (var frame = _top; frame != null; frame = frame.lower)
            {
                frames.Add(frame);

            }
            return frames.ToArray();
        }

        internal bool isEmpty()
        {
            return _top == null;
        }

        internal void clear()
        {
            for (; !isEmpty();)
            {
                pop();
            }
        }
    }
}
