using jvmsharp.rtda;
using jvmsharp.rtda.heap;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace jvmsharp.instructions
{
    class classInit_logic
    {
     public   static void InitClass(ref Thread thread,ref Class clas)
        {
            clas.StartInit();
            scheduleClinit(ref thread, ref clas);
            initSuperClass(ref thread, ref clas);
        }

        static void scheduleClinit(ref Thread thread, ref Class cla)
        {
            var clinit = cla.GetClinitMethod();
            if (clinit != null)
            {
                Frame newFrame = thread.newFrame(ref clinit);
                thread.PushFrame(ref newFrame);
            }
        }

        static void initSuperClass(ref Thread thread, ref Class clas)
        {
            if (!clas.IsInterface())
            {
                Class superClass = clas.superClass;
                if (superClass != null && !superClass.InitStarted())
                    InitClass(ref thread, ref superClass);
            }
        }
    }
}
