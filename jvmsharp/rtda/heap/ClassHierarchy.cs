namespace jvmsharp.rtda.heap
{
    partial class Class : ClassAttributes
    {
 
        public bool isSubClassOf(Class c)//是否为c的子类
        {
            for (Class k = superClass; k != null; k = k.superClass)
            {
                if (k == c)
                    return true;
            }
            return false;
        }

        public bool isSuperClassOf(Class c)//是否为c的超类
        {
            return c.isSubClassOf(this);
        }

        public bool isSubInterfaceOf(Class iface)
        {
            foreach (Class superInterface in interfaces)
            {
                if (superInterface == iface || superInterface.isSubInterfaceOf(iface))
                    return true;
            }
            return false;
        }

        public bool isImplements(Class iface)
        {
            for (Class k = this; k != null; k = k.superClass)
            {
                foreach (Class i in k.interfaces)
                {
                    if (i == iface || i.isSubInterfaceOf(iface))
                        return true;
                }
            }
            return false;
        }

        public bool isSuperInterfaceOf(Class iface)
        {
            return iface.isSubInterfaceOf(this);
        }

        public bool IsAssignableFrom(ref Class other)
        {
            Class s = other;
            Class t = this;
            if (s == t)
                return true;
            if (!t.IsInterface())
                return s.isSubClassOf(t);
            else return s.isImplements(t);
        }
    }
}
