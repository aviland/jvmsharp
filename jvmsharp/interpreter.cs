using jvmsharp.rtda.heap;
using jvmsharp.rtda;
using System;
using System.Collections.Generic;

namespace jvmsharp
{
    class interpreter
    {
        instructions.BytecodeReader reader;
        Frame frame;
        instructions.Instruction inst;
        public void interpret(ref Thread thread, bool logInst)
        {
            try
            {
                loop(ref thread, logInst);
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
            }
        }

        void loop(ref rtda.Thread thread, bool logInst)
        {
            for (;;)
            {
                frame = thread.CurrentFrame();
                int pc = frame.NextPC();
                thread.SetPC(pc);
                reader.Reset(frame.Method().code, pc);
                byte opcode = reader.ReadUint8();
                inst = instructions.factory.NewInstruction(opcode);

                inst.FetchOperands(ref reader);
                frame.SetNextPC(reader.pc);

                if (logInst)
                    logInstruction(ref frame, ref inst);
                Console.WriteLine(inst.GetType().Name);
                inst.Execute(ref frame);
                if (thread.IsStackEmpty())
                    break;
            }
        }

        void logFrames(ref Thread thread)
        {
            for (; !thread.IsStackEmpty();)
            {
                var frame = thread.PopFrame();
                var method = frame.method;
                string className = method.Class().name;
                int lineNum = method.GetLineNumber(frame.NextPC());
                Console.WriteLine(">> pc:" + frame.NextPC() + " " + className + "." + method.Name() + method.Descriptor());
            }
        }

        void logInstruction(ref Frame frame, ref instructions.Instruction inst)
        {
            var method = frame.method;
            string className = method.Class().name;
            string methodName = method.Name();
            int pc = frame.Thread().PC();
            Console.WriteLine(className + "." + methodName + "#" + pc + " " + inst + " " + inst);
        }
    }
}
