#region Imports

using System;
using System.Diagnostics;
using System.IO;
using System.Text.RegularExpressions;

#endregion

namespace Jarvis_2._0
{
    public class RunActions
    {
        public RunActions()
        {
            
        }

        public static void RunActionsFunction(string [] Actions)
        {
            foreach (string line in Actions)
            {
                string[] separate = Regex.Split(line, "I---I");

                if (separate[1] == "Open File")
                {
                    ProcessStartInfo start = new ProcessStartInfo();

                    start.Arguments = "";
                    start.FileName = separate[4];
                    start.WorkingDirectory = Path.GetDirectoryName(separate[4]);
                    start.WindowStyle = ProcessWindowStyle.Maximized;
                    start.CreateNoWindow = true;

                    Process.Start(start);

                    Console.WriteLine("Opening File: " + separate[3]);
                }

                if (separate[1] == "Open Website")
                {
                    ProcessStartInfo start = new ProcessStartInfo();

                    start.Arguments = "";
                    start.FileName = separate[4];
                    start.WorkingDirectory = Path.GetDirectoryName(separate[4]);
                    start.WindowStyle = ProcessWindowStyle.Maximized;
                    start.CreateNoWindow = true;

                    Process.Start(start);

                    Console.WriteLine("Opening Website: " + separate[4]);
                }

                if (separate[1] == "Close File")
                { 
                    try
                    {
                        foreach (var process in Process.GetProcessesByName(separate[3]))
                        {
                            process.Kill();
                        }
                    }
                    catch (System.NullReferenceException) { }

                    Console.WriteLine("Closed: " + separate[3]);
                }

                if (separate[1] == "Type")
                {
                    DiscordBot db = new DiscordBot(separate[2]);

                    Console.WriteLine("Message Sent To Discord: " + separate[2]);
                }
            }
        }
    }
}
