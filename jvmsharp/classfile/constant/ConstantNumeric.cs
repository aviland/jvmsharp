using System;

namespace jvmsharp.classfile
{
    class ConstantIntegerInfo : ConstantInfo
    {
        private int val;

        public override void readInfo(ref ClassReader reader)
        {
            uint bytes = reader.readUint32();
            val = (int)bytes;
        }

        public int Value()
        {
            return val;
        }
    }

    class ConstantLongInfo : ConstantInfo
    {
        private long val;

        public override void readInfo(ref ClassReader reader)
        {
            UInt64 bytes = reader.readUint64();
            val = (long)bytes;
        }

        public long Value()
        {
            return val;
        }
    }

    class ConstantFloatInfo : ConstantInfo
    {
        private float val;

        public override void readInfo(ref ClassReader reader)
        {
            uint bytes = reader.readUint32();
            val = Convert.ToSingle(bytes);
        }

        public float Value()
        {
            return val;
        }
    }

    class ConstantDoubleInfo : ConstantInfo
    {
        double val;

        public override void readInfo(ref ClassReader reader)
        {
            UInt64 bytes = reader.readUint64();
            val = bytes;
        }

        public double Value()
        {
            return val;
        }
    }
}
