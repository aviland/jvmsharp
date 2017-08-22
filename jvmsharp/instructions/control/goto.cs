using System;

namespace jvmsharp.instructions.control
{
    class  GOTO:BranchInstruction
    {
        public override void Execute(ref rtda.Frame frame)
        {
            BranchLogic.Branch(ref frame, this.Offset);
        }
    }
}
