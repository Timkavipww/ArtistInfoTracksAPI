using ArtistInfoTracksAPI.Models.ArtistsModel;
using System.Text.Json.Serialization;

namespace ArtistInfoTracksAPI.Models.DTO
{
    public class TrackToUpdateDTO
    {
        public int Id { get; set; } 
        public string AlbumName { get; set; }
        public string Name { get; set; }
        public string Title { get; set; }
        public string Lyrics { get; set; }

        [JsonIgnore]
        public virtual Artist Artist { get; set; }
    }
}
