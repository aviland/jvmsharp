using System;

namespace jvmsharp.rtda.heap
{
    class ConstantFieldRef : MemberRef
    {
        Field field;

        public ConstantPool CP()
        {
            return cp;
        }

        public ConstantFieldRef( ref ConstantPool cp,  classfile.ConstantFieldrefInfo refInfo)
        {
            this.cp = cp;
            classfile.ConstantMemberrefInfo cmi = refInfo;
            copyMemberRefInfo(ref cmi);
        }

        public Field ResolvedField()
        {
            if (field == null)
                resolvedFieldRef();
            return field;
        }

        public void resolvedFieldRef()
        {
            Class d = cp.clas;
        
            Class c = ResolvedClass();
           Field f = lookupField(ref c, name, descriptor);
            if (f == null)
                throw new Exception("java.lang.NoSuchFieldError");
            if (!f.isAccessibleTo(ref d))
                throw new Exception("java.lang.IllegalAccessError");
            field = f;
        }

        public Field lookupField(ref Class c, string name, string descriptor)
        {
            foreach (Field field in c.fields)
            {
                if (field.Name() == name && field.Descriptor() == descriptor)
                    return field;
            }
            for (int i = 0; i < c.interfaces.Length; i++)
            {
                Field field = lookupField(ref c.interfaces[i], name, descriptor);
                if (field != null)
                    return field;
            }
            if (c.superClass != null)
                return lookupField(ref c.superClass, name, descriptor);
            return null;
        }
    }
}
