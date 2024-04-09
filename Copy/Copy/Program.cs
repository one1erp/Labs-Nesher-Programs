using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace ConsoleApplication1
{

    public class SimpleFileCopy
    {
        private static void Main()
        {
            Copy(@"C:\Nautilus Extentions");
        }


        static void Copy(string targetDir)
        {

            try
            {

               

                var path = Directory.GetCurrentDirectory();
                var f = Directory.GetFiles(path);
                Console.WriteLine("Copy From current directory : " + path);
                Console.WriteLine("Copy to " + targetDir);
                foreach (var file in f)
                {
                    File.Copy(file, Path.Combine(targetDir, Path.GetFileName(file)), true);
                    Console.WriteLine(file.ToString());
                }
                Console.ReadKey();
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error" + "           " + ex.Message);
                Console.ReadKey();
            }
        }


        private static void aaa()
        {
            string fileName = "test.txt";
            string sourcePath = @"C:\temp1";
            string targetPath = @"C:\temp2";

            // Use Path class to manipulate file and directory paths. 
            string sourceFile = System.IO.Path.Combine(sourcePath, fileName);
            string destFile = System.IO.Path.Combine(targetPath, fileName);

            // To copy a folder's contents to a new location: 
            // Create a new target folder, if necessary. 
            if (!System.IO.Directory.Exists(targetPath))
            {
                System.IO.Directory.CreateDirectory(targetPath);
            }

            // To copy a file to another location and  
            // overwrite the destination file if it already exists.
            System.IO.File.Copy(sourceFile, destFile, true);

            // To copy all the files in one directory to another directory. 
            // Get the files in the source folder. (To recursively iterate through 
            // all subfolders under the current directory, see 
            // "How to: Iterate Through a Directory Tree.")
            // Note: Check for target path was performed previously 
            //       in this code example. 
            if (System.IO.Directory.Exists(sourcePath))
            {
                string[] files = System.IO.Directory.GetFiles(sourcePath);

                // Copy the files and overwrite destination files if they already exist. 
                foreach (string s in files)
                {
                    // Use static Path methods to extract only the file name from the path.
                    fileName = System.IO.Path.GetFileName(s);
                    destFile = System.IO.Path.Combine(targetPath, fileName);
                    System.IO.File.Copy(s, destFile, true);
                }
            }
            else
            {
                Console.WriteLine("Source path does not exist!");
            }

            // Keep console window open in debug mode.
            Console.WriteLine("Press any key to exit.");
            Console.ReadKey();
        }
    }
}