using jvmsharp.classpath;
using jvmsharp.rtda.heap;
using System;
using System.Reflection;

namespace jvmsharp
{
    class Program
    {
        static void Main(string[] args)
        {
            Cmd cmdStruct = new Cmd().parseCmd(args);
            if (cmdStruct.versionFlag)
                Console.WriteLine("version 0.0.1");
            else if (cmdStruct.helpFlag || cmdStruct.classes == "")
            {
                if (args.Length > 0)
                {
                    MethodBase method = new System.Diagnostics.StackTrace().GetFrame(0).GetMethod();
                    string className = method.ReflectedType.Namespace;
                    cmdStruct.printUsage(className);
                }
            }
            else startJVM(cmdStruct);
        }

        static void startJVM(Cmd cmd)
        {
            Classpath cp = new Classpath().Parse(cmd.XjreOption, cmd.cpOption);
            ClassLoader classLoader = new ClassLoader(ref cp, cmd.verboseClassFlag);
            string className = cmd.classes.Replace('.', '/');
            Class mainClass = classLoader.LoadClass(className);//big time
            Method mainMethod = mainClass.GetMainMethod();

         
            if (mainMethod != null)
                new interpreter().interpret(ref mainMethod,cmd.verboseInstFlag);//big time
            else Console.WriteLine("Main method not found in class " + cmd.classes);

        }

     

        //以下是注释代码
        #region ch05
        /*
         static void startJVM(Cmd cmd)
        {
            classpath.Classpath cp =new classpath.Classpath().Parse(cmd.XjreOption, cmd.cpOption);
            string className = cmd.classes.Replace('.', '/');
            classfile.ClassFile cf = loadClass(className, ref cp);
          classfile.MemberInfo mainMethod = getMainMethod(ref cf);
          if (mainMethod != null)
           new  interpreter().interpret(ref mainMethod);
          else Console.WriteLine("Main method not found in class " + cmd.classes);
        }

        static classfile.ClassFile loadClass(string className, ref classpath.Classpath cp)
        {
            byte[] classData = cp.ReadClass(className).Item1;
            classfile.ClassFile cf = classfile.ClassFile.Parse(classData);
            return cf;
        }
   
        static classfile.MemberInfo getMainMethod(ref classfile.ClassFile cf)
        {
            foreach (classfile.MemberInfo m in cf.Methods())
            {
                if (m.Name() == "main"&&m.Descriptor()=="([Ljava/lang/String;)V")
                {
                    return m;
                }
            }
            return null;
        }
         */
        #endregion
        #region  ch01
        /* 
         * static void startJVM(Cmd cmdStruct)
           {
               Console.Write("classpath:" + cmdStruct.cpOption + " class:" + cmdStruct.classes + " args:");
               Console.Write("[");
               for (int i = 0; i < cmdStruct.args.Length - 1; i++)
               {
                   Console.Write(cmdStruct.args[i] + " ");
               }
               Console.Write(cmdStruct.args[cmdStruct.args.Length - 1] + "]");
           }*/
        #endregion
        #region ch02
        /* static void startJVM(Cmd cmdStruct)
           {
               Console.WriteLine("startJVM");
               classpath.classpath cp = new classpath.classpath().Parse(cmdStruct.XjreOption, cmdStruct.cpOption);//初始化classpath
               Console.Write("classpath:" +Directory.GetCurrentDirectory() + " class:" + cmdStruct.classes);
               #region
               //输出其余参数
               Console.Write(" args:[");
               if (cmdStruct.args != null&&cmdStruct.args.Length>0)
               {
                   for (int i = 0; i < cmdStruct.args.Length - 1; i++)
                   {
                       Console.Write(cmdStruct.args[i] + " ");
                   }
                   Console.Write(cmdStruct.args[cmdStruct.args.Length - 1] );//args
               }
               Console.WriteLine("]");
               #endregion
               //class参数的.替换为/，方便在jar中查找
               string className = cmdStruct.classes.Replace('.', '/');
              var  classData= cp.ReadClass(className);
               if (classData!=null&&classData.Item1 != null)
               {
                   Console.WriteLine("class data:[");
                   foreach (byte b in classData.Item1)
                   {
                       Console.Write(b + " ");
                   }
                   Console.WriteLine("]");
               }
               else
               {
                   Console.WriteLine("Could not find or load main class " + cmdStruct.classes);
                   return;
               }
           }*/
        #endregion
        #region  ch03
        /*ch03
         * -Xjre "C:\Program Files\Java\jdk1.8.0_91\jre" java.lang.String
                 static void startJVM(Cmd cmdStruct)
        {
            classpath.classpath cp = new classpath.classpath().Parse(cmdStruct.XjreOption, cmdStruct.cpOption);
            string className = cmdStruct.classes.Replace('.', '/');
            classfile.ClassFile cf = loadClass(className, cp);
            Console.WriteLine(cmdStruct.classes);
            printClassInfo(cf);
        }

        static classfile.ClassFile loadClass(string className, classpath.classpath cp)
        {
            Tuple<byte[], classpath.Entry> tuple = cp.ReadClass(className);
            classfile.ClassFile cf = new classfile.ClassParser().Parse(tuple.Item1);
            return cf;
        }

        static void printClassInfo(classfile.ClassFile cf)
        {
            Console.WriteLine("version: " + cf.MajorVersion() + "." + cf.MinorVersion());//输出版本号信息
            Console.WriteLine("constants count: " + cf.ConstantPool().ConstantPools.Length);//输出常量池信息
            Console.WriteLine("access flags: 0x" + cf.AccessFlags());//输出类访问标志信息
            Console.WriteLine("this class: " + cf.ClassName());//输出类名信息
            Console.WriteLine("super class: " + cf.SuperClassName());//输出超类名信息
            Console.WriteLine("interfaces: [" + string.Join(" ", cf.InterfacesNames()) + "]");//输出接口表信息
            Console.WriteLine("fields count: " + cf.Fields().Length);//输出字段表信息
            foreach (classfile.MemberInfo mi in cf.Fields())
            {
                Console.WriteLine("\t" + mi.Name());
            }
            Console.WriteLine("methods count: " + cf.Methods().Length);//输出方法表信息
        //    foreach (classfile.MemberInfo mi in cf.Methods())
       //     {
       //         Console.WriteLine("\t" + mi.Name());
       //     }
            Console.WriteLine("attributes count: " + cf.Attributes().Length);//输出方法表信息
            foreach (classfile.AttributeInfoInterface mi in cf.Attributes())
            {
                Console.WriteLine("\t" + mi.GetType().Name);
            }
        }
         */
        #endregion
        #region ch04
        /*
         * 
                 static void startJVM(Cmd cmdStruct) {
            rtda.Frame frame = new rtda.Frame(100, 100);
            rtda.LocalVars localVars = frame.LocalVars();
            rtda.OperandStack operandStack = frame.OperandStack();
            testLocalvars(ref localVars);
            testOperandStack(ref operandStack );
        }

        static void testLocalvars(ref rtda.LocalVars vars)
        {
            vars.SetInt(0, 100);
            vars.SetInt(1, -100);
            vars.SetLong(2, 299887789);
            vars.SetLong(4, -299887789);
            vars.SetFloat(6,3.1415926f);
            vars.SetDouble(7, 2.71822432523);
            vars.SetRef(9, null);
            Console.WriteLine(vars.GetInt(0));
            Console.WriteLine(vars.GetInt(1));
            Console.WriteLine(vars.GetLong(2));
            Console.WriteLine(vars.GetLong(4));
            Console.WriteLine(vars.GetFloat(6));
            Console.WriteLine(vars.GetDouble(7));
            Console.WriteLine(vars.GetRef(9));
        }

        static void testOperandStack(ref rtda.OperandStack ops)
        {
            ops.PushInt(100);
            ops.PushInt(-100);
            ops.PushLong(299887789);
            ops.PushLong(-299887789);
            ops.PushFloat(3.1415926f);
            ops.PushDouble(2.71822432523);
            ops.PushRef(null);
            Console.WriteLine(ops.PopRef());
            Console.WriteLine(ops.PopDouble());
            Console.WriteLine(ops.PopFloat());
            Console.WriteLine(ops.PopLong());
            Console.WriteLine(ops.PopLong());
            Console.WriteLine(ops.PopInt());
            Console.WriteLine(ops.PopInt());
        }
         */
        #endregion
    }
}