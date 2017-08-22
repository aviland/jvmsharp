using System;
using System.Linq;

namespace jvmsharp
{
    struct Cmd
    {
        public bool helpFlag;
        public bool versionFlag;
        public bool verboseClassFlag;
        public bool verboseInstFlag;
        public string cpOption;
        public string XjreOption;
        public string classes;
        public string[] args;
        public string XssOption;

        public Cmd parseCmd(string[] args)
        {
            Cmd cmdStruct = new Cmd();
            cmdStruct.args = args;
            cmdStruct.helpFlag = false;
            cmdStruct.versionFlag = false;
            cmdStruct.verboseClassFlag = false;
            cmdStruct.verboseInstFlag = false;
            cmdStruct.cpOption = null;
            cmdStruct.classes = null;
            cmdStruct.XjreOption = null;
            cmdStruct.XssOption = null;
            if (args.Length > 0)
            {
                for (int i = 0; i < args.Length; i++)
                {
                    if (args[i].StartsWith("-"))
                    {
                        switch (args[i])
                        {
                            case "-help":
                            case "?":
                                cmdStruct.helpFlag = true;
                                cmdStruct.args = cmdStruct.args.Skip(1).ToArray();
                                //    Console.WriteLine("print help message");
                                break;
                            case "-version":
                                cmdStruct.versionFlag = true;
                                cmdStruct.args = cmdStruct.args.Skip(1).ToArray();
                                //    Console.WriteLine("print version and exit");
                                break;
                            case "-classpath":
                            case "-cp":
                                cmdStruct.cpOption = args[i + 1];
                                cmdStruct.args = cmdStruct.args.Skip(1).ToArray();
                                break;
                            case "-Xjre":
                            case "-xjre":
                                cmdStruct.XjreOption = args[i + 1];
                                cmdStruct.classes = args[i + 2];
                                cmdStruct.args = cmdStruct.args.Skip(3).ToArray();
                                break;
                            //    Console.WriteLine("path to jre");
                            case "-Xss":
                            case "-xss":
                                cmdStruct.XssOption = args[i + 1];
                                cmdStruct.args = cmdStruct.args.Skip(1).ToArray();
                                break;
                        }
                    }
                }
            }
            return cmdStruct;
        }

        public void printUsage(string args0)
        {
            Console.WriteLine("Usage:" + args0 + " [-options] class [args...]");
        }
    }
}
