#region Imports

using MaterialDesignThemes.Wpf;
using System;
using System.IO;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;

#endregion

namespace Jarvis_2._0
{
    public class NewActionElement
    {
        #region Values

        public static string newPath = "";
        public static string newPathName = "";

        #endregion

        #region NewActionExpander

        public static void Add(StackPanel Actions, AddAction ActionItem)
        {
            Expander Action = new Expander();
            Action.HorizontalAlignment = HorizontalAlignment.Stretch;
            Action.VerticalAlignment = VerticalAlignment.Center;

            BrushConverter brushConverter = new BrushConverter();

            Action.Background = (Brush)brushConverter.ConvertFrom("#FF151515");

            #region Header

            Grid headerGrid = new Grid();

            ColumnDefinition headerGridColumn1 = new ColumnDefinition();
            ColumnDefinition headerGridColumn2 = new ColumnDefinition();
            ColumnDefinition headerGridColumn3 = new ColumnDefinition();
            ColumnDefinition headerGridColumn4 = new ColumnDefinition();

            headerGridColumn1.Width = new GridLength(160);
            headerGridColumn2.Width = new GridLength(310);
            headerGridColumn3.Width = new GridLength(1, GridUnitType.Star);
            headerGridColumn4.Width = new GridLength(150);

            headerGrid.ColumnDefinitions.Add(headerGridColumn1);
            headerGrid.ColumnDefinitions.Add(headerGridColumn2);
            headerGrid.ColumnDefinitions.Add(headerGridColumn3);
            headerGrid.ColumnDefinitions.Add(headerGridColumn4);

            RowDefinition headerGridRow1 = new RowDefinition();

            headerGridRow1.Height = new GridLength(1, GridUnitType.Star);

            headerGrid.RowDefinitions.Add(headerGridRow1);

            StackPanel actionNameHeader = new StackPanel();
            Grid.SetColumn(actionNameHeader, 1);

            TextBlock actionNameLabel = new TextBlock();
            actionNameLabel.Opacity = 1;
            actionNameLabel.FontWeight = FontWeights.Normal;
            actionNameLabel.Foreground = new SolidColorBrush(Colors.LightGray);
            actionNameLabel.VerticalAlignment = VerticalAlignment.Center;
            actionNameLabel.HorizontalAlignment = HorizontalAlignment.Center;
            actionNameLabel.FontFamily = new FontFamily("Roboto");
            actionNameLabel.Text = "Action Name";

            TextBlock actionName = new TextBlock();
            actionName.FontSize = 15;
            actionName.Opacity = 0.7;
            actionName.FontWeight = FontWeights.SemiBold;
            actionName.Foreground = new SolidColorBrush(Colors.Gray);
            actionName.VerticalAlignment = VerticalAlignment.Center;
            actionName.HorizontalAlignment = HorizontalAlignment.Center;
            actionName.FontFamily = new FontFamily("Roboto");
            actionName.Text = ActionItem.ActionName;

            actionNameHeader.Children.Add(actionNameLabel);
            actionNameHeader.Children.Add(actionName);

            StackPanel actionTypeHeader = new StackPanel();
            Grid.SetColumn(actionTypeHeader, 2);

            TextBlock actionTypeLabel = new TextBlock();
            actionTypeLabel.Opacity = 1;
            actionTypeLabel.FontWeight = FontWeights.Normal;
            actionTypeLabel.Foreground = new SolidColorBrush(Colors.LightGray);
            actionTypeLabel.VerticalAlignment = VerticalAlignment.Center;
            actionTypeLabel.HorizontalAlignment = HorizontalAlignment.Center;
            actionTypeLabel.FontFamily = new FontFamily("Roboto");
            actionTypeLabel.Text = "Action Type";

            TextBlock actionType = new TextBlock();
            actionType.FontSize = 15;
            actionType.Opacity = 0.7;
            actionType.FontWeight = FontWeights.SemiBold;
            actionType.Foreground = new SolidColorBrush(Colors.Gray);
            actionType.VerticalAlignment = VerticalAlignment.Center;
            actionType.HorizontalAlignment = HorizontalAlignment.Center;
            actionType.FontFamily = new FontFamily("Roboto");
            actionType.Text = ActionItem.ActionType;

            actionTypeHeader.Children.Add(actionTypeLabel);
            actionTypeHeader.Children.Add(actionType);

            headerGrid.Children.Add(actionNameHeader);
            headerGrid.Children.Add(actionTypeHeader);

            Action.Header = headerGrid;

            #endregion

            #region ExpanderContent

            Grid dropDown = new Grid();

            dropDown.Background = (Brush)brushConverter.ConvertFrom("#FF232222");

            ColumnDefinition dropGridColumn1 = new ColumnDefinition();
            ColumnDefinition dropGridColumn2 = new ColumnDefinition();
            ColumnDefinition dropGridColumn3 = new ColumnDefinition();
            ColumnDefinition dropGridColumn4 = new ColumnDefinition();

            dropGridColumn1.Width = new GridLength(240);
            dropGridColumn2.Width = new GridLength(300);
            dropGridColumn3.Width = new GridLength(300);
            dropGridColumn4.Width = new GridLength(265);

            dropDown.ColumnDefinitions.Add(dropGridColumn1);
            dropDown.ColumnDefinitions.Add(dropGridColumn2);
            dropDown.ColumnDefinitions.Add(dropGridColumn3);
            dropDown.ColumnDefinitions.Add(dropGridColumn4);

            StackPanel PathTypePanel = new StackPanel();
            Grid.SetColumn(PathTypePanel, 0);
            PathTypePanel.Orientation = Orientation.Horizontal;

            Button pathButton = new Button();
            pathButton.Margin = new Thickness(30, 20, 10, 20);
            pathButton.HorizontalAlignment = HorizontalAlignment.Right;
            pathButton.VerticalAlignment = VerticalAlignment.Bottom;
            pathButton.Height = 50;
            pathButton.Width = 50;
            pathButton.Background = (Brush)brushConverter.ConvertFrom("#FFC7BA40");
            pathButton.BorderBrush = (Brush)brushConverter.ConvertFrom("#FF8F8129");

            string actionIdentifier = ActionItem.ActionName.Replace(" ", string.Empty);

            Style pathButtonResource = Application.Current.Resources["MaterialDesignFloatingActionButton"] as Style;
            pathButton.Style = pathButtonResource;
            ButtonProgressAssist.SetIsIndicatorVisible(pathButton, true);
            ButtonProgressAssist.SetValue(pathButton, 100);
            ButtonProgressAssist.SetIsIndeterminate(pathButton, true);
            ButtonProgressAssist.SetIndicatorForeground(pathButton, (Brush)brushConverter.ConvertFrom("#FF8BC34A"));
            pathButton.Name = actionIdentifier + "PathButton";

            object pathButtonRegister = Actions.FindName(pathButton.Name);

            if (pathButtonRegister != null)
                Actions.UnregisterName(pathButton.Name);

            Actions.RegisterName(pathButton.Name, pathButton);
            pathButton.Click += new RoutedEventHandler(ChangePath_Click);

            PackIcon path = new PackIcon();
            path.Kind = PackIconKind.File;
            pathButton.Content = path;

            ComboBox typeComboBox = new ComboBox();
            typeComboBox.Margin = new Thickness(20, 10, 0, 8);
            ThemeAssist.SetTheme(typeComboBox, BaseTheme.Dark);
            typeComboBox.HorizontalAlignment = HorizontalAlignment.Center;
            typeComboBox.Width = 100;
            typeComboBox.Name = actionIdentifier + "newTypeComboBox";

            object typeComboBoxRegister = Actions.FindName(typeComboBox.Name);

            if (typeComboBoxRegister != null)
                Actions.UnregisterName(typeComboBox.Name);

            Actions.RegisterName(typeComboBox.Name, typeComboBox);

            ComboBoxItem OpenFile = new ComboBoxItem();
            ComboBoxItem OpenWebsite = new ComboBoxItem();
            ComboBoxItem CloseFile = new ComboBoxItem();
            ComboBoxItem Type = new ComboBoxItem();

            OpenFile.IsSelected = true;
            OpenFile.Content = "Open File";
            OpenWebsite.Content = "Open Website";
            CloseFile.Content = "Close File";
            Type.Content = "Type";

            typeComboBox.Items.Add(OpenFile);
            typeComboBox.Items.Add(OpenWebsite);
            typeComboBox.Items.Add(CloseFile);
            typeComboBox.Items.Add(Type);

            typeComboBox.SelectionChanged += new SelectionChangedEventHandler(ComboBox_SelectionChanged);

            PathTypePanel.Children.Add(pathButton);
            PathTypePanel.Children.Add(typeComboBox);

            TextBox actionNameInput = new TextBox();
            Grid.SetColumn(actionNameInput, 1);
            actionNameInput.Margin = new Thickness(0, 10, 30, 10);
            actionNameInput.VerticalContentAlignment = VerticalAlignment.Center;

            Style actionInputResource = Application.Current.Resources["MaterialDesignFilledTextFieldTextBox"] as Style;
            actionNameInput.Style = actionInputResource;
            actionNameInput.VerticalAlignment = VerticalAlignment.Center;
            actionNameInput.AcceptsReturn = true;
            actionNameInput.TextWrapping = TextWrapping.Wrap;
            actionNameInput.MaxWidth = 400;
            actionNameInput.Name = actionIdentifier + "newNameTextBox";

            object actionNameInputRegister = Actions.FindName(actionNameInput.Name);

            if (actionNameInputRegister != null)
                Actions.UnregisterName(actionNameInput.Name);

            Actions.RegisterName(actionNameInput.Name, actionNameInput);
            HintAssist.SetHint(actionNameInput, "New Action Name");

            TextBox actionTypeInput = new TextBox();
            Grid.SetColumn(actionTypeInput, 2);
            actionTypeInput.Margin = new Thickness(0, 10, 30, 10);
            actionTypeInput.VerticalContentAlignment = VerticalAlignment.Center;

            actionTypeInput.Style = actionInputResource;
            actionTypeInput.VerticalAlignment = VerticalAlignment.Center;
            actionTypeInput.AcceptsReturn = true;
            actionTypeInput.TextWrapping = TextWrapping.Wrap;
            actionTypeInput.IsEnabled = false;
            actionTypeInput.MaxWidth = 400;
            actionTypeInput.Name = actionIdentifier + "newMessageTextBox";
            actionTypeInput.MaxLines = 1;
            actionTypeInput.TextWrapping = TextWrapping.NoWrap;

            object actionTypeInputRegister = Actions.FindName(actionTypeInput.Name);

            if (actionTypeInputRegister != null)
                Actions.UnregisterName(actionTypeInput.Name);

            Actions.RegisterName(actionTypeInput.Name, actionTypeInput);
            HintAssist.SetHint(actionTypeInput, "Message(Optional)");

            StackPanel SaveDeletePanel = new StackPanel();
            Grid.SetColumn(SaveDeletePanel, 3);
            SaveDeletePanel.Orientation = Orientation.Horizontal;
            SaveDeletePanel.Margin = new Thickness(0, 0, 80, 0);
            SaveDeletePanel.HorizontalAlignment = HorizontalAlignment.Right;

            Button saveButton = new Button();
            saveButton.Margin = new Thickness(30, 20, 20, 20);
            saveButton.HorizontalAlignment = HorizontalAlignment.Right;
            saveButton.VerticalAlignment = VerticalAlignment.Bottom;
            saveButton.Height = 50;
            saveButton.Width = 50;

            Style saveButtonResource = Application.Current.Resources["MaterialDesignFloatingActionButton"] as Style;
            saveButton.Style = saveButtonResource;
            ButtonProgressAssist.SetIsIndicatorVisible(saveButton, true);
            ButtonProgressAssist.SetValue(saveButton, 100);
            ButtonProgressAssist.SetIsIndeterminate(saveButton, true);

            saveButton.Name = actionIdentifier + "SaveButton";
            saveButton.Click += new RoutedEventHandler(SaveAction_Click);

            PackIcon floppy = new PackIcon();
            floppy.Kind = PackIconKind.Floppy;
            saveButton.Content = floppy;

            Button deleteButton = new Button();
            deleteButton.Margin = new Thickness(5, 20, 40, 20);
            deleteButton.HorizontalAlignment = HorizontalAlignment.Right;
            deleteButton.VerticalAlignment = VerticalAlignment.Bottom;
            deleteButton.Height = 50;
            deleteButton.Width = 50;
            deleteButton.Background = (Brush)brushConverter.ConvertFrom("#FF810000");
            deleteButton.BorderBrush = (Brush)brushConverter.ConvertFrom("#FF781818");

            Style deleteButtonResource = Application.Current.Resources["MaterialDesignFloatingActionButton"] as Style;
            deleteButton.Style = deleteButtonResource;
            ButtonProgressAssist.SetIsIndicatorVisible(deleteButton, true);
            ButtonProgressAssist.SetValue(deleteButton, 100);
            ButtonProgressAssist.SetIsIndeterminate(deleteButton, true);
            ButtonProgressAssist.SetIndicatorForeground(deleteButton, (Brush)brushConverter.ConvertFrom("#FF810000"));
            deleteButton.Name = actionIdentifier + "DeleteButton";
            deleteButton.Click += new RoutedEventHandler(DeleteAction_Click);

            PackIcon delete = new PackIcon();
            delete.Kind = PackIconKind.Delete;
            deleteButton.Content = delete;

            SaveDeletePanel.Children.Add(saveButton);
            SaveDeletePanel.Children.Add(deleteButton);

            dropDown.Children.Add(PathTypePanel);
            dropDown.Children.Add(actionNameInput);
            dropDown.Children.Add(actionTypeInput);
            dropDown.Children.Add(SaveDeletePanel);

            Action.Content = dropDown;

            #endregion

            #region Border

            Border groupSeparator = new Border();

            Brush bg = Application.Current.Resources["MaterialDesignDivider"] as Brush;
            groupSeparator.Background = bg;
            groupSeparator.Height = 1;
            groupSeparator.HorizontalAlignment = HorizontalAlignment.Stretch;
            groupSeparator.SnapsToDevicePixels = true;

            #endregion

            Actions.Children.Add(Action);
            Actions.Children.Add(groupSeparator);
        }

        #endregion

        #region EventHandlers

        public static void SaveAction_Click(object sender, RoutedEventArgs e)
        {
            Button pressed = e.Source as Button;

            string actionName = pressed.Name.Remove(pressed.Name.Length - 10);

            TextBox newName = ActionsWindow.Instance.Actions.FindName(actionName + "newNameTextBox") as TextBox;
            TextBox newMessage = ActionsWindow.Instance.Actions.FindName(actionName + "newMessageTextBox") as TextBox;
            ComboBox newType = ActionsWindow.Instance.Actions.FindName(actionName + "newTypeComboBox") as ComboBox;

            string newNameString = newName.Text;
            string newMessageString = newMessage.Text;
            string newTypeString = newType.SelectionBoxItem.ToString();

            if (newNameString == "")
            {
                ActionsWindow.Instance.MissingDialogText.Text = "Missing New Name";

                ActionsWindow.Instance.MissingDialog.IsOpen = true;

                return;
            }

            if (newMessageString == "" && newTypeString == "Type")
            {
                ActionsWindow.Instance.MissingDialogText.Text = "Missing Message";

                ActionsWindow.Instance.MissingDialog.IsOpen = true;

                return;
            }

            if (newMessageString == "" && newTypeString == "Open Website")
            {
                ActionsWindow.Instance.MissingDialogText.Text = "Missing Website";

                ActionsWindow.Instance.MissingDialog.IsOpen = true;

                return;
            }

            if (newMessageString != "" && newTypeString == "Open Website")
            {
                newMessageString = "No Message";
                newPathName = "Website";
                newPath = newMessage.Text;
            }

            if (newPath == "" && newTypeString == "Open File" || newPath == "" && newTypeString == "Close File")
            {
                ActionsWindow.Instance.MissingDialogText.Text = "Missing Path";

                ActionsWindow.Instance.MissingDialog.IsOpen = true;

                return;
            }

            DataManagement.RewriteSpecificActionsSaveData(actionName, newNameString, newMessageString, newTypeString, newPathName, newPath);

            ActionsWindow.Instance.Actions.Children.Clear();

            DataManagement.GenerateActions();

            newPath = "";
            newPathName = "";
        }

        public static void DeleteAction_Click(object sender, RoutedEventArgs e)
        {
            Button pressed = e.Source as Button;

            string actionName = pressed.Name.Remove(pressed.Name.Length - 12);

            DataManagement.DeleteAction(actionName);

            ActionsWindow.Instance.Actions.Children.Clear();

            ActionsWindow.Instance.UpdateLayout();

            DataManagement.GenerateActions();

            DataManagement.DeleteSpecificSaveData(actionName);

            DataManagement.UpdateNumberOfActions();
        }

        public static void ChangePath_Click(object sender, RoutedEventArgs e)
        {
            Button pressed = e.Source as Button;

            if (pressed.Name.Contains("PathButton"))
            {
                string actionName = pressed.Name.Remove(pressed.Name.Length - 10);
                
                newPath = DataManagement.ChooseNewPath();
                newPathName = Path.GetFileNameWithoutExtension(newPath);
            }
        }

        public static void ComboBox_SelectionChanged(object sender, RoutedEventArgs e)
        {
            ComboBox ActionType = e.Source as ComboBox;

            if (ActionType != null)
            {
                string actionName = ActionType.Name.Remove(ActionType.Name.Length - 15);

                TextBox newMessage = ActionsWindow.Instance.Actions.FindName(actionName + "newMessageTextBox") as TextBox;

                Button DialogPathButton = ActionsWindow.Instance.Actions.FindName(actionName + "PathButton") as Button;
                
                switch (ActionType.SelectedItem.ToString())
                {
                    case "System.Windows.Controls.ComboBoxItem: Open File":
                        newMessage.IsEnabled = false;
                        DialogPathButton.IsEnabled = true;
                        break;
                    case "System.Windows.Controls.ComboBoxItem: Open Website":
                        newMessage.IsEnabled = true;
                        DialogPathButton.IsEnabled = false;
                        HintAssist.SetHint(newMessage, "Website Url");
                        break;
                    case "System.Windows.Controls.ComboBoxItem: Close File":
                        newMessage.IsEnabled = false;
                        DialogPathButton.IsEnabled = true;
                        break;
                    case "System.Windows.Controls.ComboBoxItem: Type":
                        newMessage.IsEnabled = true;
                        DialogPathButton.IsEnabled = false;
                        HintAssist.SetHint(newMessage, "Message");
                        break;
                }
            }
        }

        #endregion
    }
}
