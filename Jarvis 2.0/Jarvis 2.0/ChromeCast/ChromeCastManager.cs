#region Imports

using LibVLCSharp.Shared;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

#endregion

namespace Jarvis_2._0
{
    public class ChromeCastManager
    {
        #region Values

        readonly HashSet<RendererItem> _rendererItems = new HashSet<RendererItem>();
        LibVLC _libVLC;
        public static MediaPlayer _mediaPlayer;
        RendererDiscoverer _rendererDiscoverer;

        public static ChromeCastManager movie;

        public static bool playFinished = false;
        public static string playerStatus = "Stopped";

        #endregion

        public async void PlayMovieAsync(string path)
        {
            playerStatus = "Playing";

            DiscoverChromecasts();
            
            await Task.Delay(2000);
            
            StartCasting(path);
        }

        private void StartCasting(string path)
        {
            if (!_rendererItems.Any())
            {
                Console.WriteLine("No renderer items found. Abort casting...");
                return;
            }
            
            var media = new Media(_libVLC, path, FromType.FromPath);
            
            _mediaPlayer = new MediaPlayer(_libVLC);
            
            _mediaPlayer.SetRenderer(_rendererItems.First());
            
            _mediaPlayer.Play(media);

            Console.WriteLine("\nPlaying on: " + _rendererItems.First().Name);
        }

        bool DiscoverChromecasts()
        {
            Core.Initialize();
            
            _libVLC = new LibVLC();
            
            RendererDescription renderer;
            
            renderer = _libVLC.RendererList.FirstOrDefault(r => r.Name.Equals("microdns_renderer"));
            
            _rendererDiscoverer = new RendererDiscoverer(_libVLC, renderer.Name);
            
            _rendererDiscoverer.ItemAdded += RendererDiscoverer_ItemAdded;
            
            return _rendererDiscoverer.Start();
        }

        void RendererDiscoverer_ItemAdded(object sender, RendererDiscovererItemAddedEventArgs e)
        {
            Console.WriteLine($"New item discovered: {e.RendererItem.Name} of type {e.RendererItem.Type}");
            if (e.RendererItem.CanRenderVideo)
            {
                Console.WriteLine("Can render video");

                _rendererItems.Add(e.RendererItem);
            }
            if (e.RendererItem.CanRenderAudio)
                Console.WriteLine("Can render audio");

        }
    }
}
