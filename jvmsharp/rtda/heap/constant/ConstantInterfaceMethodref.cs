using System;

namespace jvmsharp.rtda.heap
{
    class ConstantInterfaceMethodref : ConstantMethodRef
    {
        internal ConstantInterfaceMethodref(ref ConstantPool cp, classfile.ConstantInterfaceMethodrefInfo refInfo)
        {
            this.cp = cp;
            copy(refInfo);
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
            if (!method.isAccessibleTo(ref d))
                throw new Exception("java.lang.IllegalAccessError");
            this.method = method;
        }

        private Method lookupInterfaceMethod(ref Class iface, string name, string descriptor)
        {
            foreach (Method m in iface.methods)
            {
                if (m.Name() == name && method.Descriptor() == descriptor)
                    return m;
            }
            return lookupMethodInInterfaces(ref iface.interfaces, name, descriptor);
        }
    }
}
