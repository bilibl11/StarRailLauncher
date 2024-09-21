using StarRailLauncher.DataPack;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StarRailLauncher
{
    internal class Game
    {
        public static void StartGame()
        {
            ProcessStartInfo startInfo = new ProcessStartInfo();
            startInfo.FileName = MainWindow.PathData.gameExePath;
            startInfo.UseShellExecute = true;
            startInfo.WorkingDirectory = Environment.CurrentDirectory;
            startInfo.Verb = "runas"; // 请求以管理员身份运行  
            try
            {
                Process.Start(startInfo);
            }
            catch (Exception e)
            {
                Debug.WriteLine(e);
            }
        }
    }
}
