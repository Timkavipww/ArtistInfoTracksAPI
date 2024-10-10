using ArtistInfoTracksAPI.Data;
using ArtistInfoTracksAPI.ExtensionMapper;
using ArtistInfoTracksAPI.Models.ArtistsModel;
using ArtistInfoTracksAPI.Models.DTO;
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
            var artist = await _context.Artists.FindAsync(artistId);
            if (artist == null)
            {
                throw new Exception("Artist not found");
            }

            var newTrack = new Track
            {
                Name = trackCreateDTO.Name,
                ArtistId = artist.Id
            };

            _context.Tracks.Add(newTrack);
            await _context.SaveChangesAsync();
        }


        public async Task CreateAsync(TrackDTO trackCreateDTO)
        {
            try
            {
                var trackEntity = trackCreateDTO.toEntity();
                await _context.Tracks.AddAsync(trackEntity);
                await _context.AddAsync(trackEntity);

            }
            catch (Exception ex)
            {
                throw new Exception($"Ошибка при создании артиста: {ex.Message}", ex);
            }
        }

        public async Task<ICollection<Track>> GetAllAsync(int artistId)
        {

            var tracks = await _context.Tracks
            .Where(u => u.ArtistId == artistId)
            .ToListAsync();
            try
            {
                return tracks;
            }
            catch (DbException dbEx)
            {
                Console.WriteLine($"Database error: {dbEx.Message}");
                throw;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"General error: {ex.Message}");
                throw;
            }
        }

            public async Task<int> GetArtistId(Artist artist)
         {
            var result = await _context.Artists.FirstOrDefaultAsync(u => u.Id == artist.Id);
            if (result != null)
            {
                return result.Id;
            }
            return 0;
        }

        public async Task<Track> GetAsync(int id)
        {
            throw new NotImplementedException();
        }

        public async Task<Track> GetAsync(string name)
        {
            throw new NotImplementedException();
        }

        public async Task<List<Track>> GetTracksByArtistId(int artistId)
        {
                var artist = await _context.Artists.Include(a => a.Tracks).FirstOrDefaultAsync(a => a.Id == artistId);
                if (artist == null)
                {
                    throw new Exception("Artist not found");
                }

                return artist.Tracks;

        }

        public async Task RemoveAsync(Track track)
        {
            throw new NotImplementedException();
        }

        public async Task SaveAsync()
        {
            await _context.SaveChangesAsync();
        }

        public async Task UpdateAsync(TrackToUpdateDTO track)
        {
            throw new NotImplementedException();
        }
    }
}
