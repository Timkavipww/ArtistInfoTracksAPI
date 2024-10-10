using ArtistInfoTracksAPI.Models.DTO;

public class ArtistDTO
{
    public int Id { get; set; }
    public string Name { get; set; }
    public List<TrackDTO> Tracks { get; set; }
}
