using ArtistInfoTracksAPI.Models.ArtistsModel;

namespace ArtistInfoTracksAPI.Models.DTO
{
    public class TrackToCreateDTO
    {
        public int Id { get; set; }
        public TrackToCreateDTO() { }
        public TrackToCreateDTO(Artist artist)
        {
            Artist = artist;
            Artist = new Artist();
        }
        public int ArtistId { get; set; }
        public Artist Artist { get; set; }
        public string Name { get; set; }

    }
}

