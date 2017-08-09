using System;
using System.IO;

namespace jvmsharp.classpath
{
    struct Classpath
    {
        public Entry bootClasspath;//启动类路径
        public Entry extClasspath;//扩展类路径
        public Entry usrClasspath;//用户类路径

        public Classpath Parse(String jreOption, String cpOption)
        {
            Classpath cp = new Classpath();
            cp.parseBootAndExtClasspath(jreOption);
            cp.parseUserClasspath(cpOption);
            return cp;
        }

     public   static bool IsBootClassPath(Entry entry)
        {
            if (entry == null)
            {
                return true;
            }
            return entry.String().StartsWith(options.Options.AbsJreLib);
        }

        void parseBootAndExtClasspath(string jreOption)
        {
            //解析启动类路径
            string jreDir = getJreDir(jreOption);
            string jreLibPath = string.Join("\\", new string[] { jreDir, "lib", "*" });
            bootClasspath = CreateEntry.WildcardEntry(jreLibPath);
            //解析扩展类路径
            string jreExtPath = string.Join("\\", new string[] { jreDir, "lib", "ext", "*" });
            extClasspath = CreateEntry.WildcardEntry(jreExtPath);
        }

        //寻找jre文件夹路径
        string getJreDir(string jreOption)
        {
            if (jreOption != "" && exists(jreOption))
            {
                return jreOption;
            }
            if (exists("./jre"))
            {
                return "./jre";
            }
            string jh = Environment.GetEnvironmentVariable("JAVA_HOME");
            if (jh != null && jh != "")
            {
                return string.Join("\\", new string[] { jh, "jre" });
            }
            throw new Exception("Can not find jre folder");
        }

        bool exists(string path)
        {
            return Directory.Exists(path) || File.Exists(path) ? true : false;
        }

        void parseUserClasspath(string cpOption)
        {
            if (cpOption == "" || cpOption == null)
                cpOption = ".";
            usrClasspath = new CreateEntry().newEntry(cpOption);
        }

        public Tuple<byte[], Entry> ReadClass(string className)
        {
            className += ".class";
            //     Console.WriteLine(className);
            return bootClasspath.readClass(className) ?? extClasspath.readClass(className) ?? usrClasspath.readClass(className);
        }

        public string String()
        {
            return usrClasspath.String();
        }
    }
}
