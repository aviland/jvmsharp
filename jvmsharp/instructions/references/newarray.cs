using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using jvmsharp.rtda;
using jvmsharp.rtda.heap;

namespace jvmsharp.instructions.references
{
    struct NEW_ARRAY:Instruction
    {
        byte atype;

        const byte AT_BOOLEAN = 4;
        const byte AT_CHAR = 5;
        const byte AT_FLOAT = 6;
        const byte AT_DOUBLE = 7;
        const byte AT_BYTE = 8;
        const byte AT_SHORT = 9;
        const byte AT_INT = 10;
        const byte AT_LONG = 11;

        public void Execute(ref Frame frame)
        {
            var stack = frame.OperandStack();
            int count = stack.PopInt();
            if (count < 0)
                throw new Exception("java.lang.NegativeArraySizeException");
            var classLoader = frame.Method().Class().loader;
            var arrClass = getPrimitiveArrayClass(ref classLoader, atype);
            rtda.heap.Object arr = arrClass.NewArray((uint)count);
         //   Console.WriteLine("newarray:"+arr.data.GetType().Name);
            stack.PushRef(arr);
        }

        public void FetchOperands(ref BytecodeReader reader)
        {
            atype = reader.ReadUint8();
        }

        Class getPrimitiveArrayClass(ref ClassLoader loader,byte atype)
        {
      //      Console.Write("________________________atype" + atype);
            switch (atype)
            {
                case AT_BOOLEAN:return loader.LoadClass("[Z");
                case AT_BYTE: return loader.LoadClass("[B");
                case AT_CHAR: return loader.LoadClass("[C");
                case AT_SHORT: return loader.LoadClass("[S");
                case AT_INT: return loader.LoadClass("[I");
                case AT_LONG: return loader.LoadClass("[J");
                case AT_FLOAT: return loader.LoadClass("[F");
                case AT_DOUBLE: return loader.LoadClass("[D");
                default:throw new Exception("Invalid atype!");
            }
        }
    }
}
