using jvmsharp.rtda.heap;
using jvmsharp.rtda;
using System;

namespace jvmsharp
{
    class interpreter
    {
        public void interpret(ref Method method, bool logInst)
        {
            Thread thread = new Thread().newThread();
            Frame frame = thread.newFrame(ref method);
            try
            {
                thread.PushFrame(ref frame);
           //     long t1 = (long)(DateTime.UtcNow - new DateTime(1970, 1, 1, 0, 0, 0, DateTimeKind.Utc)).TotalMilliseconds;
                loop(ref thread, logInst);
                 //  long t2 = (long)(DateTime.UtcNow - new DateTime(1970, 1, 1, 0, 0, 0, DateTimeKind.Utc)).TotalMilliseconds;
                 //   Console.WriteLine("interpret time: " + (t2 - t1) + " ms");
            }
            catch (Exception e)
            {
                logFrames(ref thread);
                Console.WriteLine(e);
            }
        }
     /*   public void interpret(ref Method method)
        {
            Thread thread = new Thread().newThread();
            Frame frame = thread.newFrame(ref method);
            try
            {
                thread.PushFrame(ref frame);
                loop(ref thread, ref method.code);
            }
            catch (Exception e)
            {
                Console.Write("LocalVars:[");
                foreach (Slot s in frame.LocalVars().localVars)
                    Console.Write(s.num + " ");
                Console.Write("]\nOperandStack:[");
                foreach (Slot s in frame.OperandStack().slots)
                    Console.Write(s.num + " ");
                Console.WriteLine("]\n" + e);
            }
        }

        public void interpret(ref classfile.MemberInfo methodInfo)
        {
            classfile.CodeAttribute codeAttr = methodInfo.CodeAttribute();
            UInt16 maxLocals = codeAttr.MaxLocals();
            UInt16 maxStack = codeAttr.MaxStack();
            byte[] bytecode = codeAttr.Code();

            rtda.Thread thread = new rtda.Thread().newThread();
            rtda.Frame frame = thread.newFrame(maxLocals, maxStack);
            try
            {
                thread.PushFrame(ref frame);
                loop(ref thread, ref bytecode);

            }
            catch (Exception e)
            {
                Console.Write("LocalVars:[");
                foreach (rtda.heap.Slot s in frame.LocalVars().localVars)
                {
                    Console.Write(s.num + " ");
                }
                Console.WriteLine("]");
                Console.Write("OperandStack:[");
                foreach (rtda.heap.Slot s in frame.OperandStack().slots)
                {
                    Console.Write(s.num + " ");
                }
                Console.WriteLine("]");
                Console.WriteLine(e.Message);
            }
        }
        */
        void loop(ref rtda.Thread thread, bool logInst)
        {
            instructions.BytecodeReader reader = new instructions.BytecodeReader();
            for (;;)
            {
                Frame frame = thread.CurrentFrame();
                int pc = frame.NextPC();
                thread.SetPC(pc);

                reader.Reset(ref frame.method.code, ref pc);
                byte opcode = reader.ReadUint8();
                //  Console.WriteLine(Convert.ToByte(opcode));
                instructions.Instruction inst = new instructions.factory().NewInstruction(opcode);
                //   Console.WriteLine(inst.GetType().Name);
                inst.FetchOperands(ref reader);
                frame.SetNextPC(reader.GetPC());

                if (logInst)
                    logInstruction(ref frame, ref inst);

                inst.Execute(ref frame);
                if (thread.isStackEmpty())
                    break;
            }
        }

        void logFrames(ref Thread thread)
        {
            for (; !thread.isStackEmpty();)
            {
                var frame = thread.PopFrame();
                var method = frame.method;
                string className = method.Class().name;
                Console.WriteLine(">> pc:" + frame.NextPC() + " " + className + "." + method.Name() + method.Descriptor());
            }
        }

        void logInstruction(ref Frame frame, ref instructions.Instruction inst)
        {
            var method = frame.Method();
            string className = method.Class().name;
            string methodName = method.Name();
            int pc = frame.Thread().PC();
            Console.WriteLine(className + "." + methodName + "#" + pc + " " + inst + " " + inst);
        }
        /*
        void loop(ref rtda.Thread thread, ref byte[] bytecode)
        {
            Frame frame = thread.PopFrame();
            instructions.BytecodeReader reader = new instructions.BytecodeReader();
            for (;;)
            {
                int pc = frame.NextPC();
                thread.SetPC(pc);

                //decode
                reader.Reset(ref bytecode, ref pc);
                byte opcode = reader.ReadUint8();
                //  Console.WriteLine(Convert.ToByte(opcode));
                instructions.Instruction inst = new instructions.factory().NewInstruction(opcode);
                //   Console.WriteLine(inst.GetType().Name);
                inst.FetchOperands(ref reader);
                frame.SetNextPC(reader.GetPC());

                //execute
                Console.WriteLine("pc: " + pc + " inst:" + inst.GetType().Name + " 0x" + Convert.ToString(opcode, 16));
                inst.Execute(ref frame);
            }
        }*/
    }
}
