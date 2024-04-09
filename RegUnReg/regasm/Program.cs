using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Runtime.InteropServices;
using System.Text;

namespace regasm
{
    class Program
    {
        static void Main(string[] args)
        {
            string path = Directory.GetCurrentDirectory();
            regDlls(path);
            regOcx(path);
        }

        private static void regOcx(string path)
        {
            string[] filePaths = Directory.GetFiles(path, "*.ocx");
            foreach (var filePath in filePaths)
            {
                try
                {
                    var x = System.Diagnostics.Process.Start("regsvr32", filePath);

                    Console.WriteLine(filePath + " Register ocx: secces");
                    // Console.WriteLine(asm.FullName);
                }
                catch (Exception e)
                {
                    Console.WriteLine(filePath + " " + e.Message);
                }
            }
            Console.ReadLine();
        }

        private static void regDlls(string path)
        {
            string[] filePaths = Directory.GetFiles(path, "*.dll");
            foreach (var filePath in filePaths)
            {
                bool bResult = true;
                try
                {
                    Assembly asm = Assembly.LoadFile(filePath);
                    RegistrationServices regAsm = new RegistrationServices();
                    
                    bResult = regAsm.RegisterAssembly(asm, AssemblyRegistrationFlags.SetCodeBase);

                    Console.WriteLine(filePath + " Register: " + bResult);
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
