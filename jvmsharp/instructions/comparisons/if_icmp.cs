using System;

namespace jvmsharp.instructions.comparisons
{
    class IF_ICMPEQ : BranchInstruction
    {
        public override void Execute(ref rtda.Frame frame)
        {
            Tuple<int, int> t = ICMPPOP.icmpPop(ref frame);
            if (t.Item1 == t.Item2)
                BranchLogic.Branch(ref frame, Offset);
        }
    }

    class IF_ICMPNE : BranchInstruction
    {
        public override void Execute(ref rtda.Frame frame)
        {
            Tuple<int, int> t = ICMPPOP.icmpPop(ref frame);
            if (t.Item1!= t.Item2)
                BranchLogic.Branch(ref frame, Offset);
        }
    }

    class IF_ICMPLT : BranchInstruction
    {
        public override void Execute(ref rtda.Frame frame)
        {
            Tuple<int, int> t = ICMPPOP.icmpPop(ref frame);
            if (t.Item1< t.Item2)
                BranchLogic.Branch(ref frame, Offset);
        }
    }

    class IF_ICMPLE : BranchInstruction
    {
        public override void Execute(ref rtda.Frame frame)
        {
            Tuple<int, int> t = ICMPPOP.icmpPop(ref frame);
            if (t.Item1<= t.Item2)
                BranchLogic.Branch(ref frame, Offset);
        }
    }

    class IF_ICMPGT : BranchInstruction
    {
        public override void Execute(ref rtda.Frame frame)
        {
            Tuple<int, int> t = ICMPPOP.icmpPop(ref frame);
            if (t.Item1 > t.Item2)
                BranchLogic.Branch(ref frame, Offset);
        }
    }

    class IF_ICMPGE : BranchInstruction
    {
        public override void Execute(ref rtda.Frame frame)
        {
            Tuple<int, int> t = ICMPPOP.icmpPop(ref frame);
            if (t.Item1 >= t.Item2)
            {
                BranchLogic.Branch(ref frame, Offset);
            }
        }
    }

    class ICMPPOP
    {
    internal    static Tuple<int, int> icmpPop(ref rtda.Frame frame)
        {
            var stack = frame.OperandStack();
            var val2 = stack.PopInt();
            var val1 = stack.PopInt();
            return Tuple.Create(val1, val2);
        }
    }
}
