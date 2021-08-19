#region Imports

using MaterialDesignThemes.Wpf;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows;
using System.Text.RegularExpressions;
using System.Net;
using OMDbApiNet;
using OMDbApiNet.Model;
using System.ComponentModel;
using Jarvis_2._0.DataClasses;
using Jarvis_2._0.NewElements;
using Jarvis_2._0.Windows;

#endregion

namespace Jarvis_2._0
{
    public class DataManagement
    {
        #region Values

        public static List<AddGroup> GroupsList = new List<AddGroup>();
        public static List<AddAction> ActionsList = new List<AddAction>();
        public static List<AddMovie> MoviesList = new List<AddMovie>();
        public static List<AddMovieInfo> MoviesInfoList = new List<AddMovieInfo>();
        public static List<AddShow> ShowsList = new List<AddShow>();
        public static List<AddShowInfo> ShowsInfoList = new List<AddShowInfo>();

        public static int generatedMovies = 0;
        public static int generatedShows = 0;

        #endregion

        #region GroupsData

        public static void SaveGroups()
        {
            List<string> GroupsToSave = new List<string>();

            foreach (var group in GroupsList)
            {
                GroupsToSave.Add(group.GroupName + "I---I" + group.Command + "I---I" + group.ActionsData + "I---I" + group.Active);
            }

            File.WriteAllLines(@"Groups\Groups.txt", GroupsToSave);
            
            Console.Write("\nGroups Saved.\n");
        }

        public static void GenerateGroups()
        {
            GroupsList = null;
            GroupsList = new List<AddGroup>();

            var group = new AddGroup();
            string[] readText = File.ReadAllLines(@"Groups\Groups.txt");

            Console.WriteLine("\nCurrent Groups are: ");

            foreach (string line in readText)
            {
                string[] separate = Regex.Split(line, "I---I");

                Console.WriteLine(separate[0]);

                group.GroupName = separate[0];
                group.Command = separate[1];
                group.ActionsData = separate[2];
                group.Active = separate[3];

                GroupsList.Add(group);

                group = null;
                group = new AddGroup();
            }

            foreach (AddGroup groupItem in GroupsList)
            {
                NewGroupElement.Add(GroupsWindow.Instance.Groups, groupItem);
            }
        }

        public static void RewriteSpecificGroupSaveData(string groupName, string newName, string newCommand, string active, string dataType)
        {
            string[] readTextGroups = File.ReadAllLines(@"Groups\Groups.txt");
            string[] newWriteData = new string[readTextGroups.Length];

            int writeCounter = 0;

            foreach (string line in readTextGroups)
            {
                string[] separate = Regex.Split(line, "I---I");

                if (separate[0].Replace(" ", string.Empty) == groupName)
                {
                    if (dataType == "ChangeName")
                    {
                        newWriteData[writeCounter] = newName + "I---I" + newCommand + "I---I" + separate[2] + "I---I" + separate[3];

                        Console.Write("\nGroup Name Changed from {0} to {1}.\n", separate[0], newName);
                    }

                    if (dataType == "ToggleStatus")
                    {
                        newWriteData[writeCounter] = separate[0] + "I---I" + separate[1] + "I---I" + separate[2] + "I---I" + active;

                        Console.Write("\nGroup Toggle Changed from {0} to {1}.\n", separate[3], active);
                    }
                }
                else
                {
                    newWriteData[writeCounter] = line;
                }

                writeCounter++;
            }

            File.WriteAllLines(@"Groups\Groups.txt", newWriteData.ToList());
        }

        public static void DeleteGroup(string groupName)
        {
            var oldLines = System.IO.File.ReadAllLines(@"Groups\Groups.txt");
            var newLines = oldLines.Where(line => !line.Replace(" ", string.Empty).Contains(groupName));

            File.WriteAllLines(@"Groups\Groups.txt", newLines);

            Console.Write("\nGroup Deleted.\n");
        }

        public static void UpdateNumberOfActions()
        {
            string[] readTextGroups = File.ReadAllLines(@"Groups\Groups.txt");
            string[] readTextActions = File.ReadAllLines(@"Actions\Actions.txt");

            string[] writeGroupData = new string[readTextGroups.Length];
            string[] groupNames = new string[readTextGroups.Length];
            string[] groupSaveData = new string[readTextGroups.Length];
            string[] actionNames = new string[readTextActions.Length];
            string[] newGroupSaveData = new string[readTextGroups.Length];

            int newGroupCounter = 0;
            int newGroupSaveCounter = 0;
            int groupCounter = 0;
            int actionCounter = 0;

            foreach (string line in readTextGroups)
            {
                string[] separate = Regex.Split(line, "I---I");

                groupNames[groupCounter] = separate[0];
                groupSaveData[groupCounter] = separate[2];

                groupCounter++;
            }

            foreach (string line in readTextActions)
            {
                string[] separate = Regex.Split(line, "I---I");

                actionNames[actionCounter] = separate[0];

                actionCounter++;
            }

            foreach (string groupSave in groupSaveData)
            {
                if (groupSave != null)
                    newGroupSaveData[newGroupCounter] = ModifyString(groupSave, actionCounter);

                newGroupCounter++;
            }

            foreach (string line in readTextGroups)
            {
                string[] separate = Regex.Split(line, "I---I");

                writeGroupData[newGroupSaveCounter] = separate[0] + "I---I" + separate[1] + "I---I" + newGroupSaveData[newGroupSaveCounter] + "I---I" + separate[3];

                newGroupSaveCounter++;
            }

            File.WriteAllLines(@"Groups\Groups.txt", writeGroupData.ToList());

            Console.Write("\nGroups checklists Updated.\n");
        }

        public static void DeleteSpecificSaveData(string actionName)
        {
            string[] readTextActions = File.ReadAllLines(@"Actions\Actions.txt");
            string[] readTextGroups = File.ReadAllLines(@"Groups\Groups.txt");

            string[] newWriteData = new string[readTextGroups.Length];

            int groupsCounter = 0;
            int actionsCounter = 0;
            int indexToDelete = 0;

            foreach (string line in readTextActions)
            {
                string[] separate = Regex.Split(line, "I---I");

                if (separate[0] == actionName)
                {
                    indexToDelete = actionsCounter;
                }

                actionsCounter++;
            }

            foreach (string line in readTextGroups)
            {
                string[] separate = Regex.Split(line, "I---I");

                string NewValue = separate[2].Remove(indexToDelete, 1);

                newWriteData[groupsCounter] = separate[0] + "I---I" + separate[1] + "I---I" + NewValue + "I---I" + separate[3];

                groupsCounter++;
            }

            File.WriteAllLines(@"Groups\Groups.txt", newWriteData.ToList());

            Console.Write("\nAction removed from groups checklists.\n");
        }

        public static void SetBadgesAndToggles()
        {
            string[] readTextGroups = File.ReadAllLines(@"Groups\Groups.txt");

            foreach (string line in readTextGroups)
            {
                string[] separate = Regex.Split(line, "I---I");

                int active = 0;

                foreach (char action in separate[2])
                {
                    if (action == '1')
                        active++;
                }

                Badged activeActions = GroupsWindow.Instance.Groups.FindName(separate[0].Replace(" ", string.Empty) + "badge") as Badged;

                activeActions.Badge = active;

                ToggleButton activeButton = GroupsWindow.Instance.Groups.FindName(separate[0].Replace(" ", string.Empty) + "toggle") as ToggleButton;

                if (separate[3] == "Y")
                    activeButton.IsChecked = true;

                if (separate[3] == "N")
                    activeButton.IsChecked = false;
            }

            Console.Write("\nBadges and Toggles Set.\n");
        }

        public static void GenerateActionsCheckList(string groupName)
        {
            string[] readTextActions = File.ReadAllLines(@"Actions\Actions.txt");
            string[] readTextGroups = File.ReadAllLines(@"Groups\Groups.txt");

            string checkListValue = "";

            foreach (string line in readTextGroups)
            {
                string[] separate = Regex.Split(line, "I---I");

                if (separate[0].Replace(" ", string.Empty) == groupName)
                    checkListValue = separate[2];
            }

            string[] ActionNames = new string[readTextActions.Length];

            int ActionNamesCounter = 0;

            foreach (string line in readTextActions)
            {
                string[] separate = Regex.Split(line, "I---I");

                ActionNames[ActionNamesCounter] = separate[0];
                ActionNamesCounter++;
            }

            Style checkBoxStyle = Application.Current.Resources["MaterialDesignUserForegroundCheckBox"] as Style;

            bool[] checkboxValues = SetChecklistValues(checkListValue);

            GroupsWindow.Instance.ActionsCheckList.Children.Clear();

            for (int i = 0; i < readTextActions.Length; i++)
            {
                CheckBox action = new CheckBox();
                action.Style = checkBoxStyle;
                action.Margin = new Thickness(16, 4, 16, 0);
                action.Name = ActionNames[i].Replace(" ", string.Empty) + "CheckBox";
                action.Content = ActionNames[i];
                action.IsChecked = checkboxValues[i];

                GroupsWindow.Instance.ActionsCheckList.Children.Add(action);
            }

            Console.Write("\nGroup Checklist Generated And Opened.\n");
        }

        private static string ModifyString(string str, int length)
        {
            if (str != null && length != 0)
            {
                if (str.Length < length)
                {
                    while (str.Length < length)
                    {
                        str = str + "0";
                    }
                }

                if (str.Length > length)
                {
                    while (str.Length > length)
                    {
                        str = str.Remove(str.Length - 1);
                    }
                }
            }

            return str;
        }

        public static bool[] SetChecklistValues(string statusList)
        {
            bool[] boolStatusList = new bool[statusList.Length];

            int statusNumber = 0;

            foreach (char character in statusList)
            {
                if (character == '1')
                    boolStatusList[statusNumber] = true;
                if (character == '0')
                    boolStatusList[statusNumber] = false;
                statusNumber++;
            }

            return boolStatusList;
        }

        #endregion

        #region ActionsData

        public static void SaveActions()
        {
            List<string> ActionsToSave = new List<string>();

            foreach (var action in ActionsList)
            {
                ActionsToSave.Add(action.ActionName + "I---I" + action.ActionType + "I---I" + action.ActionMessage + "I---I" + action.ActionFileName + "I---I" + action.ActionPathName);
            }

            File.WriteAllLines(@"Actions\Actions.txt", ActionsToSave);

            Console.Write("\nActions Saved.\n");
        }

        public static void GenerateActions()
        {
            ActionsList = null;
            ActionsList = new List<AddAction>();

            var action = new AddAction();
            string[] readText = File.ReadAllLines(@"Actions\Actions.txt");

            Console.WriteLine("\nCurrent Actions are: ");

            foreach (string line in readText)
            {
                string[] separate = Regex.Split(line, "I---I");

                Console.WriteLine(separate[0]);

                action.ActionName = separate[0];
                action.ActionType = separate[1];
                action.ActionMessage = separate[2];
                action.ActionFileName = separate[3];
                action.ActionPathName = separate[4];

                ActionsList.Add(action);

                action = null;
                action = new AddAction();
            }

            foreach (AddAction actionItem in ActionsList)
            {
                NewActionElement.Add(ActionsWindow.Instance.Actions, actionItem);
            }
        }

        public static void RewriteSpecificActionsSaveData(string actionName, string newName, string newMessage, string newType, string newPathName, string newPath)
        {
            string[] readTextActions = File.ReadAllLines(@"Actions\Actions.txt");
            string[] newWriteData = new string[readTextActions.Length];

            int writeCounter = 0;

            foreach (string line in readTextActions)
            {
                string[] separate = Regex.Split(line, "I---I");

                if (separate[0].Replace(" ", string.Empty) == actionName)
                {
                    if(newType == "Type")
                        newWriteData[writeCounter] = newName + "I---I" + newType + "I---I" + newMessage + "I---I" + "No File" + "I---I" + "No Path";

                    if(newType == "Open File" || newType == "Close File" || newType == "Open Website")
                        newWriteData[writeCounter] = newName + "I---I" + newType + "I---I" + "No Message" + "I---I" + newPathName + "I---I" + newPath;
                }
                else
                {
                    newWriteData[writeCounter] = line;
                }

                writeCounter++;
            }

            File.WriteAllLines(@"Actions\Actions.txt", newWriteData.ToList());

            Console.Write("\nAction Changed.\n");
        }

        public static void DeleteAction(string actionName)
        {
            var oldLines = System.IO.File.ReadAllLines(@"Actions\Actions.txt");
            var newLines = oldLines.Where(line => !line.Replace(" ", string.Empty).Contains(actionName));

            File.WriteAllLines(@"Actions\Actions.txt", newLines);

            Console.Write("\nAction Deleted.\n");
        }

        public static void SetSelectedActions(string groupName)
        {
            string[] readTextGroups = File.ReadAllLines(@"Groups\Groups.txt");
            string[] readTextActions = File.ReadAllLines(@"Actions\Actions.txt");

            List<String> actionsToRun = new List<String>();

            string actionsBinaryValue = "";

            foreach (string line in readTextGroups)
            {
                string[] separate = Regex.Split(line, "I---I");

                if (separate[0].Replace(" ", string.Empty) == groupName)
                {
                    actionsBinaryValue = separate[2];
                }
            }

            for (int i = 0; i < actionsBinaryValue.Length; i++)
            {
                if (actionsBinaryValue[i] == '1')
                {
                    actionsToRun.Add(readTextActions[i]);
                }
            }

            string[] actionsToRunArray = new string[actionsToRun.Count];

            Console.Write("\nRunning Actions: \n");

            for (int i = 0; i < actionsToRun.Count; i++)
            {
                actionsToRunArray[i] = actionsToRun[i];
            }

            RunActions.RunActionsFunction(actionsToRunArray);
        }

        public static string ChooseNewPath()
        {
            string newPath = "";

            Microsoft.Win32.OpenFileDialog chooseNewFile = new Microsoft.Win32.OpenFileDialog();

            chooseNewFile.DefaultExt = ".exe";
            chooseNewFile.Filter = "Exe Files (.exe)|*.exe|All Files (*.*)|*.*";

            Nullable<bool> result = chooseNewFile.ShowDialog();

            if (result == true)
            {
                newPath = chooseNewFile.FileName;

                Console.Write("New Path: " + newPath);
            }

            return newPath;
        }

        #endregion

        #region MoviesData

        public static void GenerateInitialMovies()
        {
            GenerateMoviesList();

            Console.Write("\nMovies Generated: ");

            string[] readText = File.ReadAllLines(@"Movies\MovieInfo.txt");

            foreach (string line in readText)
            {
                if(generatedMovies < 6)
                {
                    string[] separate = Regex.Split(line, "I---I");

                    NewMovieElement.Add(MoviesWindow.Instance.MovieGrid, separate[0], separate[2], separate[3], separate[4], separate[9], separate[11]);

                    MoviesWindow.SetPosition();

                    generatedMovies++;
                }
            }
        }

        public static void GenerateMoreMovies()
        {
            string[] readText = File.ReadAllLines(@"Movies\MovieInfo.txt");

            for(int i = 0; i<3; i++)
            {
                if (generatedMovies < readText.Length)
                {
                    string[] separate = Regex.Split(readText[generatedMovies], "I---I");

                    NewMovieElement.Add(MoviesWindow.Instance.MovieGrid, separate[0], separate[2], separate[3], separate[4], separate[9], separate[11]);

                    MoviesWindow.SetPosition();

                    generatedMovies++;
                }
            }  
        }

        public static void GenerateMoviesList()
        {
            MoviesList = null;
            MoviesList = new List<AddMovie>();

            var movie = new AddMovie();
            string[] readTextPaths = File.ReadAllLines(@"Movies\Movies.txt");
            

            foreach (string line in readTextPaths)
            {
                string[] separate = Regex.Split(line, "I---I");

                movie.MovieName = separate[0];
                movie.MoviePath = separate[1];

                MoviesList.Add(movie);

                movie = null;
                movie = new AddMovie();
            }
        }

        public static void GetMovieInfo(string movie)
        {
            OmdbClient omdb = new OmdbClient("a6c971b", true);

            SearchList searchList = omdb.GetSearchList(movie);

            SearchItem result = searchList.SearchResults[0];

            try
            {
                Item item = omdb.GetItemByTitle(result.Title);

                var movieVar = new AddMovieInfo();

                movieVar.Title = item.Title;
                movieVar.Plot = item.Plot;
                movieVar.Genre = item.Genre;
                movieVar.Director = item.Director;
                movieVar.Actors = item.Actors;
                movieVar.Year = item.Year;
                movieVar.Runtime = item.Runtime;
                movieVar.Rated = item.Rated;
                movieVar.Metascore = item.Metascore;
                movieVar.IMDB = item.ImdbRating;
                movieVar.RottenTomatoes = item.TomatoRating;
                movieVar.Poster = item.Poster;

                MoviesInfoList.Add(movieVar);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
            }
        }

        public static void WriteMovieDataListToFile()
        {
            List<string> movieDataString = new List<string>();

            foreach(AddMovieInfo movie in MoviesInfoList)
            {
                movieDataString.Add(movie.Title + "I---I" +
                    movie.Plot + "I---I" +
                    movie.Genre + "I---I" +
                    movie.Director + "I---I" +
                    movie.Actors + "I---I" +
                    movie.Year + "I---I" +
                    movie.Runtime + "I---I" +
                    movie.Rated + "I---I" +
                    movie.Metascore + "I---I" +
                    movie.IMDB + "I---I" +
                    movie.RottenTomatoes + "I---I" +
                    movie.Poster);
            }

            WriteAllLinesBetter(@"Movies\MovieInfo.txt", movieDataString.ToArray());

            Console.WriteLine("Movies List Updated Successfully");
        }

        public static void WriteAllLinesBetter(string path, params string[] lines)
        {
            if (path == null)
                throw new ArgumentNullException("path");
            if (lines == null)
                throw new ArgumentNullException("lines");

            using (var stream = File.OpenWrite(path))
            using (StreamWriter writer = new StreamWriter(stream))
            {
                if (lines.Length > 0)
                {
                    for (int i = 0; i < lines.Length - 1; i++)
                    {
                        writer.WriteLine(lines[i]);
                    }
                    writer.Write(lines[lines.Length - 1]);
                }
            }
        }

        public static void CheckForNewMovies()
        {
            string path = Path.Combine(Windows.Settings.moviesFolder, "NewMovies");

            string[] directories = Directory.GetDirectories(path);
            string[] movieNames = new string[directories.Length];

            List<string> fileNames = new List<string>();
            List<string> folderNames = new List<string>();

            Console.WriteLine("Checking for new movies.. ");

            int movieCounter = 0;

            foreach (string directory in directories)
            {
                folderNames.Add(Path.GetFileName(directory));

                string[] files = Directory.GetFiles(directory);

                string ext = "";
                int index = 0;

                if (directory.Contains("["))
                    index = directory.IndexOf("[");

                if (directory.Contains("("))
                    index = directory.IndexOf("(");

                foreach(string file in files)
                {
                    if (file.Contains(".mp4"))
                    {
                        string fileName = Path.GetFileName(file);

                        fileNames.Add(fileName);

                        ext = ".mp4";

                        Console.WriteLine("Found Movie: " + fileName);
                    }

                    if (file.Contains(".mkv"))
                    {
                        string fileName = Path.GetFileName(file);

                        fileNames.Add(fileName);

                        ext = ".mkv";

                        Console.WriteLine("Found Movie: " + fileName);
                    }

                    else
                    {
                        Console.WriteLine("No Movies New Found.\n");
                    }
                }

                if (index > 0)
                {
                    movieNames[movieCounter] = directory.Substring(0, index-1);
                }

                Directory.Move(Windows.Settings.moviesFolder + @"\NewMovies\" + folderNames[movieCounter] + @"\" + fileNames[movieCounter], Windows.Settings.moviesFolder + @"\NewMovies\" + folderNames[movieCounter] + @"\" + Path.GetFileName(movieNames[movieCounter]) + ext);
                Directory.Move(Windows.Settings.moviesFolder + @"\NewMovies\" + folderNames[movieCounter], Windows.Settings.moviesFolder + @"\Movies\" + Path.GetFileName(movieNames[movieCounter]));

                Console.WriteLine("Added movie: " + Path.GetFileName(movieNames[movieCounter]));

                movieCounter++;
            }

            string[] oldDirectories = Directory.GetDirectories(Windows.Settings.moviesFolder + @"\Movies");

            List<string> oldMovieNames = new List<string>();

            foreach(string directory in oldDirectories)
            {
                string[] files = Directory.GetFiles(directory);

                string ext = "";

                foreach (string file in files)
                {
                    if (file.Contains(".mp4"))
                    {
                        ext = ".mp4";
                    }

                    if (file.Contains(".mkv"))
                    {
                        ext = ".mkv";
                    }
                }

                oldMovieNames.Add(Path.GetFileName(directory) + @"I---I" + Windows.Settings.moviesFolder + @"\Movies\" + Path.GetFileName(directory) + @"\" + Path.GetFileName(directory) + ext);
            }

            WriteAllLinesBetter(@"Movies\Movies.txt", oldMovieNames.ToArray());
        }

        #endregion

        #region ShowsData

        public static void GenerateInitialShows()
        {
            GenerateShowsList();

            Console.Write("\nShows Generated: ");

            string[] readText = File.ReadAllLines(@"Shows\ShowsInfo.txt");

            foreach (string line in readText)
            {
                if (generatedShows < 6)
                {
                    string[] separate = Regex.Split(line, "I---I");

                    NewShowElement.Add(ShowsWindow.Instance.ShowGrid, separate[0], separate[1], separate[2], separate[3], separate[4], separate[5]);

                    ShowsWindow.SetPosition();

                    generatedShows++;
                }
            }
        }

        public static void GenerateMoreShows()
        {
            string[] readText = File.ReadAllLines(@"Shows\ShowsInfo.txt");

            for (int i = 0; i < 3; i++)
            {
                if (generatedShows < readText.Length)
                {
                    string[] separate = Regex.Split(readText[generatedShows], "I---I");

                    NewShowElement.Add(ShowsWindow.Instance.ShowGrid, separate[0], separate[1], separate[2], separate[3], separate[4], separate[5]);

                    ShowsWindow.SetPosition();

                    generatedShows++;
                }
            }
        }

        public static void GenerateShowsList()
        {
            ShowsList = null;
            ShowsList = new List<AddShow>();

            var show = new AddShow();
            string[] showsPaths = Directory.GetDirectories(@"Shows");
            List<string> shows = new List<string>();

            foreach (var showItem in showsPaths)
            {
                show.ShowName = Path.GetDirectoryName(showItem);
                show.ShowFolderPath = showItem;

                ShowsList.Add(show);

                show = null;
                show = new AddShow();
            }
        }

        public static void GetShowInfo(string show)
        {
            OmdbClient omdb = new OmdbClient("a6c971b", false);

            SearchList searchList = omdb.GetSearchList(show);

            SearchItem result = searchList.SearchResults[0];

            Item item = omdb.GetItemByTitle(result.Title);

            var showVar = new AddShowInfo();

            showVar.Title = item.Title;
            showVar.Genre = item.Genre;
            showVar.Director = item.Director;
            showVar.Actors = item.Actors;
            showVar.IMDB = item.ImdbRating;
            showVar.Poster = item.Poster;

            ShowsInfoList.Add(showVar);
        }

        public static void WriteShowDataListToFile()
        {
            List<string> showDataString = new List<string>();

            foreach (AddShowInfo show in ShowsInfoList)
            {
                showDataString.Add(show.Title + "I---I" +
                                   show.Genre + "I---I" +
                                   show.Director + "I---I" +
                                   show.Actors + "I---I" +
                                   show.IMDB + "I---I" +
                                   show.Poster);
            }

            WriteAllLinesBetter(@"Shows\ShowsInfo.txt", showDataString.ToArray());

            Console.WriteLine("Shows List Updated Successfully");
        }

        #endregion
    }
}
