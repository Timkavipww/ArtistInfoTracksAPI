using ArtistInfoTracksAPI.Models.ArtistsModel;
using ArtistInfoTracksAPI.Models.DTO;
using Microsoft.EntityFrameworkCore.Metadata.Conventions;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Microsoft.IdentityModel.Protocols;

namespace ArtistInfoTracksAPI.ExtensionMapper
{
    public static class ArtistExtensions
    {
        public static ArtistDTO ToDto(this Artist artist)
        {
            var artistDTO = new ArtistDTO()
            {
                Id = artist.Id,
                Name = artist.Name
            };
            return artistDTO;
        }
        public static Artist toEntity(this ArtistDTO artistDTO)
        {
            var artist = new Artist()
            {
                Name = artistDTO.Name,
                Id = artistDTO.Id
            };
            return artist;
        }
        public static Artist createdToEntity(this ArtistCreateDTO artistCreateDTO)
        {
            var artist = new Artist()
            {
                Name = artistCreateDTO.Name,
                Description = artistCreateDTO.Description,


            };
            return artist;
        }
        public static ArtistToDeleteDTO toDeleteDto(this Artist artist)
        {
            var artistToDelete = new ArtistToDeleteDTO()
            {
                Id = artist.Id,
            };
            return artistToDelete;
        }
        public static Artist toEntity(this ArtistToDeleteDTO artistToDelete)
        {
            var artist = new Artist()
            {
                Id = artistToDelete.Id
            };
            return artist;
        }
    }
}
