﻿using System;

namespace jvmsharp.rtda.heap
{
    class MemberRef : SymRef
    {
        protected string name;
        protected string descriptor;

        internal string Name()
        {
            return name;
        }

        public void copyMemberRefInfo(ref classfile.ConstantMemberrefInfo refInfo)
        {
            className = refInfo.ClassName();
            name = refInfo.NameAndDescriptor().Item1;
            descriptor = refInfo.NameAndDescriptor().Item2;
        }

        internal string Descriptor()
        {
            return descriptor;
        }

        internal void copy(classfile.ConstantMemberrefInfo refInfo)
        {
            className = refInfo.ClassName();
            Tuple<string, string> tuple = refInfo.NameAndDescriptor();
            name = tuple.Item1;
            descriptor = tuple.Item2;
        }
    }
}
