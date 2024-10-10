using ArtistInfoTracksAPI.Models.ArtistsModel;

namespace ArtistInfoTracksAPI.Models.DTO
{
    public class TrackToUpdateDTO
    {
        public TrackToUpdateDTO() { }
   
        public string Name { get; set; }
        public string Title { get; set; }
        public string Lyrics { get; set; }
    }
}
