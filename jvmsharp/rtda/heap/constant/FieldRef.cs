using jvmsharp.classfile;
using System;

namespace jvmsharp.rtda.heap
{
    class FieldRef : MemberRef
    {
        public Field field;

        public ConstantPool CP()
        {
            return cp;
        }

        public FieldRef newConstantFieldRef(ref ConstantPool cp,ref classfile.ConstantFieldrefInfo refInfo)
        {
            FieldRef refs = new FieldRef();
            refs.cp = cp;
            ConstantMemberrefInfo cmi = refInfo;
            refs.copyMemberRefInfo(cmi);
            return refs;
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
            Field f = lookupField(ref c, ref name, ref descriptor);
            if (f == null)
                throw new Exception("java.lang.NoSuchFieldError");
            if (!f.isAccessibleTo( d))
                throw new Exception("java.lang.IllegalAccessError");
            field = f;
        }

        public Field lookupField(ref Class c, ref string name, ref string descriptor)
        {
            foreach (Field field in c.fields)
            {
                if (field.Name() == name && field.Descriptor() == descriptor)
                    return field;
            }
            if (c.interfaces != null) { 
                for (int i = 0; i < c.interfaces.Length; i++)
                {
                    Field field = lookupField(ref c.interfaces[i], ref name, ref descriptor);
                    if (field != null)
                        return field;
                }
            }
            if (c.superClass != null)
                return lookupField(ref c.superClass, ref name, ref descriptor);
            return null;
        }
    }
}
