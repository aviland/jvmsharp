using jvmsharp.rtda;
using jvmsharp.rtda.heap;
using System;

namespace jvmsharp.instructions
{
    class invoke_logic
    {
        public static void InvokeMethod(ref rtda.Frame invokerFrame, ref Method method)
        {

            Thread thread = invokerFrame.Thread();//获取当前帧所在的线程
            Frame newFrame = thread.newFrame(ref method);//传入method，创建新栈帧
            thread.PushFrame(ref newFrame);

            int argSlotslot =(int) method.ArgSlotCount();
           
            if (argSlotslot > 0)
            {
                for (int i = argSlotslot - 1; i >= 0; i--)
                {
                    Slot slot = invokerFrame.OperandStack().PopSlot();

                    newFrame.LocalVars().SetSlot((uint)i, ref slot);
                }
            }

            if (method.IsNative())
                if (method.Name() == "registerNatives")
                    thread.PopFrame();
                else throw new Exception("native method: "+method.Class().name+"."+method.Name()+method.Descriptor());
        }
    }
}
