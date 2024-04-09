using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace MoveFiles
{
    public class Program
    {

        private const string NEWLINE = "\r\n";

        public static void Main(string[] args)
        {



            var inipath = SetIniPath();
            var iniFile = new IniFile(inipath + "\\MoveFiles.ini");
            string logPath = inipath + "\\MoveFiles.Log";
            try
            {
                WriteLogFile(logPath, "Start program " + DateTime.Now);

                string sourceFile = iniFile.GetString("General", "FromFile", "");
                WriteLogFile(logPath, "sourceFile - " + sourceFile);

                string destinationFile = iniFile.GetString("General", "ToFile", "");
                WriteLogFile(logPath, "destinationFile - " + destinationFile);

                string ld = iniFile.GetString("General", "LastDays", "");
                WriteLogFile(logPath, "Last Days - " + ld);

                int lastDays;
                if (!int.TryParse(ld, out lastDays))
                {
                    WriteLogFile(logPath, "Last days is not valid " + NEWLINE + "Close program");

                    return;
                }


                var files = Directory.GetFiles(sourceFile);
                WriteLogFile(logPath, "Find " + files.Count() + " Files");

                foreach (var file in files)
                {
                    var date = File.GetLastWriteTime(file);
                    WriteLogFile(logPath, "date modified is " + date);
                    if (DateTime.Now.AddDays(-lastDays) > date)
                    {
                        var fn = Path.GetFileName(file);
                        File.Copy(file, destinationFile + fn, true);
                        WriteLogFile(logPath, "Copy file from " + file + " " + NEWLINE + " Copy file To " + destinationFile + fn);
                        File.Delete(file);
                        WriteLogFile(logPath, "Deleted file from " + file);


                    }
                }

            }
            catch (Exception e)
            {
                WriteLogFile(logPath, "Error " + NEWLINE + e.Message);
            }
            WriteLogFile(logPath, "end program " + NEWLINE + "/////////////////////////////" + NEWLINE + DateTime.Now);


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
