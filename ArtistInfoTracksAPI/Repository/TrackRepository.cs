using ArtistInfoTracksAPI.Data;
using ArtistInfoTracksAPI.Models.DTO;
using ArtistInfoTracksAPI.Models.TrackModel;
using ArtistInfoTracksAPI.Repository.IRepository;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ArtistInfoTracksAPI.Repository
{
    public class TrackRepository : ITrackRepository
    {
        private readonly ApplicationDbContext _context;

        public TrackRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task CreateAsync(TrackToCreateDTO artistCreateDTO)
        {
            throw new NotImplementedException();
        }

        public async Task<ICollection<Track>> GetAllAsync()
        {
            throw new NotImplementedException();
        }

        public async Task<Track> GetAsync(int id)
        {
            throw new NotImplementedException();
        }

        public async Task<Track> GetAsync(string name)
        {
            throw new NotImplementedException();
        }

        public async Task RemoveAsync(Track track)
        {
            throw new NotImplementedException();
        }

        public async Task SaveAsync()
        {
            throw new NotImplementedException();
        }

        public async Task UpdateAsync(TrackToUpdateDTO track)
        {
            throw new NotImplementedException();
        }
    }
}
