using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Forms;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using UserControl = System.Windows.Controls.UserControl;

namespace Jarvis_2._0.Windows
{
    public partial class Settings : UserControl
    {
        #region Settings

        public static string discordUri = "";
        public static string pushoverSecret = "";
        public static string pushoverID = "";
        public static string moviesFolder = "";
        public static string torrentSavePath = "";

        #endregion

        public Settings()
        {
            InitializeComponent();

            SetSettings();
        }

        public void SetSettings()
        {
            DiscordUri.Text = discordUri;
            PushoverSecret.Text = pushoverSecret;
            PushoverId.Text = pushoverID;
            MovieFolder.Text = moviesFolder;
            TorrentDestination.Text = torrentSavePath;
        }

        public static void LoadSettings()
        {
            string[] settings = File.ReadAllLines(@"Settings.txt");

            discordUri = settings[0];
            pushoverSecret = settings[1];
            pushoverID = settings[2];
            moviesFolder = settings[3];
            torrentSavePath = settings[4];
        }

        private void Save_OnClick(object sender, RoutedEventArgs e)
        {
            List<string> settings = new List<string>();

            settings.Add(DiscordUri.Text);
            settings.Add(PushoverSecret.Text);
            settings.Add(PushoverId.Text);
            settings.Add(MovieFolder.Text);
            settings.Add(TorrentDestination.Text);

            File.WriteAllLines(@"Settings.txt", settings);

            LoadSettings();

            DataManagement.MoviesInfoList.Clear();

            UserControl usc = null;
            MainWindow.Instance.GridMain.Children.Clear();

            usc = new GroupsWindow();
            MainWindow.Instance.GridMain.Children.Add(usc);

            if (Directory.Exists(moviesFolder))
                DataManagement.CheckForNewMovies();

            if (Directory.Exists(moviesFolder))
                MoviesWindow.UpdateMovieData();
        }

        private void ChooseFolder_OnClick(object sender, RoutedEventArgs e)
        {
            using (var fbd = new FolderBrowserDialog())
            {
                DialogResult result = fbd.ShowDialog();

                if (result == DialogResult.OK && !string.IsNullOrWhiteSpace(fbd.SelectedPath))
                {
                    MovieFolder.Text = fbd.SelectedPath;
                }
            }
        }

        private void ChooseDownload_OnClick(object sender, RoutedEventArgs e)
        {
            using (var fbd = new FolderBrowserDialog())
            {
                DialogResult result = fbd.ShowDialog();

                if (result == DialogResult.OK && !string.IsNullOrWhiteSpace(fbd.SelectedPath))
                {
                    TorrentDestination.Text = fbd.SelectedPath;
                }
            }
        }
    }
}
