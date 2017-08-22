using System;

namespace jvmsharp.rtda.heap
{
     partial class Object
    {
        internal Class clas;
        internal object data;
        internal object extra;
        public Object() { }

        internal Class Class()
        {
            return this.clas;
        }
        public Object newObject(Class clas)
        {
            Object obj = new Object();
            obj.clas = clas;
            obj.data = new Slots(clas.instanceSlotCount);
            return obj;
        }
        internal object Data()
        {
            return data;
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
        public Object(Class clas, object data,object extra)
        {
            this.clas = clas;
            this.data = data;
            this.extra = extra;
        }
        public Slots Fields()
        {
            return (Slots)(data);
        }
        public bool IsInstanceOf(ref Class clas)
        {
            return clas.IsAssignableFrom(ref this.clas);
        }
        internal void SetRefVar(string name, string descriptor, ref Object refs)
        {
            Field field = clas.getField(name, descriptor, false);
            Slots slots = (Slots)data;
            slots.SetRef(field.slotId, refs);
            data = slots;
        }
        internal Object GetRefVar(string name, string descriptor)
        {
            Field field = clas.getField(name, descriptor, false);
            Slots slots = (Slots)data;
            return slots.GetRef(field.slotId);
        }
        internal void SetIntVar(string name, string descriptor, int val)
        {
            Field field = clas.getField(name, descriptor, false);
            Slots slots = (Slots)data;
            slots.SetInt(field.slotId, val);
            data = slots;
        }

        internal int GetIntVar(string name, string descriptor)
        {
            Field field = clas.getField(name, descriptor, false);
            Slots slots = (Slots)data;
            return slots.GetInt(field.slotId);
        }

        internal void SetExtra(object extra)
        {
            this.extra = extra;
        }
    }
}
