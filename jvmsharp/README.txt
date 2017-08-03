   /*  ch01
         * static void startJVM(CmdStruct cmdStruct)
           {
               Console.Write("classpath:" + cmdStruct.cpOption + " class:" + cmdStruct.classes + " args:");
               Console.Write("[");
               for (int i = 0; i < cmdStruct.args.Length - 1; i++)
               {
                   Console.Write(cmdStruct.args[i] + " ");
               }
               Console.Write(cmdStruct.args[cmdStruct.args.Length - 1] + "]");
           }*/

        /*ch02
         * static void startJVM(Cmd cmdStruct)
           {
               Console.WriteLine("startJVM");
               classpath.classpath cp = new classpath.classpath().Parse(cmdStruct.XjreOption, cmdStruct.cpOption);//初始化classpath
               Console.Write("classpath:" +Directory.GetCurrentDirectory() + " class:" + cmdStruct.classes);
               #region
               //输出其余参数
               Console.Write(" args:[");
               if (cmdStruct.args != null&&cmdStruct.args.Length>0)
               {
                   for (int i = 0; i < cmdStruct.args.Length - 1; i++)
                   {
                       Console.Write(cmdStruct.args[i] + " ");
                   }
                   Console.Write(cmdStruct.args[cmdStruct.args.Length - 1] );//args
               }
               Console.WriteLine("]");
               #endregion
               //class参数的.替换为/，方便在jar中查找
               string className = cmdStruct.classes.Replace('.', '/');
              var  classData= cp.ReadClass(className);
               if (classData!=null&&classData.Item1 != null)
               {
                   Console.WriteLine("class data:[");
                   foreach (byte b in classData.Item1)
                   {
                       Console.Write(b + " ");
                   }
                   Console.WriteLine("]");
               }
               else
               {
                   Console.WriteLine("Could not find or load main class " + cmdStruct.classes);
                   return;
               }
           }*/
