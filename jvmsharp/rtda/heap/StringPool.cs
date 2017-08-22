using jvmsharp.rtda.heap;
using System;
using System.Collections.Generic;
using System.Text;

namespace jvmsharp.rtda
{
    class StringPool
    {
        public static Dictionary<string, heap.Object> internedStrings = new Dictionary<string, heap.Object>();
        public static heap.Object getInternedString(string goStr)
        {
            if (internedStrings.Count > 0&& goStr !=null&& internedStrings.ContainsKey(goStr))
                return internedStrings[goStr];
            return null;
        }
        public static string GoString(ref heap.Object jStr)
        {
            heap.Object charArr = jStr.GetRefVar("value", "[C");
            return utf16ToString(charArr.Chars());
        }

        static string utf16ToString(ushort[] s)
        {
            byte[] bytes = new byte[s.Length];
            for (int i = 0; i < bytes.Length; i++)
                bytes[i] = Convert.ToByte(s[i]);
            string str =Encoding.Default.GetString(bytes);
        //    Console.WriteLine("+++++++++++++++++"+str);
            return str;
        }

        public static heap.Object InternString(ref heap.Object jStr)
        {
            string goStr = GoString(ref jStr);
           
            heap.Object internedStr = null;
            if (internedStrings.ContainsKey(goStr)) {
                internedStr = internedStrings[goStr];
                return internedStr;
            }

            internedStrings[goStr] = jStr;
            return jStr;
        }

        public static heap.Object JString(ClassLoader loader, string goStr)
        {
            var internedStr = getInternedString(goStr);
            if (internedStr != null)
            {
                return internedStr;
            }
         //   Console.Write("__________________goStr");
       //     Console.WriteLine(goStr == null);
            ushort[] chars = stringToUtf16(goStr);
            rtda.heap.Object jChars = new rtda.heap.Object(loader.LoadClass("[C"), chars,null);
            heap.Object jStr = loader.LoadClass("java/lang/String").NewObject();
            jStr.SetRefVar("value", "[C", ref jChars);
            internedStrings[goStr] = jStr;
            return jStr;
        }

        public static UInt16[] stringToUtf16(string s)
        {
            char[] ch = s.ToCharArray();
            ushort[] u16 = new ushort[ch.Length];
            for (int i = 0; i < u16.Length; i++)
            {
                u16[i] = Convert.ToUInt16(ch[i]); 
 //               Console.WriteLine(u16[i]);
            }
              
            return u16; // func Encode(s []rune) []uint16
        }
    }
}
