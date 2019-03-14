#region Imports

using System;
using System.Threading;
using System.Windows.Controls;

#endregion
//clean

namespace Jarvis_2._0
{
    public partial class ConsoleWindow : UserControl
    {
        #region Values

        public static ConsoleWindow consoleWindow;

        #endregion
        //clean

        public ConsoleWindow()
        {
            InitializeComponent();

            consoleWindow = this;

            TestBox.Text = MainWindow.outPut;

            ScrollView.ScrollToBottom();
        }
        //clean
    }
}
