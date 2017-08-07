namespace jvmsharp.rtda.heap
{
    partial class Object
    {
        public bool IsInstanceOf(ref Class clas)
        {
            return clas.IsAssignableFrom(this.clas);
        }

        /*
        public bool IsInstanceOf(ref Class clas)
        {
            Class s = this.clas;
            Class t = clas;
            return _checkcast(ref s, ref t);
        }

        public bool _checkcast(ref Class s, ref Class t)
        {
            if (s == t)
                return true;
            if (!s.IsArray())
            {
                if (!s.IsInterface())
                {
                    if (!t.IsInterface())
                        return s.isSubClassOf(t);
                    else
                        return s.isImplements(t);
                }
                else
                {
                    if (!t.IsInterface())
                        return t.isJlObject();
                    else
                        return t.isSuperInterfaceOf(s);
                }
            }
            else
            { // s is array
                if (!t.IsArray())
                {
                    if (!t.IsInterface())
                        return t.isJlObject();
                    else
                        return t.isJlCloneable() || t.isJioSerializable();
                }
                else
                { // t is array
             //       Class sc = s.ComponentClass();
           //         Class tc = t.ComponentClass();
           //         return sc == tc || _checkcast(ref sc, ref tc);
                }
            }
                return false;
        }*/
    }
}
