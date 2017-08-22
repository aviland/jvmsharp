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
            Class clas = frame.method.Class();
            object c = clas.constantPool.GetConstant(index);
     //      Console.WriteLine("_LDC\t" + c.GetType().Name);
            switch (c.GetType().Name)
            {
                case "Int32":
                    int ic = (int)c;
                    frame.operandStack.PushInt(ic); break;
                case "Single":
                    float fc = (float)c;
                    frame.operandStack.PushFloat(fc); break;
                case "String":
                    var internedStr = StringPool.JString( clas.loader, (string)c);
                    frame.operandStack.PushRef(internedStr);
                    break;
                case "ClassRef":
                    ClassRef classRef = (ClassRef)c;
                    rtda.heap.Object classObj = classRef.ResolvedClass().jClass;
                    frame.operandStack.PushRef(classObj);
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
            var cp = frame.method.Class().constantPool;
            var c = cp.GetConstant(Index);
        //    Console.WriteLine("_LDC\t" + c.GetType().Name);
            switch (c.GetType().Name)
            {
                case "Int64":
                    long lc = (long)c;
                    frame.operandStack.PushLong(lc); break;
                case "Double":
                    double d = Convert.ToDouble(c);
                    frame.operandStack.PushDouble(d); break;
                default: throw new Exception("java.lang.ClassFormatError");
            }
        }
    }
}
