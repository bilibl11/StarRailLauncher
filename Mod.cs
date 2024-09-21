using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StarRailLauncher
{
    class Mod
    {
        public static bool StartMod()
        {
            ProcessStartInfo startInfo = new ProcessStartInfo  
            {  
                FileName = MainWindow.PathData.modExePath,  
                UseShellExecute = true,
                Verb = "runas",
                LoadUserProfile = true,
                WorkingDirectory = Path.GetDirectoryName(MainWindow.PathData.modExePath)
            };  
            try
            {
                Process.Start(startInfo);
                return true;
            }
            catch (Exception e)
            {
                Debug.WriteLine(e);
                return false;
            }
        }
    }
}
