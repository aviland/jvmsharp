using System.Collections.Generic;
using System.Linq;
using System.IO;

namespace jvmsharp.classpath
{
    class CreateEntry
    {
        const char pathListSeparator = '\\';

        public  Entry newEntry(string path)
        {
            if (path.Contains(pathListSeparator))
            {
                //路径中包含\，则判定为CompositeEntry
                return new CompositeEntry(path);
            }
            if (path.EndsWith("*"))
            {
                //路径以*结尾，则判定为WildcardEntry
                return WildcardEntry(path);
            }
            if (path.EndsWith(".jar") || path.EndsWith(".JAR") || path.EndsWith(".zip") || path.EndsWith(".ZIP"))
            {
                //路径以.jar、.zip结尾，则判定为ZipEntry
                return new ZipEntry(path);
            }
            //否则是DirEntry
            return new DirEntry(path);
        }

        public static CompositeEntry WildcardEntry(string path)
        {
            string baseDir = path.Remove(path.Length - 1);
            List<Entry> entryList = new List<Entry>();
            DirectoryInfo di = new DirectoryInfo(baseDir);
            foreach (FileInfo fi in di.GetFiles())
            {
                if (fi.Extension.Equals(".jar") || fi.Extension.Equals(".JAR"))
                {
                    ZipEntry jarEntry = new ZipEntry(fi.FullName);
                    entryList.Add(jarEntry);
                }
            }
            CompositeEntry ce = new CompositeEntry(path);
            ce.entryArray = entryList.ToArray();
            return ce;
        }
    }
}
