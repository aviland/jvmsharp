using jvmsharp.rtda;
using jvmsharp.rtda.heap;
using System;

namespace jvmsharp.instructions
{
     class ClassInitLogic//类初始化
    {
        internal static void InitClass(ref Thread thread, ref Class clas)
        {
            clas.StartInit();
            scheduleClinit(ref thread, ref clas);//类初始化
            initSuperClass(ref thread, ref clas);//超类初始化
        }

        static void scheduleClinit(ref Thread thread, ref Class cla)
        {
            Method clinit = cla.GetClinitMethod();//获取类初始化方法（clinit方法）
            if (clinit != null && clinit.Class() == cla)//若无类初始化方法则添加一个
            {
                Frame newFrame = thread.newFrame(ref clinit);
                thread.PushFrame( newFrame);
            }
        }

        static void initSuperClass(ref Thread thread, ref Class clas)
        {
            if (!clas.IsInterface())//若该类非接口，则需要初始化该类的超类
            {
                Class superClass = clas.superClass;//超类获取
                if (superClass != null && !superClass.InitStarted())
                    InitClass(ref thread, ref superClass);//初始化超类，此处使用递归
            }
        }
    }

     class BranchLogic
    {
        internal static void Branch(ref Frame frame, int offset)
        {
            int pc = frame.Thread().PC();
            int nextPC = pc + offset;
            frame.SetNextPC(nextPC);
        }
    }

     class InvokeLogic
    {
        internal static void InvokeMethod(ref Frame invokerFrame, ref Method method)
        {
           
            Thread thread = invokerFrame.Thread();//获取当前帧所在的线程
            Frame newFrame = thread.newFrame(ref method);//传入method，创建新栈帧
            thread.PushFrame( newFrame);//将新帧推入线程的虚栈

            int argSlotslot = Convert.ToInt32(method.ArgSlotCount());//因为uint大小限制，所以需要转换为int
            //      if (newFrame.LocalVars() != null)
            //             Console.WriteLine(newFrame.LocalVars().localVars.Length);
            if (argSlotslot > 0)
            {
                for (int i = argSlotslot - 1; i >= 0; i--)
                {
                    //Console.WriteLine(i+" " + newFrame.LocalVars().localVars.Length);
                    Slot slot = invokerFrame.OperandStack().PopSlot();
                //    Console.WriteLine("+++++++++++++");
                    newFrame.LocalVars().SetSlot((uint)i, slot);
                }
            }
        }
    }
}
