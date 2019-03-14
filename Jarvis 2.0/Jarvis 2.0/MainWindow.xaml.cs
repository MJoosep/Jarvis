#region Imports

using System;
using System.ComponentModel;
using System.IO;
using System.Net;
using System.Threading;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using Jarvis_2._0.NewElements;
using Jarvis_2._0.Windows;

#endregion
//clean

namespace Jarvis_2._0
{
    public partial class MainWindow : Window
    {
        #region Values

        public System.Windows.Forms.NotifyIcon JarvisIcon;

        public static bool connected = false;

        public static MainWindow Instance;

        ConsoleOutputManager outputter;

        public static bool consoleActive = false;

        public static string outPut = "";

        #endregion
        //clean

        public MainWindow()
        {
            InitializeComponent();

            Instance = this;

            Windows.Settings.LoadSettings();

            Thread ConsoleOutPut = new Thread(ConsoleOut);
            ConsoleOutPut.Start();

            UserControl usc = null;
            GridMain.Children.Clear();

            usc = new GroupsWindow();
            GridMain.Children.Add(usc);

            WebHandler.WebSocketHandler();

            JarvisIcon = new System.Windows.Forms.NotifyIcon();
            JarvisIcon.Icon = new System.Drawing.Icon(@"Assets\j_fire_dribbble_AWm_icon.ico");
            JarvisIcon.MouseDoubleClick +=
                new System.Windows.Forms.MouseEventHandler
                    (JarvisIcon_MouseDoubleClick);

            if(Directory.Exists(Windows.Settings.moviesFolder))
                DataManagement.CheckForNewMovies();

            if (Directory.Exists(Windows.Settings.moviesFolder))
                MoviesWindow.UpdateMovieData();


            //Thread torrent = new Thread(Torrent.TorrentManager.Ragnar);
            //torrent.Start();
        }
        //cleanable

        #region Updating UI elements

        public static void ConnectionCheck()
        {
            Instance.Dispatcher.Invoke(() =>
            {
                if (connected == true)
                {
                    Instance.ConnectionProgress.Visibility = Visibility.Hidden;
                    Instance.ConnectionTrue.Visibility = Visibility.Visible;
                }
                if (connected == false)
                {
                    Instance.ConnectionProgress.Visibility = Visibility.Visible;
                    Instance.ConnectionTrue.Visibility = Visibility.Hidden;
                }
            });
        }

        private void Window_StateChanged()
        {
            if (Instance.WindowState == WindowState.Minimized)
            {
                Instance.ShowInTaskbar = false;
                JarvisIcon.BalloonTipTitle = "Minimize Sucessful";
                JarvisIcon.BalloonTipText = "Minimized the app ";
                JarvisIcon.ShowBalloonTip(400);
                JarvisIcon.Visible = true;
            }
            else if (Instance.WindowState == WindowState.Normal)
            {
                JarvisIcon.Visible = false;
                Instance.ShowInTaskbar = true;
            }
        }

        public void ConsoleOut()
        {
            outputter = new ConsoleOutputManager();
            Console.SetOut(outputter);
            Console.WriteLine("Started");
        }

        #endregion
        //clean

        #region EventHandlers

        private void ButtonOpenMenu_Click(object sender, RoutedEventArgs e)
        {
            ButtonCloseMenu.Visibility = Visibility.Visible;
            ButtonOpenMenu.Visibility = Visibility.Collapsed;
        }

        private void ButtonCloseMenu_Click(object sender, RoutedEventArgs e)
        {
            ButtonCloseMenu.Visibility = Visibility.Collapsed;
            ButtonOpenMenu.Visibility = Visibility.Visible;
        }

        private void ListViewMenu_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            UserControl usc = null;
            GridMain.Children.Clear();

            MoviesWindow.columnCounter = 0;
            MoviesWindow.rowCounter = 0;

            NewMovieElement.buttonID = 0;

            ShowsWindow.columnCounter = 0;
            ShowsWindow.rowCounter = 0;

            NewShowElement.buttonID = 0;

            consoleActive = false;

            DataManagement.generatedShows = 0;
            DataManagement.generatedMovies = 0;

            switch (((ListViewItem)((ListView)sender).SelectedItem).Name)
            {
                case "Actions":
                    Console.Write("\n\nActions Clicked. \n");
                    usc = new ActionsWindow();
                    GridMain.Children.Add(usc);
                    break;
                case "ConsoleWindow":
                    usc = new ConsoleWindow();
                    GridMain.Children.Add(usc);
                    consoleActive = true;
                    break;
                case "Groups":
                    Console.Write("\n\nGroups Clicked. \n");
                    usc = new GroupsWindow();
                    GridMain.Children.Add(usc);
                    break;
                case "Movies":
                    Console.Write("\n\nMovies Clicked. \n");
                    usc = new MoviesWindow();
                    GridMain.Children.Add(usc);
                    break;
                case "Shows":
                    Console.Write("\n\nShows Clicked. \n");
                    usc = new ShowsWindow();
                    GridMain.Children.Add(usc);
                    break;
                case "Mobile":
                    usc = new ConsoleWindow();
                    GridMain.Children.Add(usc);
                    consoleActive = true;
                    break;
                default:
                    break;
            }
        }

        private void Grid_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            DragMove();
        }

        private void ExitButton_Click(object sender, RoutedEventArgs e)
        {
            System.Windows.Application.Current.Shutdown();
        }

        private void MinimizeButton_Click(object sender, RoutedEventArgs e)
        {
            Instance.WindowState = WindowState.Minimized;
            Window_StateChanged();
        }

        private void SettingsButton_Click(object sender, RoutedEventArgs e)
        {
            UserControl usc = null;
            GridMain.Children.Clear();

            MoviesWindow.columnCounter = 0;
            MoviesWindow.rowCounter = 0;

            NewMovieElement.buttonID = 0;

            consoleActive = false;

            DataManagement.generatedMovies = 0;

            usc = new Windows.Settings();
            GridMain.Children.Add(usc);
        }

        void JarvisIcon_MouseDoubleClick(object sender, System.Windows.Forms.MouseEventArgs e)
        {
            this.WindowState = WindowState.Normal;
            Window_StateChanged();
            Instance.Activate();
        }

        #endregion
        //clean
    }
}
