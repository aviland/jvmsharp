using System;

namespace jvmsharp.rtda.heap
{
   unsafe partial class Object
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
                case "Byte[]": return ((byte[])data).Length;
                case "Int16[]": return ((short[])data).Length;
                case "Int32[]": return ((int[])data).Length;
                case "Int64[]": return ((long[])data).Length;
                case "UInt16[]": return ((ushort[])data).Length;
                case "Single[]": return ((float[])data).Length;
                case "Double[]": return ((double[])data).Length;
                case "heap.Object[]": return ((Object[])data).Length;
                default:throw new Exception("Not array!");
            }
        }

        internal byte[]  GoBytes()    {
            sbyte[] s = (sbyte[])data;
            byte[] b = new byte[s.Length];
            for(int i = 0; i < s.Length; i++)
            {
                b[i] = (byte)s[i];
            }
            return b;
}
}
}
