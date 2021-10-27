using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SpecialDiretoryUtility
{
    public class SpecialDiretory
    {
        public static string GetDesktopPath()
        {
            string path = Environment.GetFolderPath(Environment.SpecialFolder.Desktop);
            return path;
        }
    }
}
