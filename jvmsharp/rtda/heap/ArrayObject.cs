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
            return (ushort[])this.data;
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
             //     Console.WriteLine("++++++++++++++"+data.GetType().Name);
            switch (data.GetType().Name)
            {
                case "Byte[]": return ((byte[])data).Length;
                case "Int16[]": return ((Int16[])data).Length;
                case "Int32[]": return ((Int32[])data).Length;
                case "Int64[]": return ((Int64[])data).Length;
                case "UInt16[]": return ((UInt16[])data).Length;
                case "Single[]": return ((Single[])data).Length;
                case "Double[]": return ((double[])data).Length;
                case "heap.Object[]": return ((Object[])data).Length;
                default:throw new Exception("Not array!");
            }
        }
    }
}
