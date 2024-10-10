using ArtistInfoTracksAPI.Models.ArtistsModel;
using ArtistInfoTracksAPI.Models.DTO;
using ArtistInfoTracksAPI.Models.Interfaces;
using ArtistInfoTracksAPI.Models.TrackModel;
using Microsoft.EntityFrameworkCore.ChangeTracking;

namespace ArtistInfoTracksAPI.Repository.IRepository
{
    public interface ITrackRepository
    {
        Task<ICollection<Track>> GetAllAsync();
        Task<Track> GetAsync(int id);
        Task<Track> GetAsync(string name);
        Task CreateAsync(TrackToCreateDTO artistCreateDTO);
        //Task RemoveAsync(int id);
        Task RemoveAsync(Track track);
        Task UpdateAsync(TrackToUpdateDTO track);
        Task SaveAsync();
    }
}
