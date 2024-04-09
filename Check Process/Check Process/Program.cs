using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Check_Process
{
    class Program
    {
        private static string IniPath;
        static void Main(string[] args)
        {

            SetIniPath();
            var iniFile = new IniFile(IniPath+"\\CheckProcess.ini");
            var list = iniFile.GetSectionValues("Parameters");
            string ProcessPath = list["ProcessPath"];
            string Arguments = list["Arguments"];
            string ProcessName = list["ProcessName"];

            Process[] processes = Process.GetProcessesByName(ProcessName);
            if (processes.Length < 1)
            {
              
                var process = new Process();
                process.StartInfo = new ProcessStartInfo(ProcessPath);
                process.StartInfo.Arguments = Arguments;
                process.Start();


            }
        }
        
        private static void SetIniPath()
        {
            string codeBase = Assembly.GetExecutingAssembly().CodeBase;
            UriBuilder uri = new UriBuilder(codeBase);
            string path = Uri.UnescapeDataString(uri.Path);
            IniPath = Path.GetDirectoryName(path);
        }
    }
}
