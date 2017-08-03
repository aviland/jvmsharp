namespace jvmsharp.rtda.heap
{
    class MethodVtable
    {
        /*    static int getVslot(ref Class clas, string name, string descriptor)
            {
                for (int i = 0; i < clas.vtable.Length; i++)
                {
                    if (clas.vtable[i].Name() == name && clas.vtable[i].Descriptor() == descriptor)
                    {
                        return i;
                    }
                }
                // todo
                return -1;
            }

            static createVtable( ref Class clas) {
                clas.vtable = copySuperVtable(ref clas);

        for _, m := range class.methods {
            if isVirtualMethod(m)
    {
        if i := indexOf(class.vtable, m); i > -1 {
                    class.vtable[i] = m // override
                } else {

                    addVmethod(class, m)
                }
            }
        }

        _eachInterfaceMethod(class, func(m* Method)
    {
        if i := indexOf(class.vtable, m); i< 0 {

                addVmethod(class, m)
            }
        })
    }

    static Method[]  copySuperVtable(ref  Class clas) 
    {
        if (clas.superClass != null) {
            Method[] superVtable= clas.superClass.vtable;
                    Method[] newVtable = new Method[superVtable.Length];
                   Array.Copy(newVtable, superVtable,superVtable.Length);
                    return newVtable;
        } else {
            return null;
        }
    }

    static bool isVirtualMethod(ref  Method method)  {
        return !method.IsStatic() &&
            //!method.IsFinal() &&
            !method.IsPrivate() &&
            method.Name() != Method.constructorName;
    }

    static int indexOf(ref Method[] vtable, ref Method m)
    {
        for (int i = 0; i < vtable.Length; i++)
        {
            if (vtable[i].Name() == m.Name() && vtable[i].Descriptor() == m.Descriptor())
                return i;
        }
        return -1;
    }

    void addVmethod( ref   Class clas, ref Method m) {
    int _len= clas.vtable.Length;
            if (_len == clas.vtable.Length)//cap length
        {
            Method[] newVtable = new Method[_len + 8];

           Array.Copy(clas.vtable,newVtable , clas.vtable.Length);
            clas.vtable = newVtable;
        }
            clas.vtable[_len] = m;
    }

    // visit all interface methods
    void  _eachInterfaceMethod( ref Class clas, ref Method f) {
        for _, iface := range class.interfaces {

            _eachInterfaceMethod(iface, f)
            for _, m := range iface.methods
    {
        f(m)
            }
        }
    */
    }
}
