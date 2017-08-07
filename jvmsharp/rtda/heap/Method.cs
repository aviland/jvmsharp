namespace jvmsharp.rtda.heap
{
    class Method : ClassMember
    {

        public const string mainMethodName = "main";
        public const string mainMethodDesc = "([Ljava/lang/String;)V";
        public const string clinitMethodName = "<clinit>";
        public const string clinitMethodDesc = "()V";
        public const string constructorName = "<init>";

        private uint maxStack;
        private uint maxLocals;
        public byte[] code;
        private uint argSlotCount;//参数计数
    //   MethodDescriptor md;

        public uint ArgSlotCount()
        {
            return this.argSlotCount;
        }

        public uint MaxStack()
        {
            return maxStack;
        }

        public uint MaxLocals()
        {
            return maxLocals;
        }

        public Method[] newMethods(ref Class clas, classfile.MemberInfo[] cfMethods)
        {
            Method[] methods = new Method[cfMethods.Length];
            for (int i = 0; i < cfMethods.Length; i++)
            {
                methods[i] = new Method();
                methods[i].clas = clas;
                methods[i].copyMemberInfo(ref cfMethods[i]);
                methods[i].copyAttributes(ref cfMethods[i]);
                methods[i].calcArgSlotCount();//当前方法参数计数计算
            }
            return methods;
        }

        void calcArgSlotCount()
        {
            MethodDescriptor parsedDescriptor = new MethodDescriptorParser().parseMethodDescriptor(descriptor);
            foreach (string paramType in parsedDescriptor.parameterTypes)
            {
                argSlotCount++;//一般参数占1个
                if (paramType=="J"||paramType=="D")
                    argSlotCount++;//long、double类型参数占2个
            }
            if (!IsStatic())
                argSlotCount++;//实例方法的参数列表前多一个this
       
        }

        public void copyAttributes(ref classfile.MemberInfo cfMethod)
        {
            classfile.CodeAttribute codeAttr = cfMethod.CodeAttribute();
            if (codeAttr != null)
            {
                maxStack = codeAttr.MaxStack();
                maxLocals = codeAttr.MaxLocals();
                code = codeAttr.Code();
            }
        }
    }
}
