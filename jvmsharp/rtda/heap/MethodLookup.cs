using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace jvmsharp.rtda.heap
{
    class MethodLookup
    {
        public static Method lookupMethodInClass(ref Class clas, string name, string descriptor)
        {
            for (Class c = clas; c != null; c = c.superClass)
            {
               if (c.methods != null)
         
                    foreach (Method method in c.methods)
                        if (method.Name() == name && method.Descriptor() == descriptor)
                            return method;
                
            }
            return null;
        }

        internal static Method lookupMethodInInterfaces(Class[] ifaces, string name, string descriptor)
        {
            foreach (Class clas in ifaces)
            {
                foreach (Method m in clas.methods)
                {
                    if (m.Name() == name && m.Descriptor() == descriptor)
                        return m;
                }
                var method = lookupMethodInInterfaces(clas.interfaces, name, descriptor);
                if (method != null)
                    return method;
            }
            return null;
        }
    }
}
