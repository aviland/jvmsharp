using System;

namespace jvmsharp.rtda.heap
{
    partial class Object
    {
        public Class clas;
        public object data;
        public object extra;
        public Object() { }
        public Object(Class clas)
        {
            this.clas = clas;
            this.data = new object[clas.instanceSlotCount];
        }

        public Object GetRefVar(string name,string descriptor)
        {
            Field field = clas.getField(name, descriptor, false);
            Slots slots = Fields();
            return slots.GetRef(field.slotId);
        }

        public Object newObj(Class clas, object data, object extra)
        {
            Object obj = new Object(clas);
            obj.extra = extra;
            return obj;
        }
        internal object Extra()
        {
            return extra;
        }
        public Object(Class clas, object data)
        {
            this.clas = clas;
            this.data = data;
        }

        public Slots Fields()
        {
            return new Slots(data);
        }
        public void SetRefVar(string name,string descriptor,ref Object refs)
        {
            var field = clas.getField(name, descriptor, false);
            var slots =(Slots) data;
            slots.SetRef(field.slotId, refs);
        }
        public void SetFieldValue(string fieldName, string fieldDescriptor, object value)
        {
            var field = clas.GetInstanceField(fieldName, fieldDescriptor);

            field.PutValue(this, value);
        }
    }
}
