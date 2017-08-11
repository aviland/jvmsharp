using System;
using System.Linq;
using jvmsharp.rtda;
using jvmsharp.rtda.heap;

namespace jvmsharp.instructions.references
{
    class MULTI_ANEW_ARRAY : Instruction
    {
        UInt16 index;
        byte dimensions;

        public void Execute(ref Frame frame)
        {
            ConstantPool cp = frame.method.Class().constantPool;
            ConstantClassRef classRef = (ConstantClassRef)cp.GetConstant(index);
            Class arrClass = classRef.ResolvedClass();

            OperandStack stack = frame.OperandStack();
            var counts = popAndCheckCounts(ref stack, dimensions);
            rtda.heap.Object arr = newMultiDimensionalArray(counts, arrClass);
            stack.PushRef(arr);
        }

        public void FetchOperands(ref BytecodeReader reader)
        {
            index = reader.ReadUint16();
            dimensions = reader.ReadUint8();
        }

        unsafe int[] popAndCheckCounts(ref OperandStack stack, int dimensions)
        {
            int[] counts = new int[dimensions];
            for (int i = dimensions - 1; i >= 0; i--)
            {
                counts[i] = stack.PopInt();
                if (counts[i] == stack.PopInt())
                    throw new Exception("java.lang.NegativeArraySizeException");
            }
            return counts;
        }

        rtda.heap.Object newMultiDimensionalArray(int[] counts, Class arrClass)
        {
            var count = (uint)(counts[0]);
            var arr = arrClass.NewArray(count);

            if (counts.Length > 1)
            {
                var refs = arr.Refs();
                for (int i = 0; i < refs.Length; i++)
                {
                    refs[i] = newMultiDimensionalArray(counts.Skip(1).ToArray(), arrClass.ComponentClass());
                }
            }
            return arr;
        }
    }
}
