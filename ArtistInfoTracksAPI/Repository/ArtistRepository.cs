using ArtistInfoTracksAPI.Data;
using ArtistInfoTracksAPI.ExtensionMapper;
using ArtistInfoTracksAPI.Models.ArtistsModel;
using ArtistInfoTracksAPI.Models.DTO;
using ArtistInfoTracksAPI.Models.TrackModel;
using ArtistInfoTracksAPI.Repository.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Data.Common;

namespace ArtistInfoTracksAPI.Repository
{
    public class ArtistRepository : IArtistRepository
    {
        private readonly ApplicationDbContext _context;
        public ArtistRepository(ApplicationDbContext context) 
        {
            _context = context;
        }

     


        public async Task CreateAsync(ArtistCreateDTO artistCreateDTO)
        {
            try
            {
                var artistEntity = artistCreateDTO.createdToEntity();

                await _context.AddAsync(artistEntity);

            }
            catch (Exception ex)
            {
                throw new Exception($"Ошибка при создании артиста: {ex.Message}", ex);
            }
        }



        public async Task<ICollection<Artist>> GetAllAsync()
        {
            try
            {
                return await _context.Artists.AsNoTracking().ToListAsync();
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


        public async Task<Artist> GetAsync(int id)
        {
            var artist =  await _context.Artists.FirstOrDefaultAsync(u => u.Id == id);

            try
            {
                if (artist != null)
                {

                    return artist;
                    
                } else
                {
                    return null;
                }

            }
            catch (DbException dbEx)
            {
                Console.WriteLine($"{dbEx.Message}");
                throw;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"{ex.Message}");
                throw;
            }
        }
 
        public async Task<Artist> GetAsync(string name)
        {
            var artist = await _context.Artists.FirstOrDefaultAsync(u => u.Name == name);
            
            if(artist != null)
            {
                return artist;
            } else
            {
                return null;
            }
        }

        public async Task RemoveAsync(Artist artist)
        {
            if(artist == null)
            {
                Console.WriteLine($"");
            }
            _context.Remove(artist);
            await _context.SaveChangesAsync();
        }

        public async Task SaveAsync()
        {
            try
            {
                await _context.SaveChangesAsync();

            }
            catch (DbException dbEx)
            {
                Console.WriteLine($"{dbEx.Message}");
                throw;
            }
        }

        public async Task UpdateAsync(ArtistToUpdateDTO artist)
        {
            if (artist != null)
            {
                try
                {
                    _context.Update(artist);
                }
                catch (DbException dbEx)
                {
                    Console.WriteLine($"{dbEx.Message}");
                }
            }

        }
    }
}
