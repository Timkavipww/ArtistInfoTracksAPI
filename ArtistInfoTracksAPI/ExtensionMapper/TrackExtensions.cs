using ArtistInfoTracksAPI.Models.DTO;
using ArtistInfoTracksAPI.Models.TrackModel;

namespace ArtistInfoTracksAPI.ExtensionMapper
{
    public static class TrackExtensions
    {
        public static Track toEntity(this TrackDTO trackDTO)
        {
            var entity = new Track()
            {
                Name = trackDTO.Name,
                ArtistId = trackDTO.ArtistId,
                Id = trackDTO.Id
            };
            return entity;
        }
        public static TrackDTO toDTO(this Track track)
        {
            var trackDTO = new TrackDTO()
            {
                Name = track.Name,
                ArtistId = track.ArtistId,
                Id = track.Id
                

            };
            return trackDTO;
        }
        public static Track fromTrackToCreateDTOtoEntity(this TrackToCreateDTO trackToCreateDTO)
        {
            var track = new Track()
            {
                Name = trackToCreateDTO.Name,
            };
            return track;
        }
    }
}
