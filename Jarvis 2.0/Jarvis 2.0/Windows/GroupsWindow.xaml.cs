#region Imports

using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;
using System.Windows;
using System.Windows.Controls;
using MaterialDesignThemes.Wpf;

#endregion
//clean

namespace Jarvis_2._0
{
    public partial class GroupsWindow : UserControl
    {
        #region Values

        public Button saveGroupActions;

        public static GroupsWindow Instance;

        #endregion
        //clean

        public GroupsWindow()
        {
            InitializeComponent();

            Instance = this;

            DataManagement.GenerateGroups();

            DataManagement.SetBadgesAndToggles();
        }

        #region EventHandlers

        private void Add_Group(object sender, RoutedEventArgs e)
        {
            if(newGroupNameTextBox.Text == "")
            {
                MissingDialogText.Text = "Missing Name";

                MissingDialog.IsOpen = true;
            }
            if (newGroupCommandTextBox.Text == "")
            {
                MissingDialogText.Text = "Missing Command";

                MissingDialog.IsOpen = true;
            }
            if (newGroupCommandTextBox.Text != "" && newGroupNameTextBox.Text != "")
            {
                var group = new AddGroup();
                group.GroupName = newGroupNameTextBox.Text;
                group.Command = newGroupCommandTextBox.Text;
                group.ActionsData = "0";
                group.Active = "Y";
                DataManagement.GroupsList.Add(group);

                DataManagement.SaveGroups();

                Instance.Groups.Children.Clear();

                DataManagement.GenerateGroups();

                DataManagement.UpdateNumberOfActions();

                DataManagement.SetBadgesAndToggles();
            }
        }

        private void Open_New_Group_Dialog(object sender, EventArgs e)
        {
            newGroupNameTextBox.Text = "";
            newGroupCommandTextBox.Text = "";

            AddGroupDialogHost.IsOpen = true;
        }

        private void Save_CheckedActions(object sender, RoutedEventArgs e)
        {
            string groupName = saveGroupActions.Name.Remove(saveGroupActions.Name.Length - 13);

            int numberOfActiveActions = 0;

            string saveData = "";

            foreach (CheckBox action in ActionsCheckList.Children)
            {
                if(action.IsChecked == true)
                {
                    saveData = saveData + "1";
                }
                if (action.IsChecked == false)
                {
                    saveData = saveData + "0";
                }
            }

            string line = string.Empty;
            List<string> lines = new List<string>();

            if (File.Exists(@"Groups\Groups.txt"))
            {
                System.IO.StreamReader file = new System.IO.StreamReader(@"Groups\Groups.txt");
                while ((line = file.ReadLine()) != null)
                {
                    if (line.Replace(" ", string.Empty).Contains(groupName))
                    {
                        string[] separate = Regex.Split(line, "I---I");

                        line = separate[0] + "I---I" + separate[1] + "I---I" + saveData + "I---I" + separate[3];
                    }
                    lines.Add(line);
                }
                file.Close();
            }

            foreach(char action in saveData)
            {
                if (action == '1')
                    numberOfActiveActions++;
            }

            Badged badge = saveGroupActions.Parent as Badged;
            badge.Badge = numberOfActiveActions;

            File.WriteAllLines(@"Groups\Groups.txt", lines.ToList());

            saveGroupActions = null;

            ActionsListDialog.IsOpen = false;
        }

        #endregion
        //clean
    }
}
