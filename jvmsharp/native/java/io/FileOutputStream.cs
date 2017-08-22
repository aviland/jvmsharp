using System;
using System.Linq;
using jvmsharp.rtda;
using System.Text;

namespace jvmsharp.native.java.io
{
    class FileOutputStream
    {
        public static void init()
        {
            Registry.Register("java/io/FileOutputStream", "writeBytes", "([BIIZ)V", writeBytes);
        }

        private static void writeBytes(ref Frame frame)
        {
            var vars = frame.LocalVars();
            //this := vars.GetRef(0)
            var b = vars.GetRef(1);
            var off = vars.GetInt(2);
            var len = vars.GetInt(3);
            //append := vars.GetBoolean(4)
            var jBytes = (sbyte[])b.data;
            byte[] goBytes = castInt8sToUint8s(jBytes);
             goBytes = goBytes.Skip(off).Take(len).ToArray();
            Console.Write(Encoding.Default.GetString(goBytes));
        }

        private static byte[] castInt8sToUint8s(sbyte[] jBytes)
        {
            byte[] bytes = new byte[jBytes.Length];
            for (int i = 0; i < jBytes.Length; i++)
            {
                bytes[i] = (byte)(jBytes[i]);
            }
            return bytes;
        }
    }
}
