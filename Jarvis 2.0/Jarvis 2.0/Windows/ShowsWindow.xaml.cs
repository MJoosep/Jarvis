using System;
using System.Collections.Generic;
using System.IO;
using System.Text.RegularExpressions;
using System.Windows.Controls;

namespace Jarvis_2._0.Windows
{
    public partial class ShowsWindow : UserControl
    {
        public static ShowsWindow Instance;

        public static bool needsNewRow = false;
        public static int rowCounter = 0;
        public static int columnCounter = 0;

        public ShowsWindow()
        {
            InitializeComponent();

            Instance = this;

            UpdateShowData();

            DataManagement.GenerateInitialShows();
        }

        public static void UpdateShowData()
        {
            string[] showsPaths = Directory.GetDirectories(@"Shows");
            List<string> shows = new List<string>();

            foreach (var show in showsPaths)
            {
                shows.Add(Path.GetFileName(show));
            }

            foreach (string show in shows)
            {
                DataManagement.GetShowInfo(show);
            }

            DataManagement.WriteShowDataListToFile();

            Console.WriteLine("Shows Data Updated.");
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

                Instance.ShowGrid.RowDefinitions.Add(newRow);
                needsNewRow = false;
            }

            if (columnCounter == 2)
                needsNewRow = true;
        }

        private void ScrollViewer_OnScrollChanged(object sender, ScrollChangedEventArgs e)
        {
            if (Directory.Exists(Settings.moviesFolder))
            {
                var scrollViewer = (ScrollViewer)sender;
                if (scrollViewer.VerticalOffset == scrollViewer.ScrollableHeight)
                {
                    DataManagement.GenerateMoreShows();
                }
            }
        }
    }
}
