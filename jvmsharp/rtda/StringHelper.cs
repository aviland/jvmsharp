using jvmsharp.rtda.heap;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace jvmsharp.rtda
{
    class StringHelper
    {

        public static heap.Object JString(ref ClassLoader loader,string goStr)
        {
            var internedStr = StringPool.getInternedString(goStr);
            if (internedStr != null)
            {
                return internedStr;
            }

            var chars = stringToUtf16(goStr);
            var jChars = new rtda.heap.Object(loader.LoadClass("[C"), chars);
            heap.Object jStr = loader.LoadClass("java/lang/String").NewObject();
            jStr.SetRefVar("value", "[C",ref  jChars);
            StringPool.internedStrings[goStr] = jStr;
            return jStr;
        }

        public static UInt16[] stringToUtf16(string s)
        {
            char[] ch = s.ToCharArray();
            UInt16[] u16 = new UInt16[ch.Length];
            for (int i = 0; i < u16.Length; i++)
                u16[i] = Convert.ToUInt16(ch[i]);
            return u16; // func Encode(s []rune) []uint16
        }
    }
}
