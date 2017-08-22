using System;

namespace jvmsharp.rtda.heap
{
    class Field : ClassMember
    {
        uint constValueIndex;
        public uint slotId;

        public Field()
        {
        }

        public uint ConstValueIndex()
        {
            return constValueIndex;
        }

        public bool IsLongOrDouble()
        {
            return descriptor == "J" || descriptor == "D";
        }

        public Field[] newFields(ref Class clas, ref classfile.MemberInfo[] cfFields)
        {
            Field[] fields = new Field[cfFields.Length];
            for (int i = 0; i < cfFields.Length; i++)
            {
                fields[i] = new Field();
                fields[i].clas = clas;
                fields[i].copyMemberInfo(ref cfFields[i]);
                fields[i].copyAttributes(ref cfFields[i]);
            }
            return fields;
        }

        public void copyAttributes(ref classfile.MemberInfo cfField)
        {
            classfile.ConstantValueAttribute valAttr = cfField.ConstantValueAttribute();
            if (valAttr != null)
                constValueIndex = valAttr.ConstantValueIndex();
        }

        internal Class Type()
        {
            string className = new ClassNameHelper().toClassName(descriptor);
            return clas.loader.LoadClass(className);
        }

        internal uint SlotId()
        {
            return this.slotId;
        }
    }
}
