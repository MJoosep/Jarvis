#region Imports

using System;
using System.IO;
using System.Text;
using System.Windows.Controls;

#endregion
//clean

namespace Jarvis_2._0
{
    class ConsoleOutputManager : TextWriter
    {
        public override void Write(char value)
        {
            base.Write(value);

            MainWindow.outPut = MainWindow.outPut + value.ToString();

            if(MainWindow.consoleActive == true)
            {
                ConsoleWindow.consoleWindow.TestBox.Dispatcher.BeginInvoke(
                    (Action)(() => { ConsoleWindow.consoleWindow.TestBox.Text = MainWindow.outPut; }));
            }
        }

        public override Encoding Encoding
        {
            get { return System.Text.Encoding.UTF8; }
        }
    }
    //clean
}
