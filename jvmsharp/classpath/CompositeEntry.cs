using System;

namespace jvmsharp.classpath
{
    struct CompositeEntry : Entry
    {
        public  Entry[] entryArray;
        /*
         * 构造函数将路径以'\'拆解为数组
         * 比如路径为C:\Program Files\Java\jdk1.8.0_66\jre\lib
         * 拆解得到的数组为[C:,Program Files,Java,jdk1.8.0_66,jre,lib]
         */
        public CompositeEntry(string pathList)
        {
            string[] paths = pathList.Split('\\');
            entryArray = new Entry[paths.Length];
            for (int i = 0; i < paths.Length - 1; i++)
            {
                Entry e = new CreateEntry().newEntry(paths[i]);
                entryArray[i] = e;
            }
        }

        //读取entryArray数组中的所有Entry，若byteEntry.bytes不为空则返回该byteEntry否则返回null
        public Tuple<byte[],Entry> readClass(string className)
        {
            foreach (Entry e in entryArray)
            {
                Tuple<byte[], Entry> be = e.readClass(className);
                if (be != null && be.Item1 != null)
                    return be;
            }
            return null;
        }

        //由于每个entryArray的entry包含string，该string保存了路径信息，需要返回其遍历组合值
        public string String()
        {
            string[] strs = new string[entryArray.Length];
            for (int i = 0; i < strs.Length; i++)
            {
                strs[i] = entryArray[i].String();
            }
            return string.Join("\\", strs);//pathListSeparator
        }
    }
}
