using System;

namespace jvmsharp.rtda
{
    class Stack
    {
        uint maxSize;
        uint size;
        Frame _top;

        public Stack(uint maxSize)
        {
           this.maxSize = maxSize;
        }

        internal bool isStackEmpty()
        {
            return this._top == null;
        }

        internal void push(ref Frame frame)
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
                throw new Exception("jvm stack is empty!");
            return _top;
        }

        internal bool isEmpty()
        {
            return _top == null;
        }

        internal void clear()
        {
            while (!isEmpty())
                pop();
        }

        internal Frame topN(uint n)
        {
            if (size < n)
                throw new Exception("jvm stack size: " + size + "n:" + n);
            var frame = _top;
            while (n > 0)
            {
                frame = frame.lower;
                n--;
            }
            return frame;
        }
    }
}
