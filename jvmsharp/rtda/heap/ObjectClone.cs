using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace jvmsharp.rtda.heap
{
   partial class Object
    {
        object cloneData()
        {
            object elements, elements2;
            switch (data.GetType().Name)
            {
                case "Byte[]":
                    elements = (Byte[])data;
                    elements2 = new Object[((Byte[])elements).Length];
                    System.Array.Copy((Byte[])elements, (Byte[])elements2, ((Byte[])elements).Length);
                    return elements2;
                case "Int16[]":
                    elements = (Int16[])data;
                    elements2 = new Object[((Int16[])elements).Length];
                    System.Array.Copy((Int16[])elements, (Int16[])elements2, ((Int16[])elements).Length);
                    return elements2;
                case "UInt16[]":
                    elements = (UInt16[])data;
                    elements2 = new Object[((UInt16[])elements).Length];
                    System.Array.Copy((UInt16[])elements, (UInt16[])elements2, ((UInt16[])elements).Length);
                    return elements2;
                case "Int32[]":
                    elements = (Int32[])data;
                    elements2 = new Object[((Int32[])elements).Length];
                    System.Array.Copy((Int32[])elements, (Int32[])elements2, ((Int32[])elements).Length);
                    return elements2;
                case "Int64[]":
                    elements = (Int64[])data;
                    elements2 = new Object[((Int64[])elements).Length];
                    System.Array.Copy((Int64[])elements, (Int64[])elements2, ((Int64[])elements).Length);
                    return elements2;
                case "Single[]":
                    elements = (Single[])data;
                    elements2 = new Object[((Single[])elements).Length];
                    System.Array.Copy((Single[])elements, (Single[])elements2, ((Single[])elements).Length);
                    return elements2;
                case "Double[]":
                    elements = (Double[])data;
                    elements2 = new Object[((Double[])elements).Length];
                    System.Array.Copy((Double[])elements, (Double[])elements2, ((Double[])elements).Length);
                    return elements2;
                case "Object[]":
                    elements = (Object[])data;
                    elements2 = new Object[((Object[])elements).Length];
                    System.Array.Copy((Object[])elements, (Object[])elements2, ((Object[])elements).Length);
                    return elements2;
                default:
                    Slots slots =(Slots) data;
                    Slots slots2 = new Slots((uint)slots.slots.Length);
                    System.Array.Copy(slots.slots, slots2.slots, slots.slots.Length);
                    return slots2;
            }
        }
        internal Object Clone()
        {
            return new Object(clas, cloneData());
        }
    }
}
