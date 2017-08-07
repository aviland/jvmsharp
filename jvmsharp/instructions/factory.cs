using System;
using jvmsharp.instructions.constants;
using jvmsharp.instructions.extended;
using jvmsharp.instructions.references;
using jvmsharp.instructions.control;
using jvmsharp.instructions.loads;
using jvmsharp.instructions.conversions;
using jvmsharp.instructions.stores;
using jvmsharp.instructions.stack;

#region  jvmsharp.instructions.math
using iadd = jvmsharp.instructions.math.IADD;
using ladd = jvmsharp.instructions.math.LADD;
using fadd = jvmsharp.instructions.math.FADD;
using dadd = jvmsharp.instructions.math.DADD;
using isub = jvmsharp.instructions.math.ISUB;
using lsub = jvmsharp.instructions.math.LSUB;
using fsub = jvmsharp.instructions.math.FSUB;
using dsub = jvmsharp.instructions.math.DSUB;
using imul = jvmsharp.instructions.math.IMUL;
using lmul = jvmsharp.instructions.math.LMUL;
using fmul = jvmsharp.instructions.math.FMUL;
using dmul = jvmsharp.instructions.math.DMUL;
using idiv = jvmsharp.instructions.math.IDIV;
using ldiv = jvmsharp.instructions.math.LDIV;
using fdiv = jvmsharp.instructions.math.FDIV;
using ddiv = jvmsharp.instructions.math.DDIV;
using irem = jvmsharp.instructions.math.IREM;
using lrem = jvmsharp.instructions.math.LREM;
using frem = jvmsharp.instructions.math.FREM;
using drem = jvmsharp.instructions.math.DREM;
using ineg = jvmsharp.instructions.math.INEG;
using lneg = jvmsharp.instructions.math.LNEG;
using fneg = jvmsharp.instructions.math.FNEG;
using dneg = jvmsharp.instructions.math.DNEG;
using ishl = jvmsharp.instructions.math.ISHL;
using lshl = jvmsharp.instructions.math.LSHL;
using ishr = jvmsharp.instructions.math.ISHR;
using lshr = jvmsharp.instructions.math.LSHR;
using iushr = jvmsharp.instructions.math.IUSHR;
using lushr = jvmsharp.instructions.math.LUSHR;
using iand = jvmsharp.instructions.math.IAND;
using land = jvmsharp.instructions.math.LAND;
using ior = jvmsharp.instructions.math.IOR;
using lor = jvmsharp.instructions.math.LOR;
using ixor = jvmsharp.instructions.math.IXOR;
using lxor = jvmsharp.instructions.math.LXOR;
using iinc = jvmsharp.instructions.math.IINC;
#endregion
#region jvmsharp.instructions.comparisons
using lcmp = jvmsharp.instructions.comparisons.LCMP;
using fcmpl = jvmsharp.instructions.comparisons.FCMPL;
using fcmpg = jvmsharp.instructions.comparisons.FCMPG;
using dcmpl = jvmsharp.instructions.comparisons.DCMPL;
using dcmpg = jvmsharp.instructions.comparisons.DCMPG;
using ifeq = jvmsharp.instructions.comparisons.IFEQ;
using ifne = jvmsharp.instructions.comparisons.IFNE;
using iflt = jvmsharp.instructions.comparisons.IFLT;
using ifge = jvmsharp.instructions.comparisons.IFGE;
using ifgt = jvmsharp.instructions.comparisons.IFGT;
using ifle = jvmsharp.instructions.comparisons.IFLE;
using if_icmpeq = jvmsharp.instructions.comparisons.IF_ICMPEQ;
using if_icmpne = jvmsharp.instructions.comparisons.IF_ICMPNE;
using if_icmplt = jvmsharp.instructions.comparisons.IF_ICMPLT;
using if_icmpge = jvmsharp.instructions.comparisons.IF_ICMPGE;
using if_icmpgt = jvmsharp.instructions.comparisons.IF_ICMPGT;
using if_icmple = jvmsharp.instructions.comparisons.IF_ICMPLE;
using if_acmpeq = jvmsharp.instructions.comparisons.IF_ACMPEQ;
using if_acmpne = jvmsharp.instructions.comparisons.IF_ACMPNE;
#endregion

namespace jvmsharp.instructions
{
    class factory
    {
        public Instruction NewInstruction(byte opcode)
        {
           // Console.WriteLine("0x" + Convert.ToString(opcode, 16));
            switch (Convert.ToInt32(opcode))
            {
                #region  constants
                case 0x00: return new NOP();
                case 0x01: return new ACONST_NULL();
                case 0x02: return new ICONST_M1();
                case 0x03: return new ICONST_0();
                case 0x04: return new ICONST_1();
                case 0x05: return new ICONST_2();
                case 0x06: return new ICONST_3();
                case 0x07: return new ICONST_4();
                case 0x08: return new ICONST_5();
                case 0x09: return new LCONST_0();
                case 0x0a: return new LCONST_1();
                case 0x0b: return new FCONST_0();
                case 0x0c: return new FCONST_1();
                case 0x0d: return new FCONST_2();
                case 0x0e: return new DCONST_0();
                case 0x0f: return new DCONST_1();
                case 0x10: return new BIPUSH();
                case 0x11: return new SIPUSH();
                case 0x12: return new LDC();
                case 0x13: return new LDC_W();
                case 0x14: return new LDC2_W();
                #endregion
                #region loads
                case 0x15: return new ILOAD();
                case 0x16: return new LLOAD();
                case 0x17: return new FLOAD();
                case 0x18: return new DLOAD();
                case 0x19: return new ALOAD();
                case 0x1a: return new ILOAD_0();
                case 0x1b: return new ILOAD_1();
                case 0x1c: return new ILOAD_2();
                case 0x1d: return new ILOAD_3();
                case 0x1e: return new LLOAD_0();
                case 0x1f: return new LLOAD_1();
                case 0x20: return new LLOAD_2();
                case 0x21: return new LLOAD_3();
                case 0x22: return new FLOAD_0();
                case 0x23: return new FLOAD_1();
                case 0x24: return new FLOAD_2();
                case 0x25: return new FLOAD_3();
                case 0x26: return new DLOAD_0();
                case 0x27: return new DLOAD_1();
                case 0x28: return new DLOAD_2();
                case 0x29: return new DLOAD_3();
                case 0x2a: return new ALOAD_0();
                case 0x2b: return new ALOAD_1();
                case 0x2c: return new ALOAD_2();
                case 0x2d: return new ALOAD_3();
                case 0x2e: return new IALOAD();
                case 0x2f: return new LALOAD();
                case 0x30: return new FALOAD();
                case 0x31: return new DALOAD();
                case 0x32: return new AALOAD();
                case 0x33: return new BALOAD();
                case 0x34: return new CALOAD();
                case 0x35: return new SALOAD();
                #endregion
                #region stores
                case 0x36: return new ISTORE();
                case 0x37: return new LSTORE();
                case 0x38: return new FSTORE();
                case 0x39: return new DSTORE();
                case 0x3a: return new ASTORE();
                case 0x3b: return new ISTORE_0();
                case 0x3c: return new ISTORE_1();
                case 0x3d: return new ISTORE_2();
                case 0x3e: return new ISTORE_3();
                case 0x3f: return new LSTORE_0();
                case 0x40: return new LSTORE_1();
                case 0x41: return new LSTORE_2();
                case 0x42: return new LSTORE_3();
                case 0x43: return new FSTORE_0();
                case 0x44: return new FSTORE_1();
                case 0x45: return new FSTORE_2();
                case 0x46: return new FSTORE_3();
                case 0x47: return new DSTORE_0();
                case 0x48: return new DSTORE_1();
                case 0x49: return new DSTORE_2();
                case 0x4a: return new DSTORE_3();
                case 0x4b: return new ASTORE_0();
                case 0x4c: return new ASTORE_1();
                case 0x4d: return new ASTORE_2();
                case 0x4e: return new ASTORE_3();
                case 0x4f: return new IASTORE();
                case 0x50: return new LASTORE();
                case 0x51: return new FASTORE();
                case 0x52: return new DASTORE();
                case 0x53: return new AASTORE();
                case 0x54: return new BASTORE();
                case 0x55: return new CASTORE();
                case 0x56: return new SASTORE();
                #endregion
                #region stack
                case 0x57: return new POP();
                case 0x58: return new POP2();
                case 0x59: return new DUP();
                case 0x5a: return new DUP_X1();
                case 0x5b: return new DUP_X2();
                case 0x5c: return new DUP2();
                case 0x5d: return new DUP2_X1();
                case 0x5e: return new DUP2_X2();
                case 0x5f: return new SWAP();
                #endregion
                #region math
                case 0x60: return new iadd();
                case 0x61: return new ladd();
                case 0x62: return new fadd();
                case 0x63: return new dadd();
                case 0x64: return new isub();
                case 0x65: return new lsub();
                case 0x66: return new fsub();
                case 0x67: return new dsub();
                case 0x68: return new imul();
                case 0x69: return new lmul();
                case 0x6a: return new fmul();
                case 0x6b: return new dmul();
                case 0x6c: return new idiv();
                case 0x6d: return new ldiv();
                case 0x6e: return new fdiv();
                case 0x6f: return new ddiv();
                case 0x70: return new irem();
                case 0x71: return new lrem();
                case 0x72: return new frem();
                case 0x73: return new drem();
                case 0x74: return new ineg();
                case 0x75: return new lneg();
                case 0x76: return new fneg();
                case 0x77: return new dneg();
                case 0x78: return new ishl();
                case 0x79: return new lshl();
                case 0x7a: return new ishr();
                case 0x7b: return new lshr();
                case 0x7c: return new iushr();
                case 0x7d: return new lushr();
                case 0x7e: return new iand();
                case 0x7f: return new land();
                case 0x80: return new ior();
                case 0x81: return new lor();
                case 0x82: return new ixor();
                case 0x83: return new lxor();
                case 0x84: return new iinc();
                #endregion
                #region conversions
                case 0x85: return new I2L();
                case 0x86: return new I2F();
                case 0x87: return new I2D();
                case 0x88: return new L2I();
                case 0x89: return new L2F();
                case 0x8a: return new L2D();
                case 0x8b: return new F2I();
                case 0x8c: return new F2L();
                case 0x8d: return new F2D();
                case 0x8e: return new D2I();
                case 0x8f: return new D2L();
                case 0x90: return new D2F();
                case 0x91: return new I2B();
                case 0x92: return new I2C();
                case 0x93: return new I2S();
                #endregion
                #region conparisons
                case 0x94: return new lcmp();
                case 0x95: return new fcmpl();
                case 0x96: return new fcmpg();
                case 0x97: return new dcmpl();
                case 0x98: return new dcmpg();
                case 0x99: return new ifeq();
                case 0x9a: return new ifne();
                case 0x9b: return new iflt();
                case 0x9c: return new ifge();
                case 0x9d: return new ifgt();
                case 0x9e: return new ifle();
                case 0x9f: return new if_icmpeq();
                case 0xa0: return new if_icmpne();
                case 0xa1: return new if_icmplt();
                case 0xa2: return new if_icmpge();
                case 0xa3: return new if_icmpgt();
                case 0xa4: return new if_icmple();
                case 0xa5: return new if_acmpeq();
                case 0xa6: return new if_acmpne();
                #endregion
                #region control
                case 0xa7: return new GOTO();
                /*case 0xa8:
                case 0xa9:*/
                case 0xaa: return new TABLE_SWITCH();
                case 0xab: return new LOOKUP_SWITCH();
                case 0xac: return new IRETURN();
                case 0xad: return new LRETURN();
                case 0xae: return new FRETURN();
                case 0xaf: return new DRETURN();
                case 0xb0: return new ARETURN();
                case 0xb1: return new RETURN();
                #endregion
                #region references
                case 0xb2: return new GET_STATIC();
                case 0xb3: return new PUT_STATIC();
                case 0xb4: return new GET_FIELD();
                case 0xb5: return new PUT_FIELD();
                case 0xb6: return new INVOKE_VIRTUAL();
                case 0xb7: return new INVOKE_SPECIAL();
                case 0xb8: return new INVOKE_STATIC();
                case 0xb9: return new INVOKE_INTERFACE();
                case 0xba: return new INVOKE_DYNAMIC();
                case 0xbb: return new NEW();
                case 0xbc: return new NEW_ARRAY();
                case 0xbd: return new ANEW_ARRAY();
                case 0xbe: return new ARRAY_LENGTH();
                /*  case 0xbf:*/
                case 0xc0: return new CHECK_CAST();
                case 0xc1: return new INSTANCE_OF();
                /*   case 0xc2: 
                 case 0xc3: */
                #endregion
                #region extended
                case 0xc4: return new WIDE();
                case 0xc5: return new MULTI_ANEW_ARRAY();
                case 0xc6: return new IFNULL();
                case 0xc7: return new IFNONNULL();
                case 0xc8: return new GOTO_W();
                case 0xc9:
                #endregion
                default: throw new Exception("Unsupported opcode: 0x" + Convert.ToString(opcode, 16) + "!");
            }
        }
    }
}
