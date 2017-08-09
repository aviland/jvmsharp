using System.Collections.Generic;

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
                methods[i] = newMethod(ref clas, ref cfMethods[i]);
            }
            return methods;
        }
        public Method newMethod(ref Class clas, ref classfile.MemberInfo cfMethod)
        {
            Method method = new Method();
            method.clas = clas;
            method.copyMemberInfo(ref cfMethod);
            method.copyAttributes(ref cfMethod);
            var md = new MethodDescriptorParser().parseMethodDescriptor(method.descriptor);
            method.calcArgSlotCount(md.parameterTypes);
            if (method.IsNative())
                method.injectCodeAttribute(md.returnType);
            return method;
        }

        void injectCodeAttribute(string returnType)
        {
            this.maxStack = 4;
            maxLocals = argSlotCount;
            switch (returnType[0])
            {
                case 'V': code =new byte[]{0xfe,0xb1 };break;
                case 'D': code = new byte[] { 0xfe, 0xaf }; break;
                case 'F': code = new byte[] { 0xfe, 0xae }; break;
                case 'J': code = new byte[] { 0xfe, 0xad }; break;
                case 'L':
                case '[':
                    code = new byte[] { 0xfe, 0xb0 }; break;
                default: code = new byte[] { 0xfe, 0xac }; break;
            }
        }

        void calcArgSlotCount(List<string> paramTypes)
        {
            MethodDescriptor parsedDescriptor = new MethodDescriptorParser().parseMethodDescriptor(descriptor);
            foreach (string paramType in paramTypes)
            {
                argSlotCount++;//一般参数占1个
                if (paramType == "J" || paramType == "D")
                    argSlotCount++;//long、double类型参数占2个
            }
            if (!IsStatic())
                argSlotCount++;//实例方法的参数列表前多一个this
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
