using System;
using System.Collections.Generic;

namespace jvmsharp.rtda.heap
{
    class ClassLoader//类加载器
    {
        internal classpath.Classpath cp;
        internal bool verboseFlag;
          Dictionary<string, Class> classMap;//存储加载的所有的类

     internal    Dictionary<string, Class> ClassMap()
        {
            return classMap; 
        }
        const string jlObjectClassName = "java/lang/Object";
        const string jlClassClassName = "java/lang/Class";
        const string jlStringClassName = "java/lang/String";
        const string jlThreadClassName = "java/lang/Thread";
        const string jlCloneableClassName = "java/lang/Cloneable";
        const string ioSerializableClassName = "java/io/Serializable";

        public ClassLoader newClassLoader(ref classpath.Classpath cp, bool verboseFlag)
        {
            ClassLoader loader = new ClassLoader();
            loader.cp = cp;
            loader.verboseFlag = verboseFlag;
            loader.classMap = new Dictionary<string, Class>();
            loader.loadBasicClasses();
            loader.loadPrimitiveClasses();
            return loader;
        }

        void loadBasicClasses()
        {
            var jlClassClass = LoadClass("java/lang/Class");
            if (classMap.Count > 0)
                foreach (string s in classMap.Keys)
                {
                    if (classMap[s].jClass == null)
                    {
                        classMap[s].jClass = jlClassClass.NewObject();
                        classMap[s].jClass.extra = classMap[s];
                    }
                }
        }
        void loadPrimitiveClasses()
        {
            foreach (string key in ClassNameHelper.primitiveTypes.Keys)
                loadPrimitiveClass(key);
        }

        void loadPrimitiveClass(string className)
        {
            Class clas = new Class();
            clas.accessFlags = AccessFlags.ACC_PUBLIC;
            clas.name = className;
            clas.loader = this;
            clas.initStarted = true;
            clas.jClass = classMap["java/lang/Class"].NewObject();
            clas.jClass.extra = clas;
            this.classMap[className] = clas;
        }

        public Class getClass(string name)
        {
            var clas = classMap[name];

            if (clas != null)
                return clas;
            throw new Exception("class not loaded! " + name);
        }

        public Class LoadClass(string name)
        {
            if (name != null && classMap != null && classMap.ContainsKey(name))//若类已加载，则直接查找并返回
                return classMap[name];
            Class clas = null;
            if (name[0] == '[')
                clas = loadArrayClass(name);
            else clas = loadNonArrayClass(name);//否则加载该类
            if (classMap != null && classMap.ContainsKey("java/lang/Class"))
            {
                var jlClassClass = classMap["java/lang/Class"];
                clas.jClass = jlClassClass.NewObject();
                clas.jClass.extra = clas;
            }
            return clas;
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
                var v = cp.ReadClass(name);
                Tuple<byte[], classpath.Entry> tbe = v;//从解析的类中读取数据

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
            hackClass(ref clas);
            clas.loader = this;

            resolveSuperClass(ref clas);
                        resolveInterfaces(ref clas);
            classMap[clas.name] = clas;
            return clas;
        }

        private void hackClass(ref Class clas)
        {
            if (clas.name == "java/lang/ClassLoader")
            {
                Method loadLibrary = clas.GetStaticMethod("loadLibrary", "(Ljava/lang/Class;Ljava/lang/String;Z)V");
                loadLibrary.code = new byte[] { 0xb1 };// return void
            }
        }

        Class parseClass(ref byte[] data)
        {
            try
            {
                classfile.ClassFile cf = new classfile.ClassFile().Parse(ref data);//读取二进制data，初始化classfile
                Class c = new Class().newClass(ref cf);//将classfile转换为Class类型?
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
                    if (f.IsLongOrDouble())
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
        }

        void allocAndInitStaticVars(ref Class clas)
        {
            clas.staticVars = new Slots(clas.staticSlotCount);
            for (int i = 0; i < clas.fields.Length; i++)
            {
                if (clas.fields[i].IsStatic() && clas.fields[i].IsFinal())
                {
                    initStaticFinalVar(ref clas, ref clas.fields[i]);
                }
            }
        }

        unsafe void initStaticFinalVar(ref Class clas, ref Field f)
        {
            ConstantPool cp = clas.constantPool;
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
                        clas.staticVars.SetInt(slotId, val);
                        break;
                    case "J":
                        long val2 = (long)cp.GetConstant(cpIndex);
                        clas.staticVars.SetLong(slotId, val2);
                        break;
                    case "F":
                        float val3 = (float)cp.GetConstant(cpIndex);
                        clas.staticVars.SetFloat(slotId, val3);
                        break;
                    case "D":
                        double vald = (double)cp.GetConstant(cpIndex);
                        clas.staticVars.SetDouble(slotId, vald);
                        break;
                    case "Ljava/lang/String;":
                        string goStr = (string)cp.GetConstant(cpIndex);
                        Object jStr = StringPool.JString( clas.loader, goStr);
                        clas.staticVars.SetRef(slotId, jStr);
                        break;
                }
            }
        }
    }
}
