using jvmsharp.rtda.heap;
using System;
using System.Collections.Generic;

namespace jvmsharp.rtda.heap
{
    class Method : ClassMember
    {
        internal uint maxStack;
        internal uint maxLocals;
        internal byte[] code;      
    
        internal ExceptionTable exceptionTable;
        internal classfile.LineNumberTableAttribute lineNumberTable;
        internal classfile.ExceptionsAttribute exceptions;
        internal byte[] parameterAnnotationData;
        internal  byte[] annotationDefaultData;
        internal MethodDescriptor parsedDescriptor;
        internal uint argSlotCount;

        internal bool isClinit()
        {
            return IsStatic() && name == "<clinit>";
        }
        public uint MaxLocals()
        {
            return maxLocals;
        }

        public Method[] newMethods(ref Class clas, ref classfile.MemberInfo[] cfMethods)
        {
            Method[] methods = new Method[cfMethods.Length];
            for (int i = 0; i < cfMethods.Length; i++)
            {
                methods[i] = newMethod(ref clas, ref cfMethods[i]);
            }
            return methods;
        }

        internal uint ArgSlotCount()
        {
            return argSlotCount;
        }
        public bool IsBridge()
        {
            return 0 != (accessFlags & AccessFlags.ACC_BRIDGE);
        }


        public bool IsStrict()
        {
            return 0 != (accessFlags & AccessFlags.ACC_STRICT);
        }
        public bool IsVarargs()
        {
            return 0 != (accessFlags & AccessFlags.ACC_VARARGS);
        }

        public bool IsSynchronized()
        {
            return 0 != (accessFlags &AccessFlags. ACC_SYNCHRONIZED);
        }
        public Method newMethod(ref Class clas, ref classfile.MemberInfo cfMethod)
        {
            Method method = new Method();
            method.clas = clas;
            method.copyMemberInfo(ref cfMethod);
            method.copyAttributes(ref cfMethod);
            MethodDescriptor md = new MethodDescriptorParser().parseMethodDescriptor(method.descriptor);
            method.parsedDescriptor = md;
            method.calcArgSlotCount(md.parameterTypes);
            if (method.IsNative())
                method.injectCodeAttribute(md.returnType);
            return method;
        }

        internal bool IsAbstract()
        {
            return 0 != (accessFlags &AccessFlags. ACC_ABSTRACT);
        }

        internal void copyAttributes(ref classfile.MemberInfo cfMethod)
        {
            classfile.CodeAttribute codeAttr = cfMethod.CodeAttribute();
            if (codeAttr != null)
            {
                maxStack = codeAttr.MaxStack();
                maxLocals = codeAttr.MaxLocals();
                code = codeAttr.Code();
                               lineNumberTable = codeAttr.LineNumberTableAttribute();
                exceptionTable = new ExceptionTable(ref codeAttr.exceptionTable, ref clas.constantPool);
            }
            exceptions = cfMethod.ExceptionsAttribute();
            annotationData = cfMethod.RuntimeVisibleAnnotationsAttributeData();
            parameterAnnotationData = cfMethod.RuntimeVisibleParameterAnnotationsAttributeData();
            annotationDefaultData = cfMethod.AnnotationDefaultAttributeData();
        }
        private bool IsNative()
        {
            return 0 != (accessFlags & AccessFlags.ACC_NATIVE);
        }

        internal Class ReturnType()
        {
            var returnType = parsedDescriptor.returnType;
            var returnClassName = new ClassNameHelper().toClassName(returnType);
            return clas.loader.LoadClass(returnClassName);
        }
        internal int GetLineNumber(int pc)
        {
            if (IsNative())
                return -2;
            if (lineNumberTable == null)
                return -1;
            return lineNumberTable.GetLineNumber(pc);
        }

        void injectCodeAttribute(string returnType)
        {
            this.maxStack = 4;
            maxLocals = argSlotCount;
            switch (returnType[0])
            {
                case 'V': code = new byte[] { 0xfe, 0xb1 }; break;
                case 'D': code = new byte[] { 0xfe, 0xaf }; break;
                case 'F': code = new byte[] { 0xfe, 0xae }; break;
                case 'J': code = new byte[] { 0xfe, 0xad }; break;
                case 'L':
                case '[':
                    code = new byte[] { 0xfe, 0xb0 }; break;
                default: code = new byte[] { 0xfe, 0xac }; break;
            }
        }

        internal bool isConstructor()
        {
            return !IsStatic() && name == "<init>";
        }

        void calcArgSlotCount(List<string> paramTypes)
        {
            foreach (string paramType in paramTypes)
            {
                argSlotCount++;//一般参数占1个
                if (paramType == "J" || paramType == "D")
                    argSlotCount++;//long、double类型参数占2个
            }
            if (!IsStatic())
                argSlotCount++;//实例方法的参数列表前多一个this
        }

        internal Class[] ExceptionTypes()
        {
            if (exceptions == null)
                return null;
            ushort[] exIndexTable = exceptions.ExceptionIndexTable();
            List<Class> exClasses = new List<Class>();
            var cp = clas.constantPool;
            foreach (ushort exIndex in exIndexTable)
            {
                ClassRef classRef = (ClassRef)cp.GetConstant(exIndex);
                exClasses.Add(classRef.ResolvedClass());
            }
            return exClasses.ToArray();
        }

        internal byte[] ParameterAnnotationData()
        {
            return this.parameterAnnotationData;
        }

        internal Class[] ParameterTypes()
        {
            if (argSlotCount == 0)
                return null;
            List<string> paramTypes = parsedDescriptor.parameterTypes;
            Class[] paramClasses = new Class[paramTypes.Count];
            for (int i = 0; i < paramClasses.Length; i++)
            {
                string paramClassName = new ClassNameHelper().toClassName(paramTypes[i]);
                paramClasses[i] = clas.loader.LoadClass(paramClassName);
            }
            return paramClasses;
        }

        internal int FindExceptionHandler(Class exClass, int pc)
        {
            ExceptionHandler handler = exceptionTable.findExceptionHandler(exClass, pc);
            if (handler != null)
                return handler.handlerPc;
            else return -1;
        }
    }
}
