
using ArtistInfoTracksAPI.Models.ArtistsModel;
using System.Data.SqlTypes;

namespace ArtistInfoTracksAPI.Models.TrackModel
{
    public class Track
    {
        public Track() { }
        public Track(Artist artist)
        {
            Artist = artist;
        }
        public int ArtistId { get; set; }
        public Artist Artist { get; set; }
        public string? Name { get; set; }
        public string? Title { get; set; }
        public string? Lyrics { get; set; }

    }
}
