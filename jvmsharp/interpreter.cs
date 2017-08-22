using jvmsharp.rtda.heap;
using jvmsharp.rtda;
using System;
using System.Collections.Generic;

namespace jvmsharp
{
    class interpreter
    {
        public  void interpret(ref Thread thread, bool logInst)
        {
            try
            {
                loop(ref thread, logInst);
            }
            catch (Exception e) {
                Console.WriteLine(e);
            }
        }
         int i = 0;
            List<string> ls = new List<string>();
         void loop(ref rtda.Thread thread, bool logInst)
        {
            instructions.BytecodeReader reader = new instructions.BytecodeReader();
            for (;;)
            {
                Frame frame = thread.CurrentFrame();
                int pc = frame.NextPC();
            //      Console.WriteLine("frame" + frame.method.Name());
                thread.SetPC(pc);

                reader.Reset(frame.Method().code, pc);
             
                byte opcode = reader.ReadUint8();
                instructions.Instruction inst = instructions.factory.NewInstruction(opcode);
                // if (!ls.Contains(inst.GetType().Name)) {
                //          ls.Add(inst.GetType().Name);
                i++;
              
       //         }
         
                inst.FetchOperands(ref reader);
                frame.SetNextPC(reader.pc);

                if (logInst)
                    logInstruction(ref frame, ref inst);
          //   Console.WriteLine(inst.GetType().Name);
                inst.Execute(ref frame);
                if (i >= 301 && i <=700)
                {
                 //   Console.WriteLine(pc);
                   // Console.WriteLine(inst.GetType().Name);
                   
                    if (frame.LocalVars().localVars != null)
                    {
                    //    Console.WriteLine();
                        /*  Console.Write("LocalVars:[");
                          foreach (Slot s in frame.LocalVars().localVars)
                          {
                              Console.Write(s.num + " ");
                              if (s.refer != null)
                                  Console.Write(s.refer.GetType().Name + " ");
                          }
                                                     Console.WriteLine("]");*/
                    }
                                                         if (frame.OperandStack() != null)
                    {
                   //     Console.WriteLine();
                       /* Console.Write("OperandStack:[");
                        foreach (Slot s in frame.OperandStack().slots)
                        {
                            Console.Write(s.num + " ");
                            if (s.refer != null)
                                Console.Write(s.refer.GetType().Name + " ");
                        }
                        Console.WriteLine("]");*/
                    }

                }

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
            int lineNum= method.GetLineNumber(frame.NextPC());
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
