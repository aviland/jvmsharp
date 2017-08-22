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
            switch (data.GetType().Name)
            {
                case "SByte[]":
                    SByte[] elementsSByte = (SByte[])data;
                    SByte[] elements2SByte = new SByte[elementsSByte.Length];
                    System.Array.Copy(elementsSByte, elements2SByte, elementsSByte.Length);
                    return elementsSByte;
                case "Int16[]":
                    Int16[] elementsInt16 = (Int16[])data;
                    Int16[] elements2Int16 = new Int16[elementsInt16.Length];
                    System.Array.Copy(elementsInt16, elements2Int16, elementsInt16.Length);
                    return elements2Int16;
                case "UInt16[]":
                    UInt16[]  elementsUInt16 = (UInt16[])data;
                    UInt16[] elements2UInt16 = new UInt16[elementsUInt16.Length];
                    System.Array.Copy(elementsUInt16, elements2UInt16, elementsUInt16.Length);
                    return elements2UInt16;
                case "Int32[]":
                    Int32[] elementsInt32 = (Int32[])data;
                    Int32[] elements2Int32 = new Int32[elementsInt32.Length];
                    System.Array.Copy(elementsInt32, elements2Int32, elementsInt32.Length);
                    return elements2Int32;
                case "Int64[]":
                    Int64[] elementsInt64 = (Int64[])data;
                    Int64[] elements2Int64 = new Int64[elementsInt64.Length];
                    System.Array.Copy(elementsInt64, elements2Int64, elementsInt64.Length);
                    return elements2Int64;
                case "Single[]":
                    Single[] elementsSingle = (Single[])data;
                    Single[] elements2Single = new Single[elementsSingle.Length];
                    System.Array.Copy(elementsSingle, elements2Single, elementsSingle.Length);
                    return elements2Single;
                case "Double[]":
                    Double[] elementsDouble = (Double[])data;
                    Double[] elements2Double = new Double[elementsDouble.Length];
                    System.Array.Copy(elementsDouble, elements2Double,elementsDouble.Length);
                    return elements2Double;
                case "Object[]":
                    Object[] elementsObject = (Object[])data;
                    Object[] elements2Object = new Object[elementsObject.Length];
                    System.Array.Copy(elementsObject, elements2Object, elementsObject.Length);
                    return elements2Object;
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
