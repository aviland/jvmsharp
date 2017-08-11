using System;

namespace jvmsharp.classfile
{
    class ConstantIntegerInfo : ConstantInfo
    {
        private int val;

        public override void readInfo(ref ClassReader reader)
        {
            uint bytes = reader.readUint32();
            val = BitConverter.ToInt32(BitConverter.GetBytes(bytes), 0);
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
            val = BitConverter.ToInt64(BitConverter.GetBytes(bytes), 0);
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
            val = BitConverter.ToSingle(BitConverter.GetBytes(bytes), 0);
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
            val = BitConverter.ToDouble(BitConverter.GetBytes(bytes), 0);
        }

        public double Value()
        {
            return val;
        }
    }
}
