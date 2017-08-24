using System;

namespace jvmsharp.rtda.heap
{
    class SymRef
    {
        public ConstantPool cp;
        public string className;
        public Class clas;

        public Class ResolvedClass()
        {
            if (clas == null)
                resolveClassRef();
            return clas;
        }

        public void resolveClassRef()
        {
            Class d = cp.clas; 
            Class c = d.loader.LoadClass(className);
           
            if (!c.isAccessibleTo(ref d))//检查d是否可以访问c，即检查当前常量池中是否含有c
            {
                throw new Exception("java.lang.IllegalAccessError");
            }
            clas = c;
        }
    }
}
