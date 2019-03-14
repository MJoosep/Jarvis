#region Imports

using System;
using System.IO;
using System.Text.RegularExpressions;
using System.Windows.Controls;
using System.Windows.Input;

#endregion
//clean

namespace Jarvis_2._0
{
    public partial class MoviesWindow : UserControl
    {
        #region Values

        public static MoviesWindow Instance;

        public static bool needsNewRow = false;
        public static int rowCounter = 0;
        public static int columnCounter = 0;

        #endregion
        //clean

        public MoviesWindow()
        {
            InitializeComponent();

            Instance = this;

            DataManagement.GenerateInitialMovies();
        }

        #region UpdatingUI

        public static void UpdateMovieData()
        {
            string[] readText = File.ReadAllLines(@"Movies\Movies.txt");

            foreach (string line in readText)
            {
                string[] separate = Regex.Split(line, "I---I");

                DataManagement.GetMovieInfo(separate[0]);
            }

            DataManagement.WriteMovieDataListToFile();

            Console.WriteLine("Movie Data Updated.");
        }

        public static void SetPosition()
        {
            if (columnCounter < 2)
            {
                columnCounter++;

                needsNewRow = false;
            }

            if (columnCounter == 2 && needsNewRow == true)
            {
                columnCounter = 0;
                rowCounter++;

                RowDefinition newRow = new RowDefinition();

                Instance.MovieGrid.RowDefinitions.Add(newRow);
                needsNewRow = false;
            }

            if (columnCounter == 2)
                needsNewRow = true;
        }

        private void ScrollViewer_ScrollChanged(object sender, ScrollChangedEventArgs e)
        {
            var scrollViewer = (ScrollViewer)sender;
            if (scrollViewer.VerticalOffset == scrollViewer.ScrollableHeight)
                DataManagement.GenerateMoreMovies();
        }

        #endregion
        //clean
    }
}
