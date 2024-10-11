using ArtistInfoTracksAPI.Models.TrackModel;
using Microsoft.AspNetCore.Mvc.ApplicationModels;

namespace ArtistInfoTracksAPI.Models.ArtistsModel
{
    public class Artist
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }

        public virtual List<Track> Tracks { get; set; } = new List<Track>();

    }
}
