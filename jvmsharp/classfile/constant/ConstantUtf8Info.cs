using System.Text;

namespace jvmsharp.classfile
{
    class ConstantUtf8Info : ConstantInfo
    {
        public string str;

        public override void readInfo(ref ClassReader reader)
        {
            ushort length = reader.readUint16();
            byte[] bytes = reader.readBytes(length);
            str = Encoding.Default.GetString(bytes);
        }
    }
}
