using Ragnar;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Jarvis_2._0.Torrent
{
    class TorrentManager
    {
        public static void Ragnar()
        {
            using (var session = new Session())
            {
                // Make the session listen on a port in the range
                // 6881-6889
                session.ListenOn(6881, 6889);

                // Create the AddTorrentParams with info about the torrent
                // we'd like to add.
                var addParams = new AddTorrentParams
                {
                    SavePath = @"C:\Users\Joose\source\repos\Jarvis 2.0\Jarvis 2.0\Torrent\Downloads",
                    Url = @"C:\Users\Joose\source\repos\Jarvis 2.0\Jarvis 2.0\Torrent\Torrents\Aquaman (2018) [WEBRip] [1080p] [YTS.AM].torrent"
                };

                // Add a torrent to the session and get a `TorrentHandle`
                // in return.
                var handle = session.AddTorrent(addParams);

                while (true)
                {
                    // Get a `TorrentStatus` instance from the handle.
                    var status = handle.QueryStatus();

                    // If we are seeding, our job here is done.
                    if (status.IsSeeding)
                    {
                        break;
                    }

                    // Print our progress and sleep for a bit.
                    Console.WriteLine("{0}% downloaded", status.Progress * 100);
                    Thread.Sleep(1000);
                }
            }
        }
    }
}
