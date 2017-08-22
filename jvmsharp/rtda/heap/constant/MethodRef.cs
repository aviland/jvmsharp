using jvmsharp.classfile;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace jvmsharp.rtda.heap
{
    class MethodRef : MemberRef
    {
        Method method;

      internal  MethodRef newMethodRef(ref ConstantPool cp, classfile.ConstantMethodrefInfo refInfo)
        {
            var refs = new MethodRef();
            refs.cp = cp;
            refs.copyMemberRefInfo(refInfo);
            return refs;
        }


        internal Method ResolvedMethod()
        {
            if (this.method == null)
                resolvedMethodRef();
            return this.method;
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
            if (!method.isAccessibleTo(d))
                throw new Exception("java.lang.IllegalAccessError");
            this.method = method;
        }
        private Method lookupMethod(ref Class clas, string name, string descriptor)
        {
            var method =MethodLookup.lookupMethodInClass(ref clas, name, descriptor);
            if (method == null)
                method = MethodLookup.lookupMethodInInterfaces(clas.interfaces, name, descriptor);
            return method;
        }

    }
}
