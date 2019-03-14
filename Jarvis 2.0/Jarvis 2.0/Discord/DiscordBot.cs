#region Imports

using System.Collections.Specialized;
using System.Net;

#endregion
//clean

namespace Jarvis_2._0
{
    public class DiscordBot
    {
        public DiscordBot(string Message)
        {
            Http.Post(Windows.Settings.discordUri, new NameValueCollection()
            {
                {
                "content",
                Message
                }
            });
        }
    }
    //clean

    class Http
    {
        public static byte[] Post(string uri, NameValueCollection pairs)
        {
            using (WebClient webClient = new WebClient())
                return webClient.UploadValues(uri, pairs);
        }
    }
    //clean
}