using System;

namespace jvmsharp.rtda.heap
{
    partial class Object
    {
        public Class clas;
        public object data;

        public Object(Class clas)
        {
            this.clas = clas;
            this.data = new object[clas.instanceSlotCount];
        }

        public Object(Class clas,object data)
        {
            this.clas = clas;
            this.data = data;
        }

        public Slots Fields()
        {
            return new Slots(data);
        }
    }
}
