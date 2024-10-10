using ArtistInfoTracksAPI.Models.ArtistsModel;

namespace ArtistInfoTracksAPI.Models.DTO
{
    public class TrackToUpdateDTO
    {
        public TrackToUpdateDTO() { Artist = new Artist(); }
   
        public Artist Artist { get; set; }
        public string Name { get; set; }
        public string Title { get; set; }
        public string Lyrics { get; set; }
    }
}
