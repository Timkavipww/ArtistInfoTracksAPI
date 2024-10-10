
using ArtistInfoTracksAPI.Models.ArtistsModel;
using System.Data.SqlTypes;
using System.Text.Json.Serialization;

namespace ArtistInfoTracksAPI.Models.TrackModel
{
    public class Track
    {
        public int Id { get; set; }
        public string AlbumName { get; set; }
        public string Name { get; set; }
        public string Title { get; set; }
        public string Lyrics { get; set; }

        public int ArtistId { get; set; }

        [JsonIgnore]
        public Artist Artist { get; set; }

    }
}
