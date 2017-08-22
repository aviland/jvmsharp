using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using jvmsharp.rtda;
using System.IO;

namespace jvmsharp.native.java.io
{
    class FileInputStream
    {
        public static void init()
        {
            Registry.Register("java/io/FileInputStream", "close0", "()V", close0);
        }

        private static void close0(ref Frame frame)
        {
            var vars = frame.LocalVars();
                        var thi = vars.GetThis();
         var goFile= (FileStream)thi.Extra();
            goFile.Close();
        }
    }
}
