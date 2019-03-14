#region Imports

using MaterialDesignThemes.Wpf;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Windows;
using System.Windows.Controls;

#endregion
//clean

namespace Jarvis_2._0
{
    public partial class ActionsWindow : UserControl
    {
        #region Values

        public static ActionsWindow Instance;

        public string newFileCache = "";

        public string newActionTypeCache = "Open File";

        #endregion
        //clean

        public ActionsWindow()
        {
            InitializeComponent();

            Instance = this;

            DataManagement.GenerateActions();
        }

        #region EventHandlers

        private void Add_Action(object sender, RoutedEventArgs e)
        {
            if (newActionTypeCache == "Open File" || newActionTypeCache == "Close File")
            {
                if (newFileCache == "")
                {
                    MissingDialogText.Text = "Missing File To Open/Close";

                    MissingDialog.IsOpen = true;
                }

                if (newActionNameTextBox.Text == "")
                {
                    MissingDialogText.Text = "Missing Action Name";

                    MissingDialog.IsOpen = true;
                }

                if (newFileCache != "" && newActionNameTextBox.Text != "")
                {
                    var action = new AddAction();

                    action.ActionName = newActionNameTextBox.Text;
                    action.ActionType = newActionTypeCache;
                    action.ActionMessage = "No Message";
                    action.ActionFileName = System.IO.Path.GetFileNameWithoutExtension(newFileCache);
                    action.ActionPathName = newFileCache;

                    DataManagement.ActionsList.Add(action);

                    DataManagement.SaveActions();

                    Instance.Actions.Children.Clear();

                    DataManagement.GenerateActions();

                    AddActionDialogHost.IsOpen = false;

                    DataManagement.UpdateNumberOfActions();

                    newActionNameTextBox.Text = "";
                    newMessageTextBox.Text = "";
                    newFileCache = "";
                }
            }

            if (newActionTypeCache == "Type")
            {
                if (newMessageTextBox.Text == "")
                {
                    MissingDialogText.Text = "Missing Message To Push";

                    MissingDialog.IsOpen = true;
                }

                if (newActionNameTextBox.Text == "")
                {
                    MissingDialogText.Text = "Missing Action Name";

                    MissingDialog.IsOpen = true;
                }

                if (newMessageTextBox.Text != "")
                {
                    var action = new AddAction();

                    action.ActionName = newActionNameTextBox.Text;
                    action.ActionType = newActionTypeCache;
                    action.ActionMessage = newMessageTextBox.Text;
                    action.ActionFileName = "No File";
                    action.ActionPathName = "No Path";

                    DataManagement.ActionsList.Add(action);

                    DataManagement.SaveActions();

                    Instance.Actions.Children.Clear();

                    DataManagement.GenerateActions();

                    AddActionDialogHost.IsOpen = false;

                    DataManagement.UpdateNumberOfActions();

                    newActionNameTextBox.Text = "";
                    newMessageTextBox.Text = "";
                    newFileCache = "";
                }
            }

            if (newActionTypeCache == "Open Website")
            {
                if (newMessageTextBox.Text == "")
                {
                    MissingDialogText.Text = "Missing Website To Open";

                    MissingDialog.IsOpen = true;
                }

                if (newActionNameTextBox.Text == "")
                {
                    MissingDialogText.Text = "Missing Action Name";

                    MissingDialog.IsOpen = true;
                }

                if (newMessageTextBox.Text != "" && newActionNameTextBox.Text != "")
                {
                    var action = new AddAction();

                    action.ActionName = newActionNameTextBox.Text;
                    action.ActionType = newActionTypeCache;
                    action.ActionMessage = "No Message";
                    action.ActionFileName = "Website";
                    action.ActionPathName = newMessageTextBox.Text;

                    DataManagement.ActionsList.Add(action);

                    DataManagement.SaveActions();

                    Instance.Actions.Children.Clear();

                    DataManagement.GenerateActions();

                    AddActionDialogHost.IsOpen = false;

                    DataManagement.UpdateNumberOfActions();

                    newActionNameTextBox.Text = "";
                    newMessageTextBox.Text = "";
                    newFileCache = "";
                }
            }
        }
        //Improvable

        private void ComboBox_SelectionChanged(object sender, RoutedEventArgs e)
        {
            ComboBox ActionType = e.Source as ComboBox;

            if (ActionType != null)
            {
                switch (ActionType.SelectedItem.ToString())
                {
                    case "System.Windows.Controls.ComboBoxItem: Open File":
                        newActionTypeCache = "Open File";
                        DialogPathButton.IsEnabled = true;
                        newMessageTextBox.IsEnabled = false;
                        
                        break;
                    case "System.Windows.Controls.ComboBoxItem: Open Website":
                        newActionTypeCache = "Open Website";
                        DialogPathButton.IsEnabled = false;
                        newMessageTextBox.IsEnabled = true;
                        HintAssist.SetHint(newMessageTextBox, "Website Url");
                        break;
                    case "System.Windows.Controls.ComboBoxItem: Close File":
                        newActionTypeCache = "Close File";
                        DialogPathButton.IsEnabled = true;
                        newMessageTextBox.IsEnabled = false;
                        break;
                    case "System.Windows.Controls.ComboBoxItem: Type":
                        newActionTypeCache = "Type";
                        DialogPathButton.IsEnabled = false;
                        newMessageTextBox.IsEnabled = true;
                        HintAssist.SetHint(newMessageTextBox, "Message");
                        break;
                }
            }
        }
        //clean

        private void DialogPathButton_Click(object sender, RoutedEventArgs e)
        {
            Microsoft.Win32.OpenFileDialog chooseNewFile = new Microsoft.Win32.OpenFileDialog();
            
            chooseNewFile.DefaultExt = ".exe";
            chooseNewFile.Filter = "Exe Files (.exe)|*.exe|All Files (*.*)|*.*";
            
            Nullable<bool> result = chooseNewFile.ShowDialog();

            if (result == true)
            {
                string filename = chooseNewFile.FileName;

                newFileCache = chooseNewFile.FileName;
            }
        }
        //clean

        private void Open_New_Action_Dialog(object sender, EventArgs e)
        {
            AddActionDialogHost.IsOpen = true;
        }
        //clean

        #endregion
        //almost clean
    }
}
