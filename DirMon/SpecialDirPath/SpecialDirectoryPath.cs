using System;

namespace SpecialDirPath
{
    public class SpecialDirectoryPath
    {
        public static string GetDesktopPath()
        {
            string path = Environment.GetFolderPath(Environment.SpecialFolder.Desktop);
            return path;
        }
    }
}
