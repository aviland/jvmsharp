using System;
using jvmsharp.rtda;

namespace jvmsharp.instructions.references
{
    class INVOKE_DYNAMIC : Instruction
    {
        public void Execute(ref Frame frame)
        {
            throw new NotImplementedException();
        }

        public void FetchOperands(ref BytecodeReader reader)
        {
            throw new NotImplementedException();
        }
    }
}
