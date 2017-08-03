using System;
using jvmsharp.rtda;

namespace jvmsharp.instructions.references
{
    class PUT_FIELD : Index16Instruction
    {
  public override void Execute(ref Frame frame)
        {
            var currentMethod = frame.Method();
            var currentClass = currentMethod.Class();
            var cp = currentClass.ConstantPool();
            var fieldRef = (rtda.heap.ConstantFieldRef)cp.GetConstant(Index);
            var field = fieldRef.ResolvedField();

            if (field.IsStatic())
                throw new Exception("java.lang.IncompatibleClassChangeError");
            if (field.IsFinal())
                if (currentClass != field.Class() || currentMethod.Name() != "<init>")
                    throw new Exception("java.lang.IllegalAccessError");

            var descriptor = field.Descriptor();
            var slotId = field.slotId;
            var stack = frame.OperandStack();
            switch (descriptor[0])
            {
                case 'Z':
                case 'B':
                case 'C':
                case 'S':
                case 'I':
                    var val = stack.PopInt();
                    var refs = stack.PopRef();
                    if (refs == null)
                        throw new Exception("java.lang.NullPointerException");
                   refs.Fields().SetInt(slotId, val);
                    break;
                case 'F':
                    float valf = stack.PopFloat();
                    var refsf = stack.PopRef();
                    if (refsf == null)
                        throw new Exception("java.lang.NullPointerException");
                    refsf.Fields().SetFloat(slotId, valf);
                    break;
                case 'J':
                    long vall = stack.PopLong();
                    var refsl = stack.PopRef();
                    if (refsl == null)
                        throw new Exception("java.lang.NullPointerException");
                    refsl.Fields().SetLong(slotId, vall);
                    break;
                case 'D':
                    double vald = stack.PopDouble();
                    var refsd = stack.PopRef();
                    if (refsd == null)
                        throw new Exception("java.lang.NullPointerException");
                    refsd.Fields().SetDouble(slotId, vald);
                    break;
                case 'L':
                case '[':
                    var valr = stack.PopRef();
                    var refsr = stack.PopRef();
                    if (refsr == null)
                        throw new Exception("java.lang.NullPointerException");
                    refsr.Fields().SetRef(slotId, ref valr);
                    break;
            }
        }
    }
}