using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using jvmsharp.rtda.heap;

namespace jvmsharp.native.java.lang
{
    class ClassHelper
    {
        public static rtda.heap.Object getSignatureStr( ref rtda.heap.ClassLoader loader, string signature)
        {
            //  Console.WriteLine("__________________");
            //      Console.WriteLine(signature==null);
            if (signature != null && signature != "")
            {
                return rtda.StringPool.JString( loader, signature);
            }
            return null;
        }

        public static rtda.heap.Object toByteArr(ref rtda.heap.ClassLoader loader,  byte[] goBytes)
        {
            if (goBytes != null)
            {
                sbyte[] jBytes = castUint8sToInt8s(goBytes);
                return rtda.heap.Class.NewByteArray(loader, jBytes);
            }
            return null;
        }

        private static sbyte[] castUint8sToInt8s(byte[] goBytes)
        {
            sbyte[] sbytes = new sbyte[goBytes.Length];
            for (int i = 0; i < goBytes.Length; i++)
            {
                sbytes[i] = Convert.ToSByte(goBytes[i]);
            }
            return sbytes;
        }

        internal static rtda.heap.Object toClassArr(ref rtda.heap.ClassLoader loader, rtda.heap.Class[] classes)
        {
            //  Console.WriteLine("+++++++++++++++++++");
            //     Console.WriteLine(classes == null);
            int arrLen = 0;
            if (classes != null)
                arrLen = classes.Length;

            var classArrClass = loader.LoadClass("java/lang/Class").ArrayClass();
                var classArr = classArrClass.NewArray((uint)(arrLen));

                if (arrLen > 0)
                {
                    rtda.heap.Object[] classObjs = classArr.Refs();
                    for (int i = 0; i < classes.Length; i++)
                        classObjs[i] = classes[i].JClass();
                }
                return classArr;

        }
    }
}
