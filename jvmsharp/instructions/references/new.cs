using System;
using jvmsharp.rtda;
using jvmsharp.rtda.heap;

namespace jvmsharp.instructions.references
{
    class NEW : Index16Instruction
    {
        public override void Execute(ref Frame frame)
        {
            ConstantPool cp = frame.method.Class().constantPool;//获取常量池
            ClassRef classRef = (ClassRef)cp.GetConstant(Index);//从常量池中获取类的符号引用

            Class clas = classRef.ResolvedClass();//解析该引用（在存储类的classMap中查找），得到class数据
            
            if (!clas.InitStarted())
            {
                frame.RevertNextPC();
                ClassInitLogic.InitClass(ref frame.thread, ref clas);
                return;
            }
            
            if (clas.IsInterface() || clas.IsAbstract())//想new一个接口或者抽象类？科科
                throw new Exception("java.lang.InstantiationError");

            rtda.heap.Object refs = clas.NewObject();//创建类的对象
            frame.OperandStack().PushRef(refs);//压入操作数栈
        }
    }
}
