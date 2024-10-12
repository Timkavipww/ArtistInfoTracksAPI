using ArtistInfoTracksAPI.Models.DTO;
using ArtistInfoTracksAPI.Models.TrackModel;
using Microsoft.Extensions.Diagnostics.HealthChecks;
using System.Reflection;
using System.Reflection.Metadata.Ecma335;

namespace ArtistInfoTracksAPI.ExtensionMapper
{
    public static class TrackExtensions
    {
        public static Track toEntity(this TrackDTO trackDTO)
        {
            return new Track()
            {
                Name = trackDTO.Name,
                ArtistId = trackDTO.ArtistId,
                Id = trackDTO.Id
            };
        }
        public static TrackDTO toDTO(this Track track)
        {
            return new TrackDTO()
            {
                Name = track.Name,
                ArtistId = track.ArtistId,
                Id = track.Id
            };
        }
        public static Track fromTrackToCreateDTOtoEntity(this TrackToCreateDTO trackToCreateDTO)
        {
            return new Track()
            {
                Name = trackToCreateDTO.Name,
                Title = trackToCreateDTO.Title,
                AlbumName = trackToCreateDTO.AlbumName,
                Artist = trackToCreateDTO.Artist,
                ArtistId = trackToCreateDTO.ArtistId,
                Lyrics = trackToCreateDTO.Lyrics
            };
        }
        public static Track fromTrackToUpdateDTOToEntity(this TrackToUpdateDTO trackToUpdateDTO)
        {
            return new Track()
            {
                Id = trackToUpdateDTO.Id,
                Name = trackToUpdateDTO.Name,
                Title = trackToUpdateDTO.Title,
                AlbumName = trackToUpdateDTO.AlbumName,
                Artist = trackToUpdateDTO.Artist,
            };
        }
        public static TrackToUpdateDTO fromEntityToUpdateDTO(this Track entity)
        {
            return new TrackToUpdateDTO()
            {
                Id = entity.Id,
                Name = entity.Name,
                Title = entity.Title,
                Artist = entity.Artist,
                AlbumName = entity.AlbumName,
                Lyrics = entity.Lyrics
            };
        }
        public static void Update (this Track track, TrackToUpdateDTO trackFromBody)
        {
            if (trackFromBody == null) return;

            var trackProperties = typeof(Track).GetProperties(BindingFlags.Public | BindingFlags.Instance);

            var dtoProperties = typeof(TrackToUpdateDTO).GetProperties(BindingFlags.Public | BindingFlags.Instance);

            foreach (var dtoProp in dtoProperties)
            {
                var value = dtoProp.GetValue(trackFromBody);
                if (value is not null && value.ToString() != "string")
                {
                    var trackProp = trackProperties.FirstOrDefault(p => p.Name == dtoProp.Name);
                    if (trackProp != null && trackProp.CanWrite)
                    {
                        trackProp.SetValue(track, value);
                    }
                }
            }
        }
        public static void UpdateFrom(this Track track, TrackToCreateDTO newTrack)
        {
            if (newTrack == null) return;

            track.Name = newTrack.Name != "string" ? newTrack.Name : track.Name;
            track.ArtistId = newTrack.ArtistId > 0 ? newTrack.ArtistId : track.ArtistId;
            track.Lyrics = newTrack.Lyrics != "string" ? newTrack.Lyrics : track.Lyrics;
            track.AlbumName = newTrack.AlbumName != "string" ? newTrack.AlbumName : track.AlbumName;
            track.Title = newTrack.Title != "string" ? newTrack.Title : track.Title;
        }
    }

}
