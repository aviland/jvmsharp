using System;

namespace jvmsharp.rtda.heap
{
    class ConstantMethodRef : MemberRef
    {
        internal Method method;
        internal ConstantMethodRef() { }


        public ConstantMethodRef(ref ConstantPool cp, classfile.ConstantMethodrefInfo refInfo)
        {
            this.cp = cp;
            classfile.ConstantMemberrefInfo cmi = refInfo;
            copyMemberRefInfo(ref cmi);
        }

      /*  internal Method StaticMethod()
        {
            if (method == null)
            {
                resolveStaticMethod();
            }
            return method;
        }*/

      /*  internal Method findMethod(bool isStatic)
        {
            var clas = ClassLoader.bootLoader.LoadClass(className);
            return clas.getMethod(name, descriptor, isStatic);
        }*/

      /*  internal void resolveStaticMethod()
        {
            var method = findMethod(true);
            if (method != null)
                this.method = method;
            else throw new Exception("static method not found!");
        }*/

    

        internal Method ResolvedMethod()
        {
            if (method == null)
                resolvedMethodRef();
            return method;
        }

        internal void resolvedMethodRef()
        {
            var d = this.cp.clas;
            var c = ResolvedClass();
            if (c.IsInterface())
                throw new Exception("java.lang.IncompatibleClassChangeError");
            var method = lookupMethod(ref c, name, descriptor);
            if (method == null)
                throw new Exception("java.lang.NoSuchMethodError");
            if (!method.isAccessibleTo(ref d))
                throw new Exception("java.lang.IllegalAccessError");
            this.method = method;
        }

        private Method lookupMethod(ref Class clas, string name, string descriptor)
        {
            var method = lookupMethodInClass(ref clas, name, descriptor);
            if(method==null)
                lookupMethodInInterfaces(ref clas.interfaces, name, descriptor);
            return method;
        }

        public Method lookupMethodInClass(ref Class clas,string name, string descriptor)
        {
            for(var c = clas; c != null; c = c.superClass)
            {
                foreach (Method m in c.methods)
                    if (m.Name() == name && m.Descriptor() == descriptor)
                        return m;
            }
            return null;
        }

        internal Method lookupMethodInInterfaces(ref Class[] ifaces, string name, string descriptor)
        {
            foreach (Class clas in ifaces)
            {
                foreach (Method m in clas.methods)
                {
                    if (m.Name() == name && m.Descriptor() == descriptor)
                        return m;
                }
                var method = lookupMethodInInterfaces(ref clas.interfaces, name, descriptor);
                if (method != null)
                    return method;
            }
            return null;
        }
    }
}
