using jvmsharp.rtda;
using jvmsharp.rtda.heap;
using System;

namespace jvmsharp.instructions
{
    class classInit_logic//类初始化
    {
        public static void InitClass(ref Thread thread, ref Class clas)
        {
            clas.StartInit();
            scheduleClinit(ref thread, ref clas);//类初始化
            initSuperClass(ref thread, ref clas);//超类初始化
        }

        static void scheduleClinit(ref Thread thread, ref Class cla)
        {
            Method clinit = cla.GetClinitMethod();//获取类初始化方法（clinit方法）
            if (clinit != null)//若无类初始化方法则添加一个
            {
                Frame newFrame = thread.newFrame(ref clinit);
                thread.PushFrame(ref newFrame);
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

    class branch_logic
    {
        internal static void Branch(ref Frame frame, int offset)
        {
            int pc = frame.Thread().PC();
            int nextPC = pc + offset;
            frame.SetNextPC(nextPC);
        }
    }

    class invoke_logic
    {
        public static void InvokeMethod(ref Frame invokerFrame, ref Method method)
        {

            Thread thread = invokerFrame.Thread();//获取当前帧所在的线程
            Frame newFrame = thread.newFrame(ref method);//传入method，创建新栈帧
            thread.PushFrame(ref newFrame);

    /*        if (method.IsNative())
                if (method.Name() == "registerNatives")
                    thread.PopFrame();
                else throw new Exception("native method: " + method.Class().name + "." + method.Name() + method.Descriptor());
                */
            int argSlotslot = Convert.ToInt32(method.ArgSlotCount());//因为uint大小限制，所以需要转换为int
            if (argSlotslot > 0)
            {
                for (int i = argSlotslot - 1; i >= 0; i--)
                {
                    object slot = invokerFrame.OperandStack().PopSlot();
                    newFrame.localVars.SetSlot(Convert.ToUInt32(i), slot);
                }
            }
        }
    }
}
