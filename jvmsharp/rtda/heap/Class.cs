using System;
using jvmsharp.classfile;
using System.Collections.Generic;

namespace jvmsharp.rtda.heap
{
    partial class Class
    {
        internal ushort accessFlags;
        internal string name;
        internal string superClassName;
        internal string[] interfaceNames;
        internal  ConstantPool constantPool;
        internal Field[] fields;
        internal Method[] methods;
        internal string sourceFile;
        internal ClassLoader loader;
        internal Class superClass;
        internal Class[] interfaces;
        internal uint instanceSlotCount;
        internal uint staticSlotCount;
        internal Slots staticVars;
        internal bool initStarted;
        public Object jClass;

        internal void SetRefVar(string fieldName, string fieldDescriptor, heap.Object refs)
        {
            var field = getField(fieldName, fieldDescriptor, true);
            staticVars.SetRef(field.slotId, refs);
        }

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
            return getMethod("<clinit>", "()V",true);
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

        internal Class SuperClass()
        {
            return this.superClass;
        }

        public Class(string name)
        {
            this.name = name;
        }

        public Class newClass(ref ClassFile cf)
        {
            Class c = new Class();
            c.accessFlags = cf.AccessFlags();

            c.name = cf.ClassName();
            c.superClassName = cf.SuperClassName();
            c.interfaceNames = cf.InterfacesNames();

            c.constantPool = new ConstantPool().newConstantPool(ref c,ref cf.constantPool);
            c.fields = new Field().newFields(ref c, ref cf.fields);
            c.methods = new Method().newMethods(ref c, ref cf.methods);
            c.sourceFile = getSourceFile(ref cf);
            return c;
        }

        private string getSourceFile(ref ClassFile cf)
        {
            SourceFileAttribute sfAttr = cf.SourceFileAttribute();
            if (sfAttr != null)
                return sfAttr.FileName();
            return "Unknown";
        }
        bool IsPublic()
        {
            return 0 != (this.accessFlags & AccessFlags.ACC_PUBLIC);
        }
        public bool IsFinal()
        {
            return 0 != (accessFlags & AccessFlags.ACC_FINAL);
        }
        public bool IsSuper()
        {
            return 0 != (accessFlags & AccessFlags.ACC_SUPER);
        }
        public bool IsInterface()
        {
            return 0 != (accessFlags & AccessFlags.ACC_INTERFACE);
        }
        public bool IsAbstract()
        {
            return 0 != (accessFlags & AccessFlags.ACC_ABSTRACT);
        }
        public bool IsSynthetic()
        {
            return 0 != (accessFlags & AccessFlags.ACC_SYNTHETIC);
        }
        public bool IsAnnotation()  {
            return 0 != (accessFlags & AccessFlags.ACC_ANNOTATION);
    }
        public bool IsEnum()
        {
            return 0 != (accessFlags & AccessFlags.ACC_ENUM);
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
            return name == "java/lang/Object";
        }
        public bool isJlCloneable()
        {
            return name == "java/lang/Cloneable";
        }
        public bool isJioSerializable()
        {
            return name == "java/io/Serializable";
        }
        public Method GetMainMethod()
        {
            return getMethod("main", "([Ljava/lang/String;)V",true);
        }
        internal ClassLoader Loader()
        {
            return loader;
        }
        internal Method getStaticField(string name, string descriptor)
        {
            foreach (Method method in methods)
            {
                // Console.WriteLine("method name: " + method.Name()+ "\tstatic: " + method.IsStatic()+ "\tmethod raw:" + method.Descriptor());
                if (method.IsStatic() && method.Name() == name && method.Descriptor() == descriptor)
                {
                    return method;
                }
            }
            return null;
        }

        internal Method getMethod(string name, string descriptor, bool isStatic)
        {
            for (Class k = this; k != null; k = k.superClass)
            {
                foreach (var method in k.methods)
                {
                    if (method.IsStatic() == isStatic && method.Name() == name && method.descriptor == descriptor)
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
                foreach (Field field in k.fields)
                {
                    if (field.IsStatic() == isStatic && field.Name() == name && field.Descriptor() == descriptor)
                        return field;
                }
            }
            return null;
        }
        internal Method[] GetConstructors(bool publicOnly)
        {
            List<Method> constructors = new List<Method>();
            foreach (Method method in methods)
            {
                if (method.isConstructor())
                {
                    if (!publicOnly || method.IsPublic())
                        constructors.Add(method);
                }
            }
            return constructors.ToArray();
        }
        Method[] GetMethods(bool publicOnly)
        {
            List<Method> methods = new List<Method>();
            foreach (Method method in this.methods)
            {
                if (!method.isClinit() && !method.isConstructor())
                {
                    if (!publicOnly || method.IsPublic() ){
                        methods.Add(method);
                    }
                }
            }
            return methods.ToArray();
        }
        internal Method GetConstructor(string descriptor)
        {
            return GetInstanceMethod("<init>", descriptor);
        }
        internal bool IsPrimitive()
        {
            foreach (string na in ClassNameHelper.primitiveTypes.Keys)
            {
                if (this.name== na)
                    return true;
            }
            return false;
        }
        internal Field[] GetFields(bool publicOnly)
        {
            if (publicOnly)
            {
                List<Field> publicFields = new List<Field>();
                foreach (Field field in fields)
                {
                    if (field.IsPublic())
                        publicFields.Add(field);
                }
                return publicFields.ToArray();
            }
            else return fields;
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

        internal string SourceFile()
        {
            return this.sourceFile;
        }

        internal Class ArrayClass()
        {
            string arrayClassName = new ClassNameHelper().getArrayClassName(this.name);
            return this.loader.LoadClass(arrayClassName);
        }
    }
}
