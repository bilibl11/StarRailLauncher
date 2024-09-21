using Microsoft.Win32;
using StarRailLauncher.DataPack;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using static StarRailLauncher.MainWindow;
using System.Diagnostics;

namespace StarRailLauncher
{
    /// <summary>
    /// SettingPage.xaml 的交互逻辑
    /// </summary>
    public partial class SettingPage : Page
    {
        MainWindow mainWindow = new();

        private void ShowLoacation()
        {
            GamePathTextBox.Text = MainWindow.PathData.gameExePath;
            ModTextBox.Text = MainWindow.PathData.modExePath;
            ModManagerTextBox.Text = MainWindow.PathData.modManagerExePath;
        }

        public SettingPage()
        {
            InitializeComponent();

            ShowLoacation();
        }

        private void Close()
        {
            Close();
        }

        private void LocationGame_Click(object sender, RoutedEventArgs e)
        {
            var dialog = new OpenFileDialog();
            dialog.Filter = "Executable Files|*.exe|All Files|*.*";
            if (dialog.ShowDialog() == true)
            {
                string path = dialog.FileName;
                GamePathTextBox.Text = path;
                MainWindow.PathData.gameExePath = path;
                DataPack.PathData.Save(path, "gameExePath");
                mainWindow.ChangeBStartState(BStartState.Normal);
            }
        }

        private void LocationMod_Click(object sender, RoutedEventArgs e)
        {
            var dialog = new OpenFileDialog();
            dialog.Filter = "Executable Files|*.exe|All Files|*.*";
            if (dialog.ShowDialog() == true)
            {
                string path = dialog.FileName;
                ModTextBox.Text = path;
                MainWindow.PathData.modExePath = path;
                DataPack.PathData.Save(path, "modExePath");
            }
        }

        private void LocationModManager_Click(object sender, RoutedEventArgs e)
        {
            var dialog = new OpenFileDialog();
            dialog.Filter = "Executable Files|*.exe|All Files|*.*";
            if (dialog.ShowDialog() == true)
            {
                string path = dialog.FileName;
                ModManagerTextBox.Text = path;
                MainWindow.PathData.modManagerExePath = path;
                DataPack.PathData.Save(path, "modManagerExePath");
            }
        }

        private void OpenOfficialURL_Click(object sender, RoutedEventArgs e)
        {
            string url = this.OfficialURLTextBox.Text;
            Process.Start(new ProcessStartInfo(url) { UseShellExecute = true });
        }

        private void OpenMiyousheURL_Click(object sender, RoutedEventArgs e)
        {
            string url = this.MiyousheURLTextBox.Text;
            Process.Start(new ProcessStartInfo(url) { UseShellExecute = true });
        }
    }
}
