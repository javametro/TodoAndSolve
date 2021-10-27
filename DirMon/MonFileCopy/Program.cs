using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SpecialDiretoryUtility;

namespace MonFileCopy
{
    class Program
    {
        public static string desktopPath = string.Empty;
        public static string destPath = string.Empty;
        public static string watchPath = string.Empty;
        static void Main(string[] args)
        {
            string[] arguments = Environment.GetCommandLineArgs();
            if(args.Length != 2)
            {
                Console.WriteLine("Arguments Error, Usage: MonFileCopy.exe watchDir destDir");
                return;
            }
            //desktopPath = GetCurrentDesktopPath();
            //Console.WriteLine(desktopPath);
            //watchPath = desktopPath + @"\Share";
            //watchPath = @"\\192.168.5.130\Share";
            watchPath = arguments[1];
            //Console.WriteLine(arguments[0]);
            //Console.WriteLine(arguments[1]);
            //return;
            destPath = arguments[2];
            //destPath = desktopPath + @"\test";
            var watcher = new FileSystemWatcher(watchPath);

            watcher.NotifyFilter = NotifyFilters.Attributes
                                 | NotifyFilters.CreationTime
                                 | NotifyFilters.DirectoryName
                                 | NotifyFilters.FileName
                                 | NotifyFilters.LastAccess
                                 | NotifyFilters.LastWrite
                                 | NotifyFilters.Security
                                 | NotifyFilters.Size;

            watcher.Changed += OnChanged;
            watcher.Created += OnCreated;
            watcher.Deleted += OnDeleted;
            watcher.Renamed += OnRenamed;
            watcher.Error += OnError;

            watcher.Filter = "*.exe";
            watcher.IncludeSubdirectories = true;
            watcher.EnableRaisingEvents = true;

            Console.WriteLine("Press enter to exit.");
            Console.ReadLine();
        }

        private static void CopyFiles(string strSrcDir, string strDestDir)
        {
            string[] files = Directory.GetFiles(strSrcDir);
            string fileName = string.Empty;
            string destFileName = string.Empty;
            foreach(string file in files)
            {
                fileName = Path.GetFileName(file);
                destFileName = destPath + @"\" + fileName;
                if (fileName.EndsWith("exe") || fileName.EndsWith("pdb"))
                {
                    File.Copy(file, destFileName, true);
                }
            }
        }

        private static string GetCurrentDesktopPath()
        {
            string path = SpecialDiretory.GetDesktopPath();
            return path;
        }

        private static void OnChanged(object sender, FileSystemEventArgs e)
        {
            if (e.ChangeType != WatcherChangeTypes.Changed)
            {
                return;
            }

            CopyFiles(watchPath, destPath);
            Console.WriteLine($"Changed: {e.FullPath}");
        }

        private static void OnCreated(object sender, FileSystemEventArgs e)
        {
            string value = $"Created: {e.FullPath}";
            CopyFiles(watchPath, destPath);
            Console.WriteLine(value);
        }

        private static void OnDeleted(object sender, FileSystemEventArgs e) =>
            Console.WriteLine($"Deleted: {e.FullPath}");

        private static void OnRenamed(object sender, RenamedEventArgs e)
        {
            Console.WriteLine($"Renamed:");
            Console.WriteLine($"    Old: {e.OldFullPath}");
            Console.WriteLine($"    New: {e.FullPath}");
        }

        private static void OnError(object sender, ErrorEventArgs e) =>
            PrintException(e.GetException());

        private static void PrintException(Exception ex)
        {
            if (ex != null)
            {
                Console.WriteLine($"Message: {ex.Message}");
                Console.WriteLine("Stacktrace:");
                Console.WriteLine(ex.StackTrace);
                Console.WriteLine();
                PrintException(ex.InnerException);
            }
        }

    }
}
