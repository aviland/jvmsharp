namespace jvmsharp.classfile
{
    class DeprecatedAttribute: MarkerAttribute { }
    class SyntheticAttribute : MarkerAttribute { }

    class MarkerAttribute : AttributeInfoInterface
    {
        public void readInfo(ref ClassReader reader) { }
    }
}
