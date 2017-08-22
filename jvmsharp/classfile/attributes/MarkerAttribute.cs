namespace jvmsharp.classfile
{
    class DeprecatedAttribute: MarkerAttribute { }
    class SyntheticAttribute : MarkerAttribute { }

    class MarkerAttribute : AttributeInfoInterface
    {
        public override void readInfo(ref ClassReader reader) { }
    }
}
