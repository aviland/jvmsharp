namespace jvmsharp.instructions
{
    interface Instruction
    {
        void FetchOperands(ref BytecodeReader reader);
        void Execute(ref rtda.Frame frame);
    }

      abstract class NoOperandsInstruction : Instruction
    {
         void Instruction.FetchOperands(ref BytecodeReader reader)
        {
            //nothing to do
        }
        public abstract void Execute(ref rtda.Frame frame);
    }

      abstract class BranchInstruction : Instruction
     {
        public int Offset;

         void Instruction.FetchOperands(ref BytecodeReader reader)
         {
            Offset = reader.ReadInt16();
         }

         public abstract void Execute(ref rtda.Frame frame);
     }

     abstract class  Index8Instruction : Instruction
    {
        public uint Index;

        void Instruction.FetchOperands(ref BytecodeReader reader)
        {
            Index = reader.ReadUint8();
        }

        public abstract void Execute(ref rtda.Frame frame);
    }

     abstract class Index16Instruction : Instruction
    {
        protected static ushort Index;

        void Instruction.FetchOperands(ref BytecodeReader reader)
        {
            Index16Instruction.Index = reader.ReadUint16();
        }

       public  abstract void Execute(ref rtda.Frame frame);
    }
}
