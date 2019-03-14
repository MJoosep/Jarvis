#region Imports

using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Text.RegularExpressions;
using WebSocketSharp;

#endregion
//clean

namespace Jarvis_2._0
{
    public class WebHandler
    {
        #region Values

        public static string[] title;
        public static string[] message;

        public static int highestMessage = 0;

        public static string secret = "8gczmkq7r4omu5h1n1iatv7fktq6utshb8ohsv7v4neaw8r5zj62zsv8ta8d";
        public static string deviceID = "cb8wmxi7jhkacgd1wvzuhnzj9b1s1r8eyjbt66sm";

        public static string receiveUrl = "https://api.pushover.net/1/messages.json?secret=" + secret + "&device_id=" + deviceID;
        public static string deleteUrl = "https://api.pushover.net/1/devices/" + deviceID + "/update_highest_message.json";
        public static string webSocketUrl = "wss://client.pushover.net/push";

        #endregion
        //clean

        #region Pushover

        public WebHandler()
        {

        }

        public static void WebSocketHandler()
        {
            var ws = new WebSocket(webSocketUrl);

            ws.SslConfiguration.EnabledSslProtocols = System.Security.Authentication.SslProtocols.Tls12;
            ws.SslConfiguration.ServerCertificateValidationCallback =
            (sender, certificate, chain, sslPolicyErrors) => {
                return true;
            };

            ws.Connect();

            ws.OnMessage += (sender, e) =>
            {
                Console.Write("\n\nAssistant info: ");

                string result = System.Text.Encoding.UTF8.GetString(e.RawData);

                if (result == "!")
                {
                    Console.Write("A new message has arrived; you should perform a sync");

                    RecieveCommand();
                }
                else if (result == "#")
                {
                    Console.Write("Keep-alive packet, no response needed");

                    MainWindow.connected = true;

                    MainWindow.ConnectionCheck();
                }
                else if (result == "R")
                {
                    Console.Write("Reload request; you should drop your connection and re-connect");

                    MainWindow.connected = false;

                    MainWindow.ConnectionCheck();
                }
                else if (result == "E")
                {
                    Console.Write("Error; a permanent problem occured and you should not automatically re-connect. Prompt the user to login again or re-enable the device");

                    MainWindow.connected = false;

                    MainWindow.ConnectionCheck();
                }
            };

            ws.OnError += (sender, e) => {
                Debug.WriteLine("Error: " + e.Message);
            };

            ws.Send("login:" + deviceID + ":" + secret + "\n");

            MainWindow.connected = true;

            MainWindow.ConnectionCheck();

            Console.WriteLine("\nAssistant Connection Established\n");
        }

        public static void RecieveCommand()
        {
            WebClient wc = new WebClient();

            var data = wc.DownloadString(receiveUrl);

            var responseString = data;

            ConvertJson(responseString);

            DeletePreviousMessages();
        }

        public static void DeletePreviousMessages()
        {
            WebClient wc = new WebClient();

            wc.QueryString.Add("secret", secret);
            wc.QueryString.Add("message", highestMessage.ToString());

            var data = wc.UploadValues(deleteUrl, "POST", wc.QueryString);

            var responseString = UnicodeEncoding.UTF8.GetString(data);

            Console.WriteLine("Messages Deleted");
        }

        public static void ConvertJson(string json)
        {
            JObject incomingMessagesJson = JObject.Parse(json);

            IList<JToken> incomingMessages = incomingMessagesJson["messages"].Children().ToList();
            IList<Messages> incomingMessagesCSharp = new List<Messages>();

            int messageCount = 0;

            title = new string[incomingMessages.Count];
            message = new string[incomingMessages.Count];

            foreach (JToken incomingMessage in incomingMessages)
            {
                Messages cSharpMessage = incomingMessage.ToObject<Messages>();
                incomingMessagesCSharp.Add(cSharpMessage);

                if (cSharpMessage.message != null)
                {
                    message[messageCount] = cSharpMessage.message;
                }

                if (cSharpMessage.title != null)
                {
                    Console.WriteLine("\n\n New Assistant Message: " + cSharpMessage.title + "!\n");
                    title[messageCount] = cSharpMessage.title;

                    if (title[messageCount] == "Blasting on Discord")
                    {
                        DiscordBot db = new DiscordBot(message[messageCount]);
                    }

                    messageCount++;
                }

                if (cSharpMessage.id != 0)
                    highestMessage = cSharpMessage.id;
            }

            if (messageCount > 0)
            {
                CheckIncomingMessageAgainstCommand(title[messageCount - 1]);
            }
        }

        public static void CheckIncomingMessageAgainstCommand(string title)
        {
            string[] readTextGroups = File.ReadAllLines(@"Groups\Groups.txt");
            string[] commands = new string[readTextGroups.Length];
            string[] groupNames = new string[readTextGroups.Length];
            string[] active = new string[readTextGroups.Length];

            string groupName = "";

            for(int i = 0; i < readTextGroups.Length; i++)
            {
                string[] separator = Regex.Split(readTextGroups[i], "I---I");

                commands[i] = separator[1];
                groupNames[i] = separator[0];
                active[i] = separator[3];
            }    
            
            for(int i = 0; i < commands.Length; i++)
            {
                if(commands[i].ToLower() == title.ToLower() && active[i] == "Y")
                {
                    groupName = groupNames[i];
                }

                if(title.ToLower().Contains("play the movie"))
                {
                    DataManagement.GenerateMoviesList();

                    string[] readText = File.ReadAllLines(@"Movies\Movies.txt");

                    int chosenMovie = 0;

                    int movieCounter = 0;

                    foreach (string line in readText)
                    {
                        if (line.ToLower().Contains(title.ToLower().Substring(15)))
                        {
                            chosenMovie = movieCounter;
                        }

                        movieCounter++;
                    }

                    string path = DataManagement.MoviesList[chosenMovie].MoviePath;

                    ChromeCastManager c = new ChromeCastManager();

                    ChromeCastManager.movie = c;

                    ChromeCastManager.movie.PlayMovieAsync(path);
                }

                if (title.ToLower() == "stop")
                {
                    if(ChromeCastManager.playerStatus == "Playing")
                        ChromeCastManager._mediaPlayer.Stop();
                }

                if (title.ToLower() == "pause")
                {
                    if (ChromeCastManager.playerStatus == "Playing")
                    {
                        ChromeCastManager._mediaPlayer.Pause();

                        ChromeCastManager.playerStatus = "Paused";
                    }
                }

                if (title.ToLower() == "resume")
                {
                    if (ChromeCastManager.playerStatus == "Paused")
                    {
                        ChromeCastManager._mediaPlayer.Play();

                        ChromeCastManager.playerStatus = "Playing";
                    }
                }
            }

            DataManagement.SetSelectedActions(groupName.Replace(" ", string.Empty));
        }

        #endregion
        //clean
    }
}
