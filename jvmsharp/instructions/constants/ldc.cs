using System;
using jvmsharp.rtda;
using jvmsharp.rtda.heap;

namespace jvmsharp.instructions.constants
{
    class _LDC
    {
        public static void _ldc(ref Frame frame, uint index)
        {
            OperandStack stack = frame.OperandStack();
            Class clas = frame.Method().Class();
            object c = clas.ConstantPool().GetConstant(index);
     //      Console.WriteLine("_LDC\t" + c.GetType().Name);
            switch (c.GetType().Name)
            {
                case "Int32": stack.PushInt((int)c); break;
                case "Single":
                    stack.PushFloat((float)c); break;
                case "String":
                    var internedStr = StringHelper.JString(ref clas.loader, (string)c);
                    stack.PushRef(internedStr);
                    break;
                case "ConstantClassRef":
                    ConstantClassRef classRef = (ConstantClassRef)c;
                    rtda.heap.Object classObj = classRef.ResolvedClass().jClass;
                    stack.PushRef(classObj);
                    break;
                default:
                    // todo
                    // ref to MethodType or MethodHandle
                    throw new Exception("todo: ldc! " + c);
            }
        }
    }
    class LDC : Index8Instruction
    {
        public override void Execute(ref Frame frame)
        {
            _LDC._ldc(ref frame, Index);
        }
    }

    class LDC_W : Index16Instruction
    {
        public override void Execute(ref Frame frame)
        {
            _LDC._ldc(ref frame, Index);
        }
    }

    class LDC2_W : Index16Instruction
    {
        public override void Execute(ref Frame frame)
        {
            var stack = frame.OperandStack();
            var cp = frame.Method().Class().ConstantPool();
            var c = cp.GetConstant(Index);
        //    Console.WriteLine("_LDC\t" + c.GetType().Name);
            switch (c.GetType().Name)
            {
                case "Int64": stack.PushLong((long)c); break;
                case "Double": stack.PushDouble((double)c); break;
                default: throw new Exception("java.lang.ClassFormatError");
            }
        }
    }
}
