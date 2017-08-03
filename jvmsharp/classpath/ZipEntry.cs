using System;
using System.IO;
using System.IO.Compression;
namespace jvmsharp.classpath
{
    struct ZipEntry : Entry
    {
        public string absPath;

        public ZipEntry(string path)
        {
            //    Console.WriteLine(Path.GetFullPath(path));
            absPath = Path.GetFullPath(path);
        }

        public Tuple<byte[],Entry> readClass(string className)
        {
            ZipArchive zipArchive = ZipFile.Open(absPath, ZipArchiveMode.Read);
      //      if (absPath.Contains("rt"))
   //             Console.WriteLine(absPath);
            foreach (ZipArchiveEntry entry in zipArchive.Entries)
            {
            //    Console.WriteLine(entry.FullName);
                if (entry.FullName.Contains(className))
                {
                    byte[] data = new byte[entry.Length];
                    BinaryReader br = new BinaryReader(entry.Open());
                    int i;
                    long l =0;
                    while (l< entry.Length)
                    {
                        data[l] =br.ReadByte();
                        l++;
                    }
                    br.Close();
                    return Tuple.Create<byte[], Entry>(data, this);
                }
            }
            return Tuple.Create<byte[],Entry>(null, null);   
        }

        public string String()
        {
            return absPath;
        }
    }
}
