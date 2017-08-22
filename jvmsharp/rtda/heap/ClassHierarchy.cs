using System;
using System.Collections.Generic;

namespace jvmsharp.rtda.heap
{
    partial class Class
    {

        public bool IsSubClassOf(Class other)//是否为c的子类
        {
            for (Class k = superClass; k != null; k = k.superClass)
            {
                if (k == other)
                    return true;
            }
            return false;
        }

        public bool IsSuperClassOf(Class c)//是否为c的超类
        {
            return c.IsSubClassOf(this);
        }

        public bool isSubInterfaceOf(Class iface)
        {
            if (interfaces != null && interfaces.Length > 0)
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
                if(k.interfaces!=null)
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
            if (!s.IsArray())//s非数组
            {
                if (!s.IsInterface())
                {
                    if (!t.IsInterface())
                        return s.IsSubClassOf(t);
                    else return s.isImplements(t);
                }
                else
                {
                    if (!t.IsInterface())
                        return t.isJlObject();
                    else return t.isSubInterfaceOf(s);
                }
            }
            else
            {
                if (!t.IsArray())
                {
                    if (!t.IsInterface())
                        return t.isJlObject();
                    else return t.isJlCloneable() || t.isJioSerializable();
                }
                else
                {
                    var sc = s.ComponentClass();
                    var tc = t.ComponentClass();
                    return sc == tc || tc.IsAssignableFrom(ref sc);
                }
            }
        }
    }
}
