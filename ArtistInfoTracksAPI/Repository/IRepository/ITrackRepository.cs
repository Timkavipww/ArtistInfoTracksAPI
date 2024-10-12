using ArtistInfoTracksAPI.Models.ArtistsModel;
using ArtistInfoTracksAPI.Models.DTO;
using ArtistInfoTracksAPI.Models.Interfaces;
using ArtistInfoTracksAPI.Models.TrackModel;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;

namespace ArtistInfoTracksAPI.Repository.IRepository
{
    public interface ITrackRepository
    {
        Task<int> GetArtistId(Artist artist);
        Task<ICollection<Track>> GetAllAsync(int artistId);
        Task<ICollection<Track>> GetAllAsync();
        Task<Track> GetAsync(int id);
        Task AddTrackToArtist(int artistId, TrackToCreateDTO trackToCreateDTO);
        Task<List<Track>> GetTracksByArtistId(int artistId);
  
        Task<Track> GetAsync(string name);
        Task CreateAsync(TrackToCreateDTO trackToCreateDTO);

        //Task RemoveAsync(int id);
        Task RemoveAsync(int id);
        Task UpdateAsync(Track track);
        Task SaveAsync();
    }
}
