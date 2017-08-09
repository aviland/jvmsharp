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

        public Field[] newFields(ref Class clas, classfile.MemberInfo[] cfFields)
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
        public void PutValue(Object refs, object val)
        {
            var fields = refs.Fields();
            fields.slots[slotId].refer = (heap.Object)val;
        }
        public object defaultValue()
        {
            switch (descriptor[0])
            {
                case 'Z': // boolean
                    return 0;
                case 'B': // byte
                    return 0;
                case 'S': // short
                    return 0;
                case 'C': // char
                    return 0;
                case 'I': // int
                    return 0;
                case 'J': // long
                    return (long)(0);
                case 'F': // float
                    return (float)(0);
                case 'D': // double
                    return (double)(0);
                case 'L': // Object
                    return null;
                case '[': // Array
                    return null;
                default:
                    throw new Exception("BAD field descriptor: " + descriptor);
            }
        }
    }
}
