using Genius;
using Genius.Models.Annotation;
using Genius.Models.Referent;

namespace GeniusParser
{
        class Program
        {
            public static async Task Main(string[] args)
            {
            string id = "z9XVldO50ImWrL5Al_HrcqEQfbyoNowDHX_s7z_xoSE4vd-EkvXKpsrmZT0c9VhD";
            string secret = "2_LITWg0XSxHf64zGKJTrU_f0GqEksiVhBJJaheJ3E4w28e7BNgft_LfEZGEDkWHs-t2huZac5R5-c4wh5OxTQ";
            string token = "MFnsOnG2wTa3e16Z66EBn_l4PgoniKJAEyJ1LHvAPBo8rczf0Nfu0qJpnS3ZSmVv";
                try
                {
                    var client1 = new GeniusClient(id);
                    var client2 = new GeniusClient(secret);
                    var client = new GeniusClient(token);




                    var referent = await client.ReferentClient.GetReferent(webPageId: "10347");

                    var song = await client.SongClient.GetSong(378195);

                    var artist = client.ArtistClient.GetArtist(16775);

                    var artistsSongs = await client.ArtistClient.GetArtistsSongs(16775, sort: "title");

                var webPage = await client.WebPageClient.GetWebPage(Uri.EscapeDataString("https://docs.genius.com"));

                    var search = client.SearchClient.Search("Kendrick Lamar");
                }
                catch (Exception e)
                {
                    Console.WriteLine(e);
                }
            }
        }
}
