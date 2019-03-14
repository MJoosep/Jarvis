#region Imports

using MaterialDesignThemes.Wpf;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Media;

#endregion
//clean

namespace Jarvis_2._0
{
    public class NewGroupElement
    {
        #region NewGroupExpander

        public static void Add(StackPanel Groups, AddGroup GroupItem)
        {
            Expander Group = new Expander();
            Group.HorizontalAlignment = HorizontalAlignment.Stretch;
            Group.VerticalAlignment = VerticalAlignment.Center;

            string groupIdentifier = GroupItem.GroupName.Replace(" ", string.Empty);

            BrushConverter brushConverter = new BrushConverter();

            Group.Background = (Brush)brushConverter.ConvertFrom("#FF151515");

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

            StackPanel groupNameHeader = new StackPanel();
            Grid.SetColumn(groupNameHeader, 1);

            TextBlock groupNameLabel = new TextBlock();
            groupNameLabel.Opacity = 1;
            groupNameLabel.FontWeight = FontWeights.Normal;
            groupNameLabel.Foreground = new SolidColorBrush(Colors.LightGray);
            groupNameLabel.VerticalAlignment = VerticalAlignment.Center;
            groupNameLabel.HorizontalAlignment = HorizontalAlignment.Center;
            groupNameLabel.FontFamily = new FontFamily("Roboto");
            groupNameLabel.Text = "Group Name";

            TextBlock groupName = new TextBlock();
            groupName.FontSize = 15;
            groupName.Opacity = 0.7;
            groupName.FontWeight = FontWeights.SemiBold;
            groupName.Foreground = new SolidColorBrush(Colors.Gray);
            groupName.VerticalAlignment = VerticalAlignment.Center;
            groupName.HorizontalAlignment = HorizontalAlignment.Center;
            groupName.FontFamily = new FontFamily("Roboto");
            groupName.Text = GroupItem.GroupName;

            groupNameHeader.Children.Add(groupNameLabel);
            groupNameHeader.Children.Add(groupName);

            StackPanel groupCommandHeader = new StackPanel();
            Grid.SetColumn(groupCommandHeader, 2);

            TextBlock groupCommandLabel = new TextBlock();
            groupCommandLabel.Opacity = 1;
            groupCommandLabel.FontWeight = FontWeights.Normal;
            groupCommandLabel.Foreground = new SolidColorBrush(Colors.LightGray);
            groupCommandLabel.VerticalAlignment = VerticalAlignment.Center;
            groupCommandLabel.HorizontalAlignment = HorizontalAlignment.Center;
            groupCommandLabel.FontFamily = new FontFamily("Roboto");
            groupCommandLabel.Text = "Command";

            TextBlock groupCommand = new TextBlock();
            groupCommand.FontSize = 15;
            groupCommand.Opacity = 0.7;
            groupCommand.FontWeight = FontWeights.SemiBold;
            groupCommand.Foreground = new SolidColorBrush(Colors.Gray);
            groupCommand.VerticalAlignment = VerticalAlignment.Center;
            groupCommand.HorizontalAlignment = HorizontalAlignment.Center;
            groupCommand.FontFamily = new FontFamily("Roboto");
            groupCommand.Text = GroupItem.Command;

            groupCommandHeader.Children.Add(groupCommandLabel);
            groupCommandHeader.Children.Add(groupCommand);

            ToggleButton groupActiveToggle = new ToggleButton();

            Style resource = Application.Current.Resources["MaterialDesignSwitchToggleButton"] as Style;

            Grid.SetColumn(groupActiveToggle, 0);
            groupActiveToggle.Style = resource;
            groupActiveToggle.ToolTip = "Toggle Group Active Status";
            groupActiveToggle.HorizontalAlignment = HorizontalAlignment.Left;
            groupActiveToggle.VerticalAlignment = VerticalAlignment.Center;
            groupActiveToggle.Name = groupIdentifier + "toggle";
            groupActiveToggle.Click += new RoutedEventHandler(Toggle_Click);

            object groupActiveToggleRegister = Groups.FindName(groupActiveToggle.Name);

            if (groupActiveToggleRegister != null)
                Groups.UnregisterName(groupActiveToggle.Name);

            Groups.RegisterName(groupActiveToggle.Name, groupActiveToggle);

            headerGrid.Children.Add(groupNameHeader);
            headerGrid.Children.Add(groupCommandHeader);
            headerGrid.Children.Add(groupActiveToggle);

            Group.Header = headerGrid;

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
            dropGridColumn4.Width = new GridLength(225);

            dropDown.ColumnDefinitions.Add(dropGridColumn1);
            dropDown.ColumnDefinitions.Add(dropGridColumn2);
            dropDown.ColumnDefinitions.Add(dropGridColumn3);
            dropDown.ColumnDefinitions.Add(dropGridColumn4);

            StackPanel RunActionsPanel = new StackPanel();
            Grid.SetColumn(RunActionsPanel, 0);
            RunActionsPanel.Orientation = Orientation.Horizontal;

            Button RunGroup = new Button();
            RunGroup.Height = 50;
            RunGroup.Width = 50;
            RunGroup.Margin = new Thickness(20);

            PackIcon RunIcon = new PackIcon();
            RunIcon.Kind = PackIconKind.Run;
            RunIcon.Height = 24;
            RunIcon.Width = 24;
            RunIcon.HorizontalAlignment = HorizontalAlignment.Center;
            RunIcon.VerticalAlignment = VerticalAlignment.Center;
            RunGroup.Click += new RoutedEventHandler(Run_Click);
            RunGroup.Name = groupIdentifier + "run";

            object RunGroupRegister = Groups.FindName(RunGroup.Name);

            if (RunGroupRegister != null)
                Groups.UnregisterName(RunGroup.Name);

            Groups.RegisterName(RunGroup.Name, RunGroup);

            RunGroup.Content = RunIcon;

            Badged actionsCountBadge = new Badged();
            actionsCountBadge.VerticalAlignment = VerticalAlignment.Center;
            actionsCountBadge.HorizontalAlignment = HorizontalAlignment.Left;
            actionsCountBadge.Margin = new Thickness(20);
            actionsCountBadge.Badge = "0";
            actionsCountBadge.BadgeBackground = (Brush)brushConverter.ConvertFrom("#FF8BC34A");
            actionsCountBadge.Name = groupIdentifier + "badge";

            object actionsCountBadgeRegister = Groups.FindName(actionsCountBadge.Name);

            if (actionsCountBadgeRegister != null)
                Groups.UnregisterName(actionsCountBadge.Name);

            Groups.RegisterName(actionsCountBadge.Name, actionsCountBadge);

            Button addActions = new Button();
            addActions.Height = 40;
            addActions.Background = (Brush)brushConverter.ConvertFrom("#FF1D1D1D");
            addActions.BorderBrush = (Brush)brushConverter.ConvertFrom("#FF1A1A1A");
            addActions.Name = groupIdentifier + "ActionsButton";
            addActions.Content = "ACTIONS";
            addActions.Click += new RoutedEventHandler(Actions_Click);

            actionsCountBadge.Content = addActions;

            RunActionsPanel.Children.Add(RunGroup);
            RunActionsPanel.Children.Add(actionsCountBadge);

            TextBox groupNameInput = new TextBox();
            Grid.SetColumn(groupNameInput, 1);
            groupNameInput.Margin = new Thickness(0, 10, 30, 10);
            groupNameInput.VerticalContentAlignment = VerticalAlignment.Center;

            Style groupInputResource = Application.Current.Resources["MaterialDesignFilledTextFieldTextBox"] as Style;
            groupNameInput.Style = groupInputResource;
            groupNameInput.VerticalAlignment = VerticalAlignment.Center;
            groupNameInput.AcceptsReturn = true;
            groupNameInput.TextWrapping = TextWrapping.Wrap;
            groupNameInput.MaxWidth = 400;
            groupNameInput.Name = groupIdentifier + "newNameTextBox";

            object groupNameInputRegister = Groups.FindName(groupNameInput.Name);

            if (groupNameInputRegister != null)
                Groups.UnregisterName(groupNameInput.Name);

            Groups.RegisterName(groupNameInput.Name, groupNameInput);
            HintAssist.SetHint(groupNameInput, "New Group Name");

            TextBox groupCommandInput = new TextBox();
            Grid.SetColumn(groupCommandInput, 2);
            groupCommandInput.Margin = new Thickness(0, 10, 30, 10);
            groupCommandInput.VerticalContentAlignment = VerticalAlignment.Center;

            groupCommandInput.Style = groupInputResource;
            groupCommandInput.VerticalAlignment = VerticalAlignment.Center;
            groupCommandInput.AcceptsReturn = true;
            groupCommandInput.TextWrapping = TextWrapping.Wrap;
            groupCommandInput.MaxWidth = 400;
            groupCommandInput.Name = groupIdentifier + "newCommandTextBox";

            object groupCommandInputRegister = Groups.FindName(groupCommandInput.Name);

            if (groupCommandInputRegister != null)
                Groups.UnregisterName(groupCommandInput.Name);

            Groups.RegisterName(groupCommandInput.Name, groupCommandInput);
            HintAssist.SetHint(groupCommandInput, "New Command");

            StackPanel SaveDeletePanel = new StackPanel();
            Grid.SetColumn(SaveDeletePanel, 3);
            SaveDeletePanel.Orientation = Orientation.Horizontal;
            SaveDeletePanel.Margin = new Thickness(0, 0, 60, 0);
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

            saveButton.Name = groupIdentifier + "SaveButton";
            saveButton.Click += new RoutedEventHandler(SaveGroup_Click);

            PackIcon floppy = new PackIcon();
            floppy.Kind = PackIconKind.Floppy;
            saveButton.Content = floppy;

            Button deleteButton = new Button();
            deleteButton.Margin = new Thickness(5, 20, 30, 20);
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
            deleteButton.Name = groupIdentifier + "DeleteButton";
            deleteButton.Click += new RoutedEventHandler(DeleteGroup_Click);

            PackIcon delete = new PackIcon();
            delete.Kind = PackIconKind.Delete;
            deleteButton.Content = delete;

            SaveDeletePanel.Children.Add(saveButton);
            SaveDeletePanel.Children.Add(deleteButton);

            dropDown.Children.Add(RunActionsPanel);
            dropDown.Children.Add(groupNameInput);
            dropDown.Children.Add(groupCommandInput);
            dropDown.Children.Add(SaveDeletePanel);

            Group.Content = dropDown;

            #endregion

            #region Border

            Border groupSeparator = new Border();

            Brush bg = Application.Current.Resources["MaterialDesignDivider"] as Brush;
            groupSeparator.Background = bg;
            groupSeparator.Height = 1;
            groupSeparator.HorizontalAlignment = HorizontalAlignment.Stretch;
            groupSeparator.SnapsToDevicePixels = true;

            #endregion

            Groups.Children.Add(Group);
            Groups.Children.Add(groupSeparator);
        }

        #endregion
        //clean

        #region EventHandlers

        public static void SaveGroup_Click(object sender, RoutedEventArgs e)
        {
            Button pressed = e.Source as Button;

            string groupName = pressed.Name.Remove(pressed.Name.Length - 10);

            TextBox newName = GroupsWindow.Instance.Groups.FindName(groupName + "newNameTextBox") as TextBox;
            TextBox newCommand = GroupsWindow.Instance.Groups.FindName(groupName + "newCommandTextBox") as TextBox;

            string newNameString = newName.Text;
            string newCommandString = newCommand.Text;

            if (newNameString == "")
            {
                GroupsWindow.Instance.MissingDialogText.Text = "Missing New Name";

                GroupsWindow.Instance.MissingDialog.IsOpen = true;

                return;
            }

            if (newCommandString == "")
            {
                GroupsWindow.Instance.MissingDialogText.Text = "Missing New Command";

                GroupsWindow.Instance.MissingDialog.IsOpen = true;

                return;
            }

            DataManagement.RewriteSpecificGroupSaveData(groupName, newNameString, newCommandString, "", "ChangeName");

            GroupsWindow.Instance.Groups.Children.Clear();

            DataManagement.GenerateGroups();

            DataManagement.SetBadgesAndToggles();
        }

        public static void DeleteGroup_Click(object sender, RoutedEventArgs e)
        {
            Button pressed = e.Source as Button;

            string groupName = pressed.Name.Remove(pressed.Name.Length - 12);

            DataManagement.DeleteGroup(groupName);

            GroupsWindow.Instance.Groups.Children.Clear();

            DataManagement.GenerateGroups();

            DataManagement.SetBadgesAndToggles();
        }

        public static void Toggle_Click(object sender, RoutedEventArgs e)
        {
            ToggleButton pressed = e.Source as ToggleButton;

            if(pressed != null)
            {
                string groupName = pressed.Name.Remove(pressed.Name.Length - 6);

                if(pressed.IsChecked == false)
                    DataManagement.RewriteSpecificGroupSaveData(groupName, "", "", "N", "ToggleStatus");

                if(pressed.IsChecked == true)
                    DataManagement.RewriteSpecificGroupSaveData(groupName, "", "", "Y", "ToggleStatus");
            }
        }

        public static void Actions_Click(object sender, RoutedEventArgs e)
        {
            Button pressed = e.Source as Button;

            GroupsWindow.Instance.saveGroupActions = e.Source as Button;

            string buttonName = pressed.Name;

            if (buttonName.Contains("ActionsButton"))
            {
                string groupName = buttonName.Remove(buttonName.Length - 13);

                DataManagement.GenerateActionsCheckList(groupName);

                GroupsWindow.Instance.ActionsListDialog.IsOpen = true;
            }
        }

        public static void Run_Click(object sender, RoutedEventArgs e)
        {
            Button pressed = e.Source as Button;

            string groupName = pressed.Name.Remove(pressed.Name.Length - 3);

            DataManagement.SetSelectedActions(groupName);
        }

        #endregion
        //clean
    }
}
