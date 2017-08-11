namespace jvmsharp.rtda.heap
{
    partial class Class : ClassAttributes
    {
        const int _notInitialized = 0;// This Class object is verified and prepared but not initialized.
        const int _beingInitialized = 1;// This Class object is being initialized by some particular thread T.
        const int _fullyInitialized = 2; // This Class object is fully initialized and ready for use.
        const int _initFailed = 3;// This Class object is in an erroneous state, perhaps because initialization was attempted and failed.

        internal readonly ConstantPool constantPool;
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

        internal classpath.Entry LoadedFrom()
        {
            return loadedFrom;
        }

        public Method GetClinitMethod()
        {
            return GetStaticMethod("<clinit>", "()V");
        }

        internal Method GetStaticMethod(string name, string descriptor)
        {
            return getMethod(name, descriptor, true);
        }

        public Class() { }

        public string JavaName()
        {
            return name.Replace('/', '.');
        }

        public Class(string name)
        {
            this.name = name;
        }

        public void MarkFullyInitialized()
        {
            initState = _fullyInitialized;
        }

        public Class(classfile.ClassFile cf)
        {
            //       Class c = new Class();
            accessFlags = cf.AccessFlags();

            name = cf.ClassName();
            superClassName = cf.SuperClassName();
            interfaceNames = cf.InterfacesNames();

            constantPool = new ConstantPool().newConstantPool(this, ref cf.ConstantPool().constantInfos);
            fields = new Field().newFields(this, cf.Fields());
            methods = new Method().newMethods(this, cf.Methods());

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
            return new Object().newObject(this);
        }


        public Field GetInstanceField(string name, string descriptor)
        {
            return getField(name, descriptor, false);
        }

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
        internal ClassLoader Loader()
        {
            return loader;
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

        public Field getField(string name, string descriptor, bool isStatic)
        {
            for (var k = this; k != null; k = k.superClass)
            {
                foreach (Field field in fields)
                {
                    if (field.IsStatic() == isStatic && field.Name() == name && field.Descriptor() == descriptor)
                        return field;
                }
            }
            return null;
        }

         internal Object GetRefVar(string fieldName, string fieldDescriptor)
        {
            var field = getField(fieldName, fieldDescriptor, true);
            return staticVars.GetRef(field.slotId);
        }

        internal Method GetInstanceMethod(string name, string descriptor)
        {
            return getMethod(name, descriptor, false);
        }
    }
}
