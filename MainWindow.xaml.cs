using Microsoft.Win32;
using StarRailLauncher.DataPack;
using System.Configuration;
using System.Diagnostics;
using System.Security.Principal;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace StarRailLauncher
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {

        MainWindowData mainWindowData = new();
        public static SettingPage SettingPage = new();
        public static PathData? PathData;

        public static bool IsShowSettingPage = false;
        public static bool IsFirstShowSettingPage = true;

        public enum BStartState
        {
            NoGame,Normal
        }
        
        private void SetBStart()
        {
            PathData = PathData.Load();
            if (PathData.gameExePath == "")
            {
                ChangeBStartState(BStartState.NoGame);
            }
            else if(PathData.gameExePath != "")
            {
                ChangeBStartState(BStartState.Normal);
            }
        }

        public void ChangeBStartState(BStartState state)
        {
            switch (state)
            {
                case BStartState.NoGame:
                    {
                        this.BStart.IsEnabled = false;
                        this.BStart.Content = "定位游戏";
                        break;
                    }
                case BStartState.Normal:
                    {
                        this.BStart.IsEnabled = true;
                        this.BStart.Content = "开始游戏";
                        break;
                    }
            }
        }

        private void SetBackground(string picturePath)
        {
            if(picturePath != null && picturePath != "")
            {
                ImageBrush brush = new ImageBrush();
                brush.ImageSource = new BitmapImage(new Uri(picturePath, UriKind.Relative));
                brush.Stretch = System.Windows.Media.Stretch.UniformToFill;
                this.Background = brush;
            }
        }

        public MainWindow()
        {
            InitializeComponent();

            SetBackground(MainWindowData.Load().BackgroundPath);

            SetBStart();

        }
        private void BTitle_MouseDown(object sender, MouseButtonEventArgs e)
        {
            DragMove();
        }

        private void BClose_Click(object sender, RoutedEventArgs e)
        {
            Close();
            Application.Current.Shutdown();  
        }

        private void BMinimize_Click(object sender, RoutedEventArgs e)
        {
            WindowState = WindowState.Minimized;
        }

        private void BSettings_Click(object sender, RoutedEventArgs e)
        {
            if(!IsShowSettingPage && IsFirstShowSettingPage)
            {
                SettingFrame.Content = SettingPage;
                IsShowSettingPage = true;
                IsFirstShowSettingPage = false;
            }
            else if(!IsShowSettingPage && !IsFirstShowSettingPage)
            {
                SettingFrame.Visibility = Visibility.Visible;
                IsShowSettingPage = true;
            }
            else if(IsShowSettingPage)
            {
                SettingFrame.Visibility = Visibility.Collapsed;
                IsShowSettingPage = false;
            }
        }
        
        private void BStart_MouseEnter(object sender, MouseEventArgs e)
        {
            BStart.Background = new SolidColorBrush(Color.FromArgb(204, 255, 255, 255));
        }

        private void BStart_MouseLeave(object sender, MouseEventArgs e)
        {
            BStart.Background = new SolidColorBrush(Color.FromArgb(0, 255, 255, 255));
        }
        
        private async void BStart_Click(object sender, RoutedEventArgs e)
        {
            if (Mod.StartMod())
            {
                await Task.Delay(1000);
                Game.StartGame();
                WindowState = WindowState.Minimized;
            }
            else
            {
                Debug.WriteLine("Mod启动失败!");
            }
        }

        private void BOption_Click(object sender, RoutedEventArgs e)
        {
            if(GOption.Visibility == Visibility.Collapsed)
            {
                GOption.Visibility = Visibility.Visible;
            }
            else
            {
                GOption.Visibility = Visibility.Collapsed;
            }
        }

        private void GOption_MouseLeave(object sender, MouseEventArgs e)
        {
            if (GOption.Visibility == Visibility.Collapsed)
            {
                GOption.Visibility = Visibility.Visible;
            }
            else
            {
                GOption.Visibility = Visibility.Collapsed;
            }
        }

        private void BOption_MouseEnter(object sender, MouseEventArgs e)
        {
            BOption.Background = new SolidColorBrush(Color.FromArgb(204, 255, 255, 255));
        }

        private void BOption_MouseLeave(object sender, MouseEventArgs e)
        {
            BOption.Background = new SolidColorBrush(Color.FromArgb(0, 255, 255, 255));
        }

        private void BRevisewallpaper_Click(object sender, RoutedEventArgs e)
        {
            var dialog = new OpenFileDialog();
            dialog.Filter = "图片文件 (*.jpg;*.jpeg;*.png;*.gif)|*.jpg;*.jpeg;*.png;*.gif|所有文件 (*.*)|*.*";
            if (dialog.ShowDialog() == true)
            {
                SetBackground(dialog.FileName);

                //储存路径
                mainWindowData.BackgroundPath = dialog.FileName;
                MainWindowData.Save(mainWindowData);
            }
        }

        private void BOpenGamePath_Click(object sender, RoutedEventArgs e)
        {
            string folderPath = PathData.gameExePath;
            folderPath = folderPath.Replace("\\StarRail.exe", "");
            Debug.WriteLine(folderPath);
            Process.Start("explorer.exe", folderPath);
        }

        private void BAddShortCut_Click(object sender, RoutedEventArgs e)
        {
            string deskPath = Environment.GetFolderPath(Environment.SpecialFolder.Desktop);
            Utils.CreateDesktopShortcut("星穹铁道启动器", PathData.gameExePath);
        }

        private void BModManagement_Click(object sender, RoutedEventArgs e)
        {
            Process.Start(PathData.modManagerExePath);
        }
    }
}