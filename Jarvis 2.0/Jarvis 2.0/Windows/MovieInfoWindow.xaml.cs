#region Imports

using MaterialDesignThemes.Wpf;
using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media.Imaging;

#endregion
//clean

namespace Jarvis_2._0
{
    public partial class MovieInfoWindow : UserControl
    {
        #region Values

        System.Windows.Threading.DispatcherTimer dispatcherTimer;

        public static int loadingTime = 0;
        public static int currentMovie = 0;

        #endregion
        //clean

        public MovieInfoWindow(string _title, string _plot, string _genre, string _director, string _actors, string _year, string _runtime, string _rated, string _imdb, string _poster)
        {
            InitializeComponent();

            SetMovieData(_title, _plot, _genre, _director, _actors, _year, _runtime, _rated, _imdb, _poster);

            NewMovieElement.buttonID = 0;

            DataManagement.generatedMovies = 0;
        }

        #region UpdatingUI

        public void SetMovieData(string _title, string _plot , string _genre, string _director, string _actors, string _year, string _runtime, string _rated, string _imdb, string _poster)
        {
            BitmapImage PosterImage = new BitmapImage();
            PosterImage.BeginInit();
            PosterImage.UriSource = new Uri(_poster, UriKind.RelativeOrAbsolute);
            PosterImage.EndInit();

            var title = Title;
            var director = Director;
            var plot = Plot;
            var genre = Genre;
            var actors = Actors;
            var imdb = IMDB;
            var runtime = Runtime;
            var rated = Rated;
            var year = Year;
            var poster = MoviePoster;

            title.Text = _title;
            director.Text = "Director: " + _director;
            plot.Text = _plot;
            actors.Text = "Actors: " + _actors;
            imdb.Text = "IMDB: " + _imdb;
            genre.Text = _genre;
            runtime.Text = _runtime;
            rated.Text = "Rated: " + _rated;
            year.Text = _year;
            poster.ImageSource = PosterImage;
        }

        #endregion
        //clean

        #region EventHandlers

        private void PlayPause_Click(object sender, RoutedEventArgs e)
        {
            switch (ChromeCastManager.playerStatus)
            {
                case "Paused":
                    Console.WriteLine("\nResuming Playback.. ");
                    ChromeCastManager._mediaPlayer.Play();
                    ChromeCastManager.playerStatus = "Playing";
                    PackIcon playPauseIcon = PlayPauseIcon;
                    playPauseIcon.Kind = PackIconKind.Play;
                    return;
                case "Playing":
                    Console.WriteLine("\nPausing Playback.. ");
                    ChromeCastManager._mediaPlayer.Pause();
                    ChromeCastManager.playerStatus = "Paused";
                    PackIcon playPauseIconToPause = PlayPauseIcon;
                    playPauseIconToPause.Kind = PackIconKind.Pause;
                    return;
                case "Stopped":
                    ButtonProgressAssist.SetIsIndicatorVisible(PlayPause, true);
                    dispatcherTimer = new System.Windows.Threading.DispatcherTimer();
                    dispatcherTimer.Tick += new EventHandler(dispatcherTimer_Tick);
                    dispatcherTimer.Interval = TimeSpan.FromMilliseconds(5);
                    dispatcherTimer.Start();
                    string path = DataManagement.MoviesList[currentMovie].MoviePath;
                    Console.WriteLine("\nStarting Playback: " + DataManagement.MoviesList[currentMovie].MovieName);
                    ChromeCastManager c = new ChromeCastManager();
                    ChromeCastManager.movie = c;
                    ChromeCastManager.movie.PlayMovieAsync(path);
                    return;
            }
        }

        private void Stop_Click(object sender, RoutedEventArgs e)
        {
            if (ChromeCastManager.playerStatus == "Playing")
            {
                Console.WriteLine("\nStopping playback.. ");

                ButtonProgressAssist.SetIsIndicatorVisible(Stop, true);

                dispatcherTimer = new System.Windows.Threading.DispatcherTimer();
                dispatcherTimer.Tick += new EventHandler(dispatcherTimer_Tick);
                dispatcherTimer.Interval = TimeSpan.FromMilliseconds(10);
                dispatcherTimer.Start();

                ChromeCastManager._mediaPlayer.Stop();

                ChromeCastManager.playerStatus = "Stopped";
            }
        }

        private void dispatcherTimer_Tick(object sender, EventArgs e)
        {
            loadingTime++;

            if(loadingTime == 101)
            {
                dispatcherTimer.Stop();

                loadingTime = 0;

                ButtonProgressAssist.SetIsIndicatorVisible(PlayPause, false);

                ButtonProgressAssist.SetIsIndicatorVisible(Stop, false);
            }
        }

        private void BackButton_Click(object sender, RoutedEventArgs e)
        {
            Console.Write("\n\nBack Clicked.\n");

            MoviesWindow.columnCounter = 0;
            MoviesWindow.rowCounter = 0;

            UserControl usc = null;
            MainWindow.Instance.GridMain.Children.Clear();

            usc = new MoviesWindow();
            MainWindow.Instance.GridMain.Children.Add(usc);
        }

        #endregion
        //clean
    }
}
