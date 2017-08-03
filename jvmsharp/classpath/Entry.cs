using System;

namespace jvmsharp.classpath
{
    public interface Entry
    {
        Tuple<byte[],Entry> readClass(string className);
        string String();
    }
}
