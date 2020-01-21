#region Imports

using MaterialDesignThemes.Wpf;
using System;
using System.IO;
using System.Text.RegularExpressions;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;

#endregion

namespace Jarvis_2._0
{
    public class NewMovieElement
    {
        #region Values

        public static int buttonID = 0;

        #endregion

        #region MakeMovieCard

        public static void Add(Grid moviesGrid, string _title, string _genre, string _director, string _actors, string _imdb, string _poster)
        {
            Card movieFrame = new Card();
            movieFrame.MouseLeftButtonDown += new MouseButtonEventHandler(MovieFrame_Click);
            movieFrame.MouseEnter += new MouseEventHandler(MouseEnter);
            movieFrame.MouseLeave += new MouseEventHandler(MouseLeave);
            movieFrame.UniformCornerRadius = 0;
            movieFrame.Name = "movie" + buttonID;
            movieFrame.Height = 270;

            BrushConverter brushConverter = new BrushConverter();
            movieFrame.Background = (Brush)brushConverter.ConvertFrom("#FF111111");

            ShadowAssist.SetShadowDepth(movieFrame, ShadowDepth.Depth1);

            Grid.SetRow(movieFrame, MoviesWindow.rowCounter);
            Grid.SetColumn(movieFrame, MoviesWindow.columnCounter);

            StackPanel movieFrameOrientation = new StackPanel();
            movieFrameOrientation.Orientation = Orientation.Horizontal;

            Card moviePoster = new Card();
            moviePoster.Height = 230;
            moviePoster.Width = 151;
            moviePoster.VerticalAlignment = VerticalAlignment.Top;
            moviePoster.HorizontalAlignment = HorizontalAlignment.Left;
            moviePoster.Margin = new Thickness(20);
            moviePoster.IsHitTestVisible = false;

            BitmapImage PosterImage = new BitmapImage();
            PosterImage.BeginInit();
            PosterImage.UriSource = new Uri(_poster, UriKind.RelativeOrAbsolute);
            PosterImage.EndInit();

            ImageBrush poster = new ImageBrush();
            poster.ImageSource = PosterImage;

            moviePoster.Background = poster;

            Grid infoAndControls = new Grid();
            infoAndControls.Height = 270;

            RowDefinition movieInfoRow = new RowDefinition();
            RowDefinition buttonsRow = new RowDefinition();

            GridLengthConverter converter = new GridLengthConverter();

            movieInfoRow.Height = (GridLength)converter.ConvertFromString("180");
            buttonsRow.Height = (GridLength)converter.ConvertFromString("90");

            infoAndControls.RowDefinitions.Add(movieInfoRow);
            infoAndControls.RowDefinitions.Add(buttonsRow);

            Grid movieInfo = new Grid();
            movieInfo.Width = 146;
            movieInfo.IsHitTestVisible = false;

            RowDefinition titleRow = new RowDefinition();
            RowDefinition GenreRow = new RowDefinition();
            RowDefinition directorRow = new RowDefinition();
            RowDefinition actorsRow = new RowDefinition();
            RowDefinition IMDBRow = new RowDefinition();

            titleRow.Height = GridLength.Auto;
            directorRow.Height = GridLength.Auto;
            actorsRow.Height = GridLength.Auto;
            IMDBRow.Height = GridLength.Auto;
            GenreRow.Height = GridLength.Auto;

            movieInfo.RowDefinitions.Add(titleRow);
            movieInfo.RowDefinitions.Add(GenreRow);
            movieInfo.RowDefinitions.Add(directorRow);
            movieInfo.RowDefinitions.Add(actorsRow);
            movieInfo.RowDefinitions.Add(IMDBRow);

            TextBlock title = new TextBlock();
            TextBlock Genre = new TextBlock();
            TextBlock director = new TextBlock();
            TextBlock actors = new TextBlock();
            TextBlock IMDB = new TextBlock();

            Grid.SetRow(title, 0);
            Grid.SetRow(director, 2);
            Grid.SetRow(actors, 3);
            Grid.SetRow(IMDB, 4);
            Grid.SetRow(Genre, 1);

            title.Foreground = (Brush)brushConverter.ConvertFrom("#FFDCDCDC");
            director.Foreground = (Brush)brushConverter.ConvertFrom("#FF9C9C9C");
            actors.Foreground = (Brush)brushConverter.ConvertFrom("#FF9C9C9C");
            IMDB.Foreground = (Brush)brushConverter.ConvertFrom("#FF9C9C9C");
            Genre.Foreground = (Brush)brushConverter.ConvertFrom("#FF9C9C9C");

            title.FontSize = 16;
            director.FontSize = 10;
            actors.FontSize = 10;
            IMDB.FontSize = 10;
            Genre.FontSize = 10;

            title.Margin = new Thickness(0, 20, 0, 0);
            director.Margin = new Thickness(0, 5, 0, 0);
            actors.Margin = new Thickness(0, 5, 0, 0);
            IMDB.Margin = new Thickness(0, 5, 0, 0);
            Genre.Margin = new Thickness(0, 10, 0, 0);

            title.TextWrapping = TextWrapping.Wrap;
            director.TextWrapping = TextWrapping.Wrap;
            actors.TextWrapping = TextWrapping.Wrap;
            IMDB.TextWrapping = TextWrapping.Wrap;
            Genre.TextWrapping = TextWrapping.Wrap;

            title.Text = _title;
            director.Text = "Director: " + _director;
            actors.Text = "Actors: " + _actors;
            IMDB.Text = "IMDB: " + _imdb;
            Genre.Text = _genre;

            StackPanel buttonsPanel = new StackPanel();
            buttonsPanel.Orientation = Orientation.Vertical;
            Grid.SetRow(buttonsPanel, 1);

            RatingBar rating = new RatingBar();
            rating.Value = 3;
            rating.Margin = new Thickness(0, 0, 0, 0);
            rating.VerticalAlignment = VerticalAlignment.Bottom;

            StackPanel buttons = new StackPanel();
            buttons.Orientation = Orientation.Horizontal;
            buttons.VerticalAlignment = VerticalAlignment.Bottom;

            Grid.SetRow(rating, 5);
            Grid.SetRow(buttons, 6);

            Button playButton = new Button();
            
            Style buttonStyle = Application.Current.Resources["MaterialDesignFloatingActionButton"] as Style;

            playButton.Style = buttonStyle;
            playButton.Width = 40;
            playButton.Height = 40;
            playButton.HorizontalAlignment = HorizontalAlignment.Right;
            playButton.Margin = new Thickness(10);
            playButton.Click += new RoutedEventHandler(Play_Movie);
            playButton.Name = "playButton" + buttonID;
            playButton.IsHitTestVisible = true;

            Button deleteButton = new Button();

            deleteButton.Style = buttonStyle;
            deleteButton.Width = 40;
            deleteButton.Height = 40;
            deleteButton.HorizontalAlignment = HorizontalAlignment.Right;
            deleteButton.Margin = new Thickness(10);
            deleteButton.Background = (Brush)brushConverter.ConvertFrom("#FF991D1D");
            deleteButton.BorderBrush = (Brush)brushConverter.ConvertFrom("#FF991D1D");
            deleteButton.Click += new RoutedEventHandler(Delete_Movie);
            deleteButton.Name = "deleteButton" + buttonID;
            deleteButton.IsHitTestVisible = true;

            PackIcon playIcon = new PackIcon();
            playIcon.Kind = PackIconKind.Play;
            playIcon.Height = 24;
            playIcon.Width = 24;

            PackIcon deleteIcon = new PackIcon();
            deleteIcon.Kind = PackIconKind.Delete;
            deleteIcon.Height = 24;
            deleteIcon.Width = 24;

            buttonsPanel.Children.Add(rating);
            buttonsPanel.Children.Add(buttons);

            playButton.Content = playIcon;
            deleteButton.Content = deleteIcon;

            buttons.Children.Add(playButton);
            buttons.Children.Add(deleteButton);

            movieInfo.Children.Add(title);
            movieInfo.Children.Add(Genre);
            movieInfo.Children.Add(director);
            movieInfo.Children.Add(actors);
            movieInfo.Children.Add(IMDB);

            infoAndControls.Children.Add(movieInfo);
            infoAndControls.Children.Add(buttonsPanel);

            movieFrameOrientation.Children.Add(moviePoster);
            movieFrameOrientation.Children.Add(infoAndControls);

            movieFrame.Content = movieFrameOrientation;
            
            moviesGrid.Children.Add(movieFrame);

            Console.Write("\n" + _title);

            buttonID++;
        }

        #endregion

        #region EventHandlers

        public static void MovieFrame_Click(object sender, RoutedEventArgs e)
        {
            Card pressed = e.Source as Card;

            if (pressed != null && pressed.Name.Contains("movie"))
            {
                int movieID = Convert.ToInt32(pressed.Name.Substring(5));

                MovieInfoWindow.currentMovie = movieID;

                string[] readText = File.ReadAllLines(@"Movies\MovieInfo.txt");

                string[] separate = Regex.Split(readText[movieID], "I---I");

                Console.WriteLine("\n\nShowing Movie Info - " + separate[0]);

                UserControl usc = null;
                MainWindow.Instance.GridMain.Children.Clear();

                usc = new MovieInfoWindow(separate[0], separate[1], separate[2], separate[3], separate[4], separate[5], separate[6], separate[7], separate[9], separate[11]);
                MainWindow.Instance.GridMain.Children.Add(usc);

                MoviesWindow.columnCounter = 0;
                MoviesWindow.rowCounter = 0;

                buttonID = 0;
            }
        }

        public static void Play_Movie(object sender, RoutedEventArgs e)
        {
            switch (ChromeCastManager.playerStatus)
            {
                case "Paused":
                    Console.WriteLine("\n\nResuming playback.. ");
                    ChromeCastManager._mediaPlayer.Play();
                    ChromeCastManager.playerStatus = "Playing";
                    return;
                case "Playing":
                    Console.WriteLine("\n\nPausing playback.. ");
                    ChromeCastManager._mediaPlayer.Pause();
                    ChromeCastManager.playerStatus = "Paused";
                    return;
                case "Stopped":
                    Button pressed = e.Source as Button;
                    int movieID = Convert.ToInt32(pressed.Name.Substring(10));
                    string path = DataManagement.MoviesList[movieID].MoviePath;
                    Console.WriteLine("\n\nStarting Playback: " + DataManagement.MoviesList[movieID].MovieName);
                    ChromeCastManager c = new ChromeCastManager();
                    ChromeCastManager.movie = c;
                    ChromeCastManager.movie.PlayMovieAsync(path);
                    return;
            }
        }

        public static void Delete_Movie(object sender, RoutedEventArgs e)
        {
            if(ChromeCastManager.playerStatus == "Playing")
            {
                Console.WriteLine("\n\nStopping playback.. ");

                ChromeCastManager._mediaPlayer.Stop();

                ChromeCastManager.playerStatus = "Stopped";
            }
        }

        public static void MouseEnter(object sender, RoutedEventArgs e)
        {
            Card hover = e.Source as Card;

            BrushConverter brushConverter = new BrushConverter();

            if (hover != null)
            {
                hover.Background = (Brush)brushConverter.ConvertFrom("#FF1B1B1B");
            }
        }

        public static void MouseLeave(object sender, RoutedEventArgs e)
        {
            Card hover = e.Source as Card;

            BrushConverter brushConverter = new BrushConverter();

            if (hover != null)
            {
                hover.Background = (Brush)brushConverter.ConvertFrom("#FF111111");
            }
        }

        #endregion
    }
}
