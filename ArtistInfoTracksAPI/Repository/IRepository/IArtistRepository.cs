using ArtistInfoTracksAPI.Models.ArtistsModel;
using ArtistInfoTracksAPI.Models.DTO;
using ArtistInfoTracksAPI.Models.TrackModel;

namespace ArtistInfoTracksAPI.Repository.Interfaces
{
    public interface IArtistRepository
    {
        Task<ICollection<Artist>> GetAllAsync();
        Task<Artist> GetAsync(int id);
        Task<Artist> GetAsync(string name);
        Task CreateAsync(ArtistCreateDTO artistCreateDTO);
        //Task RemoveAsync(int id);
        Task RemoveAsync(Artist artist);
        Task UpdateAsync(ArtistToUpdateDTO artist);
        Task SaveAsync();

    }
}
