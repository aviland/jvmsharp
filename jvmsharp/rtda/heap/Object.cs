namespace jvmsharp.rtda.heap
{
    partial class Object
    {
        public Class clas;
        public Slots fields;
        public System.Object extra;
        public Monitor monitor;
        public System.Object locker;

        public Object(Class clas, Slots fields, System.Object extra, Monitor monitor, System.Object locker)
        {
            this.clas = clas;
            this.fields = fields;
            this.extra = extra;
            this.monitor = monitor;
            this.locker = locker;
        }

        public Object(ref Class clas, ref Slots fields)
        {
            this.clas = clas;
            this.fields = fields;
        }

        public Slots Fields()
        {
            return fields;
        }

        public static Object newObj(Class clas, Slots fields, System.Object extra)
        {
            return new Object(clas, fields, extra, new Monitor(), new System.Object());
        }

   /*     public void initFields()
        {
            Slots fields = this.fields;
            for (Class clas = this.clas; clas != null; clas = clas.superClass)
            {
                foreach (Field f in clas.fields)
                {
                    if (!f.IsStatic()) {
                        fields.slots[f.slotId].num = f.defaultValue();
                    }
                }
            }
        }*/
    }
}
