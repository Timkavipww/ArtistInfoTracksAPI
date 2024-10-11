using ServiceStack;
using SpotifyAPI.Web;

namespace ArtistsParser
{
    class Program
    {
        static async Task Main()
        {
            string token = "3fdc5a8f934346678ece8819b87078bf";
            var spotify = new SpotifyClient(token);
            public static SpotifyClientConfig DefaultConfig = SpotifyClientConfig.CreateDefault();

        public Http     Result Get()
        {
            var config = DefaultConfig.WithToken("YourAccessToken");
            var spotify = new SpotifyClient(config);
        }
        var track = await spotify.Tracks.Get("4oi4zOkiJ0x2D9WTTbrPWI");
            Console.WriteLine(track.Name);
        }
    }
}
