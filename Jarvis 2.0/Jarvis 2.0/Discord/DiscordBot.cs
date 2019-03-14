#region Imports

using System.Collections.Specialized;
using System.Net;

#endregion
//clean

namespace Jarvis_2._0
{
    public class DiscordBot
    {
        #region Values

        string uri = "https://discordapp.com/api/webhooks/552817880945655819/84m77BGk23298D-Idb5gXACP5m0hdYhVOAljy3QDNdoe7hXgTBA0QDwzEV30HjVb4_7I";

        #endregion
        //clean

        public DiscordBot(string Message)
        {
            Http.Post(uri, new NameValueCollection()
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