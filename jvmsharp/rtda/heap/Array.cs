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

        public static void ArrayCopy(ref Object src, ref Object dst, ref int srcPos, ref int dstPos, ref int length)
        {
            switch (src.data.GetType().Name)
            {
                case "Byte[]":
                    System.Array.Copy(((Byte[])src.data), srcPos, ((Byte[])dst.data), dstPos, length);
                    break;
                case "Int16[]":
                    System.Array.Copy(((Int16[])src.data), srcPos, ((Int16[])dst.data), dstPos, length);
                    break;
                case "Int32[]":
                    System.Array.Copy(((Int32[])src.data), srcPos, ((Int32[])dst.data), dstPos, length);
                    break;
                case "Int64[]":
                    System.Array.Copy(((long[])src.data), srcPos, ((long[])dst.data), dstPos, length);
                    break;
                case "UInt16[]":
                    System.Array.Copy(((UInt16[])src.data), srcPos, ((UInt16[])dst.data), dstPos, length);
                    break;
                case "Single[]":
                    System.Array.Copy(((float[])src.data), srcPos, ((float[])dst.data), dstPos, length);
                    break;
                case "Double[]":
                    System.Array.Copy(((Double[])src.data), srcPos, ((Double[])dst.data), dstPos, length);
                    break;
                case "Object[]":
                    System.Array.Copy(((Object[])src.data), srcPos, ((Object[])dst.data), dstPos, length);
                    break;
                default:
                    throw new Exception("Not array: %v" + src + "!");
            }
        }
    }
}
