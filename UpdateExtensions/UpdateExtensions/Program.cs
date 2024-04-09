using System;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Runtime.InteropServices;
using System.Security.Cryptography;

namespace UpdateExtensions
{
    class Program
    {

        private const string NEWLINE = "\r\n";
        static void Main(string[] args)
        {
            var inipath = SetIniPath();
            var iniFile = new IniFile(inipath + "\\UpdateExtensions.ini");
            string logPath = inipath + "\\UpdateExtensions.Log";
            try
            {



                WriteLogFile(logPath, "Start program " + DateTime.Now);


                string sourceFile = iniFile.GetString("General", "FromFile", "");
                WriteLogFile(logPath, "sourceFile - " + sourceFile);

                string destinationFile = iniFile.GetString("General", "ToFile", "");
                WriteLogFile(logPath, "destinationFile - " + destinationFile + NEWLINE + NEWLINE + "Start to Copy Files");


                //1
                //Copy From server

                var files = Directory.GetFiles(sourceFile);


                WriteLogFile(logPath, "Find " + files.Count() + "Files");



                foreach (var file in files)
                {
                    try
                    {


                        var fn = Path.GetFileName(file);
                        File.Copy(file, destinationFile + "\\" + fn, true);
                        WriteLogFile(logPath, "From source " + sourceFile + NEWLINE + "To " + destinationFile + "\\" + fn);

                    }
                    catch (Exception e)
                    {
                        WriteLogFile(logPath, "Error in copy file " + NEWLINE + file + " " + NEWLINE + e.Message);
                    }
                }

                //2
                //Check If nautilus is Activated

                WriteLogFile(logPath, "Check If nautilus is Activated");

                string ProcessName = iniFile.GetString("General", "ProcessName", "");

                WriteLogFile(logPath, " Process Name " + ProcessName);

                Process[] processes = Process.GetProcessesByName(ProcessName);



                if (processes.Length > 0)
                {
                    WriteLogFile(logPath, "Nautilus is Activated" + NEWLINE + "Close program");
                    return;

                }

                WriteLogFile(logPath, "Nautilus is not Activated ,Continue" + NEWLINE + "Register dlls");

                //3
                //Reg dlls

                string[] filePaths = Directory.GetFiles(destinationFile, "*.dll");
                WriteLogFile(logPath, "Find " + filePaths.Length + "dlls");
                foreach (var filePath in filePaths)
                {

                    try
                    {
                        Assembly asm = Assembly.LoadFile(filePath);
                        RegistrationServices regAsm = new RegistrationServices();

                        //Registration
                        var b = regAsm.RegisterAssembly(asm, AssemblyRegistrationFlags.SetCodeBase);
                        WriteLogFile(logPath, filePath + " " + b);


                    }
                    catch (Exception e)
                    {
                        WriteLogFile(logPath, filePath + " " + e.Message);

                    }
                }
                WriteLogFile(logPath, "/////////////////////////////end program/////////////////////////////////////" + NEWLINE + "//////////////////////////////////////////////////////////////////");


            }
            catch (Exception e)
            {

                WriteLogFile(logPath, "Error " + NEWLINE + e.Message);
            }
        }
        private static string SetIniPath()
        {
            string codeBase = Assembly.GetExecutingAssembly().CodeBase;
            UriBuilder uri = new UriBuilder(codeBase);
            string path = Uri.UnescapeDataString(uri.Path);
            return Path.GetDirectoryName(path);
        }
        public static void WriteLogFile(string path, string msg)
        {
            try
            {
                using (FileStream file = new FileStream(path, FileMode.Append, FileAccess.Write))
                {
                    var streamWriter = new StreamWriter(file);

                    streamWriter.WriteLine(msg);

                    streamWriter.WriteLine();
                    streamWriter.Close();
                }
            }
            catch
            {
            }


        }
    }
}
