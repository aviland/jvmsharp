using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace jvmsharp.rtda
{
    class StringPool
    {
        public static Dictionary<string, heap.Object> internedStrings = new Dictionary<string, heap.Object>();
        public static heap.Object getInternedString(string goStr)
        {
            if (internedStrings.Count > 0&&internedStrings.ContainsKey(goStr))
                return internedStrings[goStr];
            return null;
        }
        public static string GoString(ref heap.Object jStr)
        {
            heap.Object charArr = jStr.GetRefVar("value", "[C");
            return utf16ToString(charArr.Chars());
        }

        static string utf16ToString(UInt16[] s)
        {
            byte[] bytes = new byte[s.Length];
            for (int i = 0; i < bytes.Length; i++)
                bytes[i] = Convert.ToByte(s[i]);
           string str= Encoding.Default.GetString(bytes);
            return str;
        }

        public static heap.Object InternString(string goStr, heap.Object jStr)
        {
            heap.Object internedStr = null;
            if (internedStrings.Count > 0)
                internedStr = internedStrings[goStr];

            if (internedStr != null)
            {
                return internedStr;
            }

            internedStrings[goStr] = jStr;
            return jStr;
        }
    }
}
