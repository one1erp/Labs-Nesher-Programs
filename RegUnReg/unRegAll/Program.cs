using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Runtime.InteropServices;
using System.Text;

namespace unRegAll
{
    class Program
    {
        static void Main(string[] args)
        {
            string path = Directory.GetCurrentDirectory();
            string[] filePaths = Directory.GetFiles(path, "*.dll");
            foreach (var filePath in filePaths)
            {
                bool bResult = true;
                try
                {
                    Assembly asm = Assembly.LoadFile(filePath);
                    RegistrationServices regAsm = new RegistrationServices();

                    bResult = regAsm.UnregisterAssembly(asm);
                    Console.WriteLine(filePath + " Un register: " + bResult);
                    // Console.WriteLine(asm.FullName);
                }
                catch (Exception e)
                {

                    Console.WriteLine(filePath + " " + e.Message);
                }
            }
            Console.ReadLine();
            
        }
    }
}
