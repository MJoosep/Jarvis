#region Imports

using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using Jarvis_2._0.Windows;
using MaterialDesignThemes.Wpf;

#endregion

namespace Jarvis_2._0.NewElements
{
    public class NewShowElement
    {
        #region Values

        public static int buttonID = 0;

        #endregion

        #region MakeShowCard

        public static void Add(Grid showsGrid, string _title, string _genre, string _director, string _actors, string _imdb, string _poster)
        {
            Card showFrame = new Card();
            showFrame.MouseLeftButtonDown += new MouseButtonEventHandler(ShowFrame_Click);
            showFrame.MouseEnter += new MouseEventHandler(MouseEnter);
            showFrame.MouseLeave += new MouseEventHandler(MouseLeave);
            showFrame.UniformCornerRadius = 0;
            showFrame.Name = "show" + buttonID;
            showFrame.Height = 270;

            BrushConverter brushConverter = new BrushConverter();
            showFrame.Background = (Brush)brushConverter.ConvertFrom("#FF111111");

            ShadowAssist.SetShadowDepth(showFrame, ShadowDepth.Depth1);

            Grid.SetRow(showFrame, ShowsWindow.rowCounter);
            Grid.SetColumn(showFrame, ShowsWindow.columnCounter);

            StackPanel showFrameOrientation = new StackPanel();
            showFrameOrientation.Orientation = Orientation.Horizontal;

            Card showPoster = new Card();
            showPoster.Height = 230;
            showPoster.Width = 151;
            showPoster.VerticalAlignment = VerticalAlignment.Top;
            showPoster.HorizontalAlignment = HorizontalAlignment.Left;
            showPoster.Margin = new Thickness(20);
            showPoster.IsHitTestVisible = false;

            BitmapImage PosterImage = new BitmapImage();
            PosterImage.BeginInit();
            PosterImage.UriSource = new Uri(_poster, UriKind.RelativeOrAbsolute);
            PosterImage.EndInit();

            ImageBrush poster = new ImageBrush();
            poster.ImageSource = PosterImage;

            showPoster.Background = poster;

            Grid infoAndControls = new Grid();
            infoAndControls.Height = 270;

            RowDefinition showInfoRow = new RowDefinition();
            RowDefinition buttonsRow = new RowDefinition();

            GridLengthConverter converter = new GridLengthConverter();

            showInfoRow.Height = (GridLength)converter.ConvertFromString("180");
            buttonsRow.Height = (GridLength)converter.ConvertFromString("90");

            infoAndControls.RowDefinitions.Add(showInfoRow);
            infoAndControls.RowDefinitions.Add(buttonsRow);

            Grid showInfo = new Grid();
            showInfo.Width = 146;
            showInfo.IsHitTestVisible = false;

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

            showInfo.RowDefinitions.Add(titleRow);
            showInfo.RowDefinitions.Add(GenreRow);
            showInfo.RowDefinitions.Add(directorRow);
            showInfo.RowDefinitions.Add(actorsRow);
            showInfo.RowDefinitions.Add(IMDBRow);

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

            Button openButton = new Button();

            Style buttonStyle = Application.Current.Resources["MaterialDesignFloatingActionButton"] as Style;

            openButton.Style = buttonStyle;
            openButton.Width = 40;
            openButton.Height = 40;
            openButton.HorizontalAlignment = HorizontalAlignment.Right;
            openButton.Margin = new Thickness(10);
            openButton.Click += new RoutedEventHandler(Open_Show);
            openButton.Name = "openButton" + buttonID;
            openButton.IsHitTestVisible = true;

            Button deleteButton = new Button();

            deleteButton.Style = buttonStyle;
            deleteButton.Width = 40;
            deleteButton.Height = 40;
            deleteButton.HorizontalAlignment = HorizontalAlignment.Right;
            deleteButton.Margin = new Thickness(10);
            deleteButton.Background = (Brush)brushConverter.ConvertFrom("#FF991D1D");
            deleteButton.BorderBrush = (Brush)brushConverter.ConvertFrom("#FF991D1D");
            deleteButton.Click += new RoutedEventHandler(Delete_Show);
            deleteButton.Name = "deleteButton" + buttonID;
            deleteButton.IsHitTestVisible = true;

            PackIcon playIcon = new PackIcon();
            playIcon.Kind = PackIconKind.DoorOpen;
            playIcon.Height = 24;
            playIcon.Width = 24;

            PackIcon deleteIcon = new PackIcon();
            deleteIcon.Kind = PackIconKind.Delete;
            deleteIcon.Height = 24;
            deleteIcon.Width = 24;

            buttonsPanel.Children.Add(rating);
            buttonsPanel.Children.Add(buttons);

            openButton.Content = playIcon;
            deleteButton.Content = deleteIcon;

            buttons.Children.Add(openButton);
            buttons.Children.Add(deleteButton);

            showInfo.Children.Add(title);
            showInfo.Children.Add(Genre);
            showInfo.Children.Add(director);
            showInfo.Children.Add(actors);
            showInfo.Children.Add(IMDB);

            infoAndControls.Children.Add(showInfo);
            infoAndControls.Children.Add(buttonsPanel);

            showFrameOrientation.Children.Add(showPoster);
            showFrameOrientation.Children.Add(infoAndControls);

            showFrame.Content = showFrameOrientation;

            showsGrid.Children.Add(showFrame);

            Console.Write("\n" + _title);

            buttonID++;
        }

        #endregion

        #region EventHandlers

        public static void ShowFrame_Click(object sender, RoutedEventArgs e)
        {
            
        }

        public static void Open_Show(object sender, RoutedEventArgs e)
        {
            
        }

        public static void Delete_Show(object sender, RoutedEventArgs e)
        {
            
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
