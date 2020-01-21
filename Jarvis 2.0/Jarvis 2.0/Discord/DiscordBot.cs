#region Imports

using System.Collections.Specialized;
using System.Net;

#endregion

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

    class Http
    {
        public static byte[] Post(string uri, NameValueCollection pairs)
        {
            using (WebClient webClient = new WebClient())
                return webClient.UploadValues(uri, pairs);
        }
    }
}