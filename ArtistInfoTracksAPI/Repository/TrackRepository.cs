using ArtistInfoTracksAPI.Data;
using ArtistInfoTracksAPI.ExtensionMapper;
using ArtistInfoTracksAPI.Models.ArtistsModel;
using ArtistInfoTracksAPI.Models.DTO;
using ArtistInfoTracksAPI.Models.Interfaces;
using ArtistInfoTracksAPI.Models.TrackModel;
using ArtistInfoTracksAPI.Repository.IRepository;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using System.Collections.Generic;
using System.Data.Common;
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

        public async Task AddTrackToArtist(int artistId, TrackToCreateDTO trackCreateDTO)
        {
            _context.Tracks.Add(trackCreateDTO.fromTrackToCreateDTOtoEntity());
        }


        public async Task CreateAsync(TrackToCreateDTO trackCreateDTO)
        {
            await _context.AddAsync(trackCreateDTO.fromTrackToCreateDTOtoEntity());
        }

        public async Task<ICollection<Track>> GetAllAsync(int artistId)
        {
            return await _context.Tracks
            .Where(u => u.ArtistId == artistId)
            .ToListAsync();
        }

        public async Task<ICollection<Track>> GetAllAsync()
        {
            return await _context.Tracks.AsNoTracking().ToListAsync(); 
        }

        public async Task<int> GetArtistId(Artist artist)
         {
            var result = await _context.Artists.FirstOrDefaultAsync(u => u.Id == artist.Id);

            return result.Id;
        }

        public async Task<Track> GetAsync(int id)
        {
            return await _context.Tracks.FirstOrDefaultAsync(u => u.Id == id);
        }

        public async Task<Track> GetAsync(string name)
        {
            return await _context.Tracks.FirstOrDefaultAsync(u => u.Name == name);
        }

        public async Task<List<Track>> GetTracksByArtistId(int artistId)
        {
            var artist = await _context.Artists.Include(a => a.Tracks).FirstOrDefaultAsync(a => a.Id == artistId);
            return artist.Tracks;
        }

        public async Task RemoveAsync(int id)
        {
            _context.Remove(await _context.Tracks.FirstOrDefaultAsync(u => u.Id == id));
        }

        public async Task SaveAsync()
        {
            await _context.SaveChangesAsync();
        }

        public async Task UpdateAsync(Track track)
        {

            _context.Tracks.Update(track);
        }
    }
}
