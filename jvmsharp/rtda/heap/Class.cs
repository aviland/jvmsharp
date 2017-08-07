namespace jvmsharp.rtda.heap
{
    partial class Class : ClassAttributes
    {
        const int _notInitialized = 0;// This Class object is verified and prepared but not initialized.
        const int _beingInitialized = 1;// This Class object is being initialized by some particular thread T.
        const int _fullyInitialized = 2; // This Class object is fully initialized and ready for use.
        const int _initFailed = 3;// This Class object is in an erroneous state, perhaps because initialization was attempted and failed.

        ConstantPool constantPool;
        public string name;
        public string superClassName;
        public string[] interfaceNames;
        internal bool initStarted;
        public Field[] fields;
        public Method[] methods;
    //    public uint instanceFieldCount;
//public uint staticFieldCount;
     //   public object[] staticFieldSlots;
   //     public Method[] vtable;// virtual method table
        public Object jClass;  // java.lang.Class instance
        public Class superClass;
        public Class[] interfaces;
        public classpath.Entry loadedFrom; // todo
        public int initState;
        internal ClassLoader loader;

        public uint instanceSlotCount;
        public uint staticSlotCount;
        public Slots staticVars;

        public bool InitStarted()
        {
            return initStarted;
        }

        public void StartInit()
        {
            initStarted = true;
        }

        public Method GetClinitMethod()
        {
            return GetStaticMethod("<clinit>", "()V");
        }

        Method GetStaticMethod(string name,  string descriptor)  {
            return getMethod(name, descriptor, true);
}

    public ConstantPool ConstantPool()
        {
            return constantPool;
        }

        public Class() { }

        public Class(string name)
        {
            this.name = name;
        }

        public void MarkFullyInitialized()
        {
            initState = _fullyInitialized;
        }

        public Class newClass(classfile.ClassFile cf)
        {
            Class c = new Class();
            c.accessFlags = cf.AccessFlags();

            c.name = cf.ClassName();
            c.superClassName = cf.SuperClassName();
            c.interfaceNames = cf.InterfacesNames();

            c.constantPool = new ConstantPool().newConstantPool(ref c, ref cf.ConstantPool().constantInfos);
            c.fields = new Field().newFields(ref c, cf.Fields());
            c.methods = new Method().newMethods(ref c, cf.Methods());
            return c;
        }

        public bool isAccessibleTo(ref Class other)
        {
            return IsPublic() || getPackageName() == other.getPackageName();
        }

        public string getPackageName()
        {
            int i = name.LastIndexOf('/');
            if (i >= 0)
            {
                return name.Substring(0, i);
            }
            return "";
        }

        public Object NewObject()
        {
            return newObject(this);
        }

        Object newObject(Class clas)
        {
            Slots s = new Slots(clas.instanceSlotCount);
            return new heap.Object(clas, s);
        }
        /*      public Object NewObj()
              {
                  if (this.instanceFieldCount > 0)
                  {
                      System.Object[] fields = new System.Object[this.instanceFieldCount];
                      Object obj = Object.newObj(this, fields, null);
                      obj.initFields();
                      return obj;
                  }
                  else
                      return Object.newObj(this, null, null);
              }*/
        public bool isJlObject()
        {
            return this == ClassLoader._jlObjectClass;
        }
        public bool isJlCloneable()
        {
            return this == ClassLoader._jlCloneableClass;
        }
        public bool isJioSerializable()
        {
            return this == ClassLoader._ioSerializableClass;
        }
        public Method GetMainMethod()
        {
            return getStaticField("main", "([Ljava/lang/String;)V");
        }

        internal Method getStaticField(string name, string descriptor)
        {
            foreach (Method method in methods)
            {
                // Console.WriteLine("method name: " + method.Name()+ "\tstatic: " + method.IsStatic()+ "\tmethod descriptor:" + method.Descriptor());
                if (method.IsStatic() && method.Name() == name && method.Descriptor() == descriptor)
                {
                    return method;
                }
            }
            return null;
        }

        internal bool InitializationNotStarted()
        {
            return initState < _beingInitialized;
        }

        internal Method getMethod(string name, string descriptor, bool isStatic)
        {
            for (var k = this; k != null; k = k.superClass)
            {
                foreach (var method in k.methods)
                {
                    if (method.IsStatic() == isStatic && method.Name() == name && method.Descriptor() == descriptor)
                        return method;
                }
            }
            // todo
            return null;
        }
    }
}
