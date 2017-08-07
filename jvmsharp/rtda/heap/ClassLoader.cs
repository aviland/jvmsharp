using System;
using System.Collections.Generic;

namespace jvmsharp.rtda.heap
{
    class ClassLoader//类加载器
    {
        classpath.Classpath cp;
        Dictionary<string, Class> classMap;//存储加载的所有的类
        bool verboseFlag;

        const string jlObjectClassName = "java/lang/Object";
        const string jlClassClassName = "java/lang/Class";
        const string jlStringClassName = "java/lang/String";
        const string jlThreadClassName = "java/lang/Thread";
        const string jlCloneableClassName = "java/lang/Cloneable";
        const string ioSerializableClassName = "java/io/Serializable";

        //public static ClassLoader bootLoader;
        public static Class _jlObjectClass;
        public static Class _jlClassClass;
        public static Class _jlStringClass;
        public static Class _jlThreadClass;
        public static Class _jlCloneableClass;
        public static Class _ioSerializableClass;

        public ClassLoader(ref classpath.Classpath cp, bool verboseFlag)
        {
            this.cp = cp;
            classMap = new Dictionary<string, Class>();
            this.verboseFlag = verboseFlag;
        }


       /* public void InitBootLoader(classpath.Classpath cp)
        {
            bootLoader = new ClassLoader(ref cp, verboseFlag);
            bootLoader._init();
        }*/

        public void _init()
        {
            _jlObjectClass = LoadClass(jlObjectClassName);
            _jlClassClass = LoadClass(jlClassClassName);
            foreach (Class clas in classMap.Values)
            {
                if (clas.jClass == null)
                {
                    clas.jClass = _jlClassClass.NewObject();
                }
            }
            _jlCloneableClass = LoadClass(jlCloneableClassName); ;
            _ioSerializableClass = LoadClass(ioSerializableClassName);
            _jlThreadClass = LoadClass(jlThreadClassName);
            _jlStringClass = LoadClass(jlStringClassName);
            this.loadPrimitiveClasses();
            this.loadPrimitiveArrayClasses();
        }
        void loadPrimitiveArrayClasses()
        {
            foreach (PrimitiveType pt in PrimitiveTypes.primitiveTypes)
                loadArrayClass(pt.ArrayClassName);
        }

        void loadPrimitiveClasses()
        {
            foreach (PrimitiveType pt in PrimitiveTypes.primitiveTypes)
                loadPrimitiveClass(pt.Name);
        }

        void loadPrimitiveClass(string className)
        {

            Class clas = new Class(className);
            //class.classLoader = self
            //     clas.jClass = _jlClassClass.NewObj();

            clas.MarkFullyInitialized();
            classMap[className] = clas;
        }


        public Class LoadClass(string name)
        {
            if (name != null && classMap.ContainsKey(name))//若类已加载，则直接查找并返回
                return classMap[name];
            if (name[0] == '[')
            {
                return loadArrayClass(name);
            }
            else return loadNonArrayClass(name);//否则加载该类
        }

        Class loadArrayClass(string name)
        {
            Class clas = new Class();
            clas.accessFlags = AccessFlags.ACC_PUBLIC;
            clas.name = name;
            clas.loader = this;
            clas.initStarted = true;
            clas.superClass = LoadClass("java/lang/Object");
            clas.interfaces = new Class[] { LoadClass("java/lang/Cloneable"), LoadClass("java/io/Serializable") };
            classMap[name] = clas;
            return clas;
        }

        public Class loadNonArrayClass(string name)
        {
            Tuple<byte[], classpath.Entry> tbe = readClass(name);
            byte[] data = tbe.Item1;//取得class文件的二进制数据

            Class clas = defineClass(ref data);//定义类

            link(ref clas);//链接类
            if (verboseFlag)//参数确定是否打印类加载信息
                Console.WriteLine("[Loaded  " + name + " from " + tbe.Item2.String() + " ]");
            return clas;
        }

        Tuple<byte[], classpath.Entry> readClass(string name)
        {
            try
            {
                Tuple<byte[], classpath.Entry> tbe = cp.ReadClass(name);//从解析的类中读取数据
                return tbe;
            }
            catch (Exception exp)
            {
                throw new Exception("java.lang.ClassNotFoundException:" + name + "\n" + exp);
            }
        }

        Class defineClass(ref byte[] data)
        {
            Class clas = parseClass(ref data);
            clas.loader = this;

            resolveSuperClass(ref clas);

            resolveInterfaces(ref clas);
            classMap[clas.name] = clas;
            return clas;
        }

        Class parseClass(ref byte[] data)
        {
            try
            {
                classfile.ClassFile cf = new classfile.ClassFile().Parse(ref data);//读取二进制data，初始化classfile
                Class c = new Class().newClass(cf);//将classfile转换为Class类型?
                return c;
            }
            catch (Exception e)
            {
                throw new Exception("java.lang.ClassFormatError" + e);
            }
        }

        void resolveSuperClass(ref Class clas)
        {
            if (clas.name != "java/lang/Object")
            {
                clas.superClass = clas.loader.LoadClass(clas.superClassName);
            }
        }

        void resolveInterfaces(ref Class clas)
        {
            int interfaceCount = clas.interfaceNames.Length;
            if (interfaceCount > 0)
            {
                clas.interfaces = new Class[interfaceCount];
                for (int i = 0; i < interfaceCount; i++)
                {
                    clas.interfaces[i] = clas.loader.LoadClass(clas.interfaceNames[i]);
                }
            }
        }

        void link(ref Class clas)
        {
            verify(ref clas);
            prepare(ref clas);
        }

        void verify(ref Class clas)
        {

        }

        void prepare(ref Class clas)
        {
            calcInstanceFieldSlotIds(ref clas);
            calcStaticFieldSlotIds(ref clas);
            allocAndInitStaticVars(ref clas);
        }

        void calcInstanceFieldSlotIds(ref Class clas)
        {
            uint slotId = 0;
            if (clas.superClass != null)
                slotId = clas.superClass.instanceSlotCount;
            foreach (Field f in clas.fields)
            {
                if (!f.IsStatic())
                {
                    f.slotId = slotId;
                    slotId++;
                }
            }
            clas.instanceSlotCount = slotId;
        }

        void calcStaticFieldSlotIds(ref Class clas)
        {
            uint slotId = 0;
            foreach (Field f in clas.fields)
            {
                if (f.IsStatic())
                {
                    f.slotId = slotId;
                    slotId++;
                    if (f.IsLongOrDouble())
                        slotId++;
                }
            }
            clas.staticSlotCount = slotId;
            //  Console.WriteLine("yyyyyyyyyyyyyyyyyyyyyyyy" + clas.staticSlotCount);
        }

        void allocAndInitStaticVars(ref Class clas)
        {
            //   Console.WriteLine("----------------------create" + clas.staticSlotCount);
            clas.staticVars = new Slots(clas.staticSlotCount);
            for (int i = 0; i < clas.fields.Length; i++)
            {
                if (clas.fields[i].IsStatic() && clas.fields[i].IsFinal())
                {
                    initStaticFinalVar(ref clas, ref clas.fields[i]);
                }
            }
        }

        void initStaticFinalVar(ref Class clas, ref Field f)
        {
            Slots vars = clas.staticVars;
            ConstantPool cp = clas.ConstantPool();
            uint cpIndex = f.ConstValueIndex();
            uint slotId = f.slotId;
            if (cpIndex > 0)
            {
                switch (f.Descriptor())
                {
                    case "Z":
                    case "B":
                    case "C":
                    case "S":
                    case "I":
                        int val = (int)cp.GetConstant(cpIndex);
                        vars.SetInt(slotId, val);
                        break;
                    case "J":
                        long val2 = (long)cp.GetConstant(cpIndex);
                        vars.SetLong(slotId, val2);
                        break;
                    case "F":
                        float val3 = (float)cp.GetConstant(cpIndex);
                        vars.SetFloat(slotId, val3);
                        break;
                    case "D":
                        double vald = (double)cp.GetConstant(cpIndex);
                        vars.SetDouble(slotId, vald);
                        break;
                    case "Ljava/lang/String;":
                        throw new Exception("todo");
                }
            }
        }
    }
}
