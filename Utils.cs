using IWshRuntimeLibrary;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Net.Http;
using Newtonsoft.Json;

namespace StarRailLauncher
{
    internal class Utils
    {
        public static void CreateDesktopShortcut(string fileName, string exePath)
        {
            try
            {
                WshShell shell = new WshShell();
                string filePath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.Desktop), fileName);
                if (System.IO.File.Exists(filePath))
                {
                    System.IO.File.Delete(filePath);
                }
                //快捷键方式创建的位置、名称
                IWshShortcut shortcut = (IWshShortcut)shell.CreateShortcut(filePath);
                shortcut.TargetPath = exePath;
                //指定应用程序的工作目录
                shortcut.WorkingDirectory = exePath;
                //目标应用程序的窗口状态分为普通、最大化、最小化【1,3,7】
                shortcut.WindowStyle = 1;
                //快捷方式的描述信息
                shortcut.Description = fileName;
                //快捷方式图标文件路径
                shortcut.IconLocation = exePath;
                shortcut.Arguments = "";
                shortcut.Save();
            }
            catch (Exception e)
            {
                MessageBox.Show($"快捷方式创建失败了，请给开发者提供以下信息：\n{e.Message}");
            }
        }
    }
}
