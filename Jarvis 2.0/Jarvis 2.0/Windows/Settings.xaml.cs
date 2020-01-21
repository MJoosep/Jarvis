#region Imports

using System.Collections.Generic;
using System.IO;
using System.Windows;
using System.Windows.Forms;
using UserControl = System.Windows.Controls.UserControl;

#endregion

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
        }

        public static void LoadSettings()
        {
            string[] settings = File.ReadAllLines(@"Settings.txt");

            discordUri = settings[0];
            pushoverSecret = settings[1];
            pushoverID = settings[2];
            moviesFolder = settings[3];
        }

        private void Save_OnClick(object sender, RoutedEventArgs e)
        {
            List<string> settings = new List<string>();

            settings.Add(DiscordUri.Text);
            settings.Add(PushoverSecret.Text);
            settings.Add(PushoverId.Text);
            settings.Add(MovieFolder.Text);

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
    }
}
