using System;

namespace jvmsharp.rtda.heap
{
    class InterfaceMethodref : MethodRef
    {
        Method method;
        internal InterfaceMethodref newInterfaceMethodref(ref ConstantPool cp, classfile.ConstantInterfaceMethodrefInfo refInfo)
        {
            InterfaceMethodref refs = new InterfaceMethodref();

            refs.cp = cp;
            refs.copyMemberRefInfo(refInfo);
            return refs;
        }


        internal Method ResolvedInterfaceMethod()
        {
            if (method == null)
                resolvedInterfaceMethodRef();
            return method;
        }

        internal void resolvedInterfaceMethodRef()
        {
            Class d = cp.clas;
            Class c = ResolvedClass();
            if (!c.IsInterface())
                throw new Exception("java.lang.IncompatibleClassChangeError");
            Method method = lookupInterfaceMethod(ref c, name, descriptor);
            if (method == null)
                throw new Exception("java.lang.NoSuchMethodError");
            if (!method.isAccessibleTo(d))
                throw new Exception("java.lang.IllegalAccessError");
            this.method = method;
        }

        private Method lookupInterfaceMethod(ref Class iface, string name, string descriptor)
        {
        //    Console.WriteLine(name + "\t" + raw);
            
            foreach (Method m in iface.methods)
            {
            //    Console.WriteLine(m.Name()+"\t"+m.raw);
                if (m.Name() == name && m.Descriptor() == descriptor)
                    return m;
            }
            return MethodLookup.lookupMethodInInterfaces(iface.interfaces, name, descriptor);
        }
    }
}
