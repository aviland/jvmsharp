using System;

namespace jvmsharp.instructions.extended
{
    struct WIDE : Instruction
    {
        instructions.Instruction modifiedInstruction;

        public void FetchOperands(ref BytecodeReader reader)
        {
            uint opcode = reader.ReadUint8();
            switch (opcode)
            {
                case 0x15:
                    loads.ILOAD iload = new loads.ILOAD();
                    iload.Index = reader.ReadUint16();
                    modifiedInstruction = iload;
                    break;
                case 0x16:
                    loads.LLOAD lload = new loads.LLOAD();
                    lload.Index = reader.ReadUint16();
                    modifiedInstruction = lload;
                    break;
                case 0x17:
                    loads.FLOAD fload = new loads.FLOAD();
                    fload.Index = reader.ReadUint16();
                    modifiedInstruction = fload;
                    break;
                case 0x18:
                    loads.DLOAD dload = new loads.DLOAD();
                    dload.Index = reader.ReadUint16();
                    modifiedInstruction = dload;
                    break;
                case 0x19:
                    loads.ALOAD aload = new loads.ALOAD();
                    aload.Index = reader.ReadUint16();
                    modifiedInstruction = aload;
                    break;
                case 0x36:
                    stores.ISTORE istore = new stores.ISTORE();
                    istore.Index = reader.ReadUint16();
                    modifiedInstruction = istore;
                    break;
                case 0x37:
                    stores.LSTORE lstore = new stores.LSTORE();
                    lstore.Index = reader.ReadUint16();
                    modifiedInstruction = lstore;
                    break;
                case 0x38:
                    stores.FSTORE fstore = new stores.FSTORE();
                    fstore.Index = reader.ReadUint16();
                    modifiedInstruction = fstore;
                    break;
                case 0x39:
                    stores.DSTORE dstore = new stores.DSTORE();
                    dstore.Index = reader.ReadUint16();
                    modifiedInstruction = dstore;
                    break;
                case 0x3a:
                    stores.ASTORE astore = new stores.ASTORE();
                    astore.Index = reader.ReadUint16();
                    modifiedInstruction = astore;
                    break;
                case 0x84:
                    math.IINC iinc = new math.IINC();
                    iinc.Index = reader.ReadUint16();
                    iinc.Const = reader.ReadUint16();
                    modifiedInstruction = iinc;
                    break;
                case 0xa9:
                    throw new Exception("Unsupported opcode:0xa9!");
            }
        }

        public void Execute(ref rtda.Frame frame)
        {
            modifiedInstruction.Execute(ref frame);
        }
    }
}
