using System;
using System.IO;

namespace jvmsharp.classpath
{
    struct DirEntry : Entry
    {
        string absDir;

        public DirEntry(string path)
        {
            absDir = Path.GetFullPath(path);
        }

        //readclass 在debug目录下查找className，即当前exe程序所在目录
        public Tuple<byte[], Entry> readClass(string className)
        {
            if (File.Exists(className))
            {
                byte[] data = File.ReadAllBytes(className);
                return Tuple.Create<byte[], Entry>(data, this);
            }
            return null;
        }

        public string String()
        {
            return absDir;
        }
    }
}
