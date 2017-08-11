using System;
using jvmsharp.rtda;

namespace jvmsharp.instructions.references
{
    unsafe class PUT_FIELD : Index16Instruction
    {
        public override void Execute(ref Frame frame)
        {
            var currentMethod = frame.method;
            var currentClass = currentMethod.Class();
            var cp = currentClass.constantPool;
            rtda.heap.ConstantFieldRef fieldRef = (rtda.heap.ConstantFieldRef)cp.GetConstant(Index);
            var field = fieldRef.ResolvedField();

            if (field.IsStatic())
                throw new Exception("java.lang.IncompatibleClassChangeError");
            if (field.IsFinal())
                if (currentClass != field.Class() || currentMethod.Name() != "<init>")
                    throw new Exception("java.lang.IllegalAccessError");

            string descriptor = field.Descriptor();
            uint slotId = field.slotId;
            OperandStack stack = frame.OperandStack();
            //         Console.WriteLine("ddddddddddddddddddddd" + descriptor[0]);
            switch (descriptor[0])
            {
                case 'Z':
                case 'B':
                case 'C':
                case 'S':
                case 'I':
                    int val = stack.PopInt();
                    rtda.heap.Object refs = stack.PopRef();
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
                    //       Console.WriteLine("slotid:" + slotId+"\tsize:"+ refsd.Fields().slots.Length);
                    refsd.Fields().SetDouble(slotId, vald);
                    break;
                case 'L':
                case '[':
                    var valr = stack.PopRef();
                    var refsr = stack.PopRef();
                    if (refsr == null)
                        throw new Exception("java.lang.NullPointerException");
                    refsr.Fields().SetRef(slotId, valr);
                    break;
            }
        }
    }
}