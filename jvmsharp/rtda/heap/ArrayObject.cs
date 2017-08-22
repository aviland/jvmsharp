using System;

namespace jvmsharp.rtda.heap
{
    partial class Object
    {
        internal byte[] Bytes()
        {
            return (byte[])this.data;
        }

        internal short[] Shorts()
        {
            return (short[])this.data;
        }

        internal int[] Ints()
        {
            return (int[])data;
        }

        internal long[] Longs()
        {
            return (long[])this.data;
        }

        internal ushort[] Chars()
        {
            return ((ushort[])this.data);
        }

        internal float[] Floats()
        {
            return (float[])this.data;
        }

        internal double[] Doubles()
        {
            return (double[])this.data;
        }

        internal Object[] Refs()
        {
            return (Object[])this.data;
        }

        internal int ArrayLength()
        {
                     switch (data.GetType().Name)
            {
                case "Byte[]": return ((Byte[])data).Length;
                case "Int16[]": return ((Int16[])data).Length;
                case "Int32[]": return ((Int32[])data).Length;
                case "Int64[]": return ((Int64[])data).Length;
                case "UInt16[]":                    return ((UInt16[])data).Length;
                case "Single[]": return ((Single[])data).Length;
                case "Double[]": return ((Double[])data).Length;
                case "Object[]": return ((Object[])data).Length;
                default: throw new Exception("Not array!");
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
