using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using jvmsharp.rtda;
using jvmsharp.rtda.heap;

namespace jvmsharp.native.sun.misc
{
    class Unsafe : Native
    {
        public void init()
        {
            Registry.Register("sun/misc/Unsafe", "arrayBaseOffset", "(Ljava/lang/Class;)I", arrayBaseOffset);
            Registry.Register("sun/misc/Unsafe", "arrayIndexScale", "(Ljava/lang/Class;)I", arrayIndexScale);
            Registry.Register("sun/misc/Unsafe", "addressSize", "()I", addressSize);
            Registry.Register("sun/misc/Unsafe", "objectFieldOffset", "(Ljava/lang/reflect/Field;)J", objectFieldOffset);
            Registry.Register("sun/misc/Unsafe", "compareAndSwapObject", "(Ljava/lang/Object;JLjava/lang/Object;Ljava/lang/Object;)Z", compareAndSwapObject);
            Registry.Register("sun/misc/Unsafe", "getIntVolatile", "(Ljava/lang/Object;J)I", getIntVolatile);
            Registry.Register("sun/misc/Unsafe", "compareAndSwapInt", "(Ljava/lang/Object;JII)Z", compareAndSwapInt);
            Registry.Register("sun/misc/Unsafe", "getObjectVolatile", "(Ljava/lang/Object;J)Ljava/lang/Object;", getObjectVolatile);
        }

        private void getObjectVolatile(ref Frame frame)
        {
            LocalVars vars = frame.LocalVars();
            object fields = vars.GetRef(1).Data();
            long offset = vars.GetLong(2);
            Slots anys = (rtda.heap.Slots)fields;
            if (anys != null)
            { // object
                rtda.heap.Object x = anys.GetRef((uint)offset);
                frame.OperandStack().PushRef(x);
            }
            else
            {
                rtda.heap.Object[] objs = (rtda.heap.Object[])fields;
                if (objs != null)
                {
                    // ref[]
                    rtda.heap.Object x = objs[offset];
                    frame.OperandStack().PushRef(x);
                }
                else throw new Exception("getObject!");
            }
        }

        private void compareAndSwapInt(ref Frame frame)
        {
            LocalVars vars = frame.LocalVars();
            object fields = vars.GetRef(1).Data();
            long offset = vars.GetLong(2);
            int expected = vars.GetInt(4);
            int newVal = vars.GetInt(5);
            Slots slots = (Slots)fields;
            if (slots != null)
            {
                // object
                int oldVal = slots.GetInt((uint)offset);
                if (oldVal == expected)
                {
                    slots.SetInt((uint)offset, newVal);
                    frame.OperandStack().PushBoolean(true);
                }
                else frame.OperandStack().PushBoolean(false);
            }
            else
            {
                int[] ints = (int[])fields;
                if (ints != null)
                {
                    // int[]
                    int oldVal = ints[offset];
                    if (oldVal == expected)
                    {
                        ints[offset] = newVal;
                        frame.OperandStack().PushBoolean(true);
                    }
                    else frame.OperandStack().PushBoolean(false);
                }
                else throw new Exception("todo: compareAndSwapInt!");
            }
        }

        private void getIntVolatile(ref Frame frame)
        {
            LocalVars vars = frame.LocalVars();
            object fields = vars.GetRef(1).Data();
            long offset = vars.GetLong(2);
            OperandStack stack = frame.OperandStack();
            Slots slots = (Slots)fields;
            if (slots != null)
                stack.PushInt(slots.GetInt((uint)offset));     // object
            else
            {
                short[] shorts = (short[])fields;
                if (shorts != null)
                    stack.PushInt(shorts[offset]);
                else throw new Exception("getInt!");
            }
        }

        private void compareAndSwapObject(ref Frame frame)
        {
            LocalVars vars = frame.LocalVars();
            rtda.heap.Object obj = vars.GetRef(1);
            object fields = obj.Data();
            long offset = vars.GetLong(2);
            rtda.heap.Object expected = vars.GetRef(4);
            rtda.heap.Object newVal = vars.GetRef(5);
            // todo
            Slots anys = (rtda.heap.Slots)fields;
            if (anys != null)
            {
                // object
                bool swapped = casObj(obj, ref anys, offset, expected, newVal);
                frame.OperandStack().PushBoolean(swapped);
            }
            else
            {
                rtda.heap.Object[] objs = (rtda.heap.Object[])fields;
                if (objs != null)
                {
                    bool swapped = casArr(ref objs, offset, expected, newVal);
                    frame.OperandStack().PushBoolean(swapped);
                }
                else throw new Exception("todo: compareAndSwapObject!");
            }
        }

        bool casArr(ref rtda.heap.Object[] objs, long offset, rtda.heap.Object expected, rtda.heap.Object newVal)
        {
            rtda.heap.Object current = objs[offset];
            if (current == expected)
            {
                objs[offset] = newVal;
                return true;
            }
            else return false;
        }

        bool casObj(rtda.heap.Object obj, ref rtda.heap.Slots fields, long offset, rtda.heap.Object expected, rtda.heap.Object newVal)
        {
            rtda.heap.Object current = fields.GetRef((uint)offset);
            if (current == expected)
            {
                fields.SetRef((uint)offset, newVal);
                return true;
            }
            else return false;
        }

        private void objectFieldOffset(ref Frame frame)
        {
            LocalVars vars = frame.LocalVars();
            rtda.heap.Object jField = vars.GetRef(1);
            int offset = jField.GetIntVar("slot", "I");
            OperandStack stack = frame.OperandStack();
            stack.PushLong(offset);
        }

        private void addressSize(ref Frame frame)
        {
            frame.OperandStack().PushInt(8); // todo
        }

        private void arrayIndexScale(ref Frame frame)
        {
            frame.OperandStack().PushInt(1); // todo
        }

        private void arrayBaseOffset(ref Frame frame)
        {
            frame.OperandStack().PushInt(0); // todo
        }
    }
}
