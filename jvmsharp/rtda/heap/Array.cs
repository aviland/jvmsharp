using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace jvmsharp.rtda.heap
{
    class Array
    {
        public static int ArrayLength(Object arr)
        {
            switch (arr.data.GetType().Name)
            {
                case "Byte[]":
                    return ((byte[])arr.data).Length;
                case "Int16[]":
                    return ((short[])arr.data).Length;
                case "Int32[]":
                    return ((Int32[])arr.data).Length;
                case "Int64[]":
                    return ((long[])arr.data).Length;
                case "UInt16[]":
                    return ((UInt16[])arr.data).Length;
                case "Single[]":
                    return ((float[])arr.data).Length;
                case "Double[]":
                    return ((Double[])arr.data).Length;
                case "Object[]":
                    return ((Object[])arr.data).Length;
                default:
                    throw new Exception("Not array: " + arr + "!");
            }
        }

    
    }
}
