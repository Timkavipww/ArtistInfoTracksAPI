using ArtistInfoTracksAPI.Data;
using ArtistInfoTracksAPI.Models.ArtistsModel;
using ArtistInfoTracksAPI.Models.Interfaces;
using ArtistInfoTracksAPI.Repository.IRepository;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using System;

namespace ArtistInfoTracksAPI.Repository
{
    public class BaseRepository : IBaseRepository
    {
        private readonly ApplicationDbContext _context;
        public BaseRepository(ApplicationDbContext context)
        {
            _context = context;
        }
        public async Task SaveChangesAsync()
        {
            await _context.SaveChangesAsync();
        }

        public async Task<EntityEntry<T>> Create<T>(T entity) where T : class, IEntity
        {
            return await _context.Set<T>().AddAsync(entity);
        }

        public async Task<bool> Delete<T>(int Id) where T : class, IEntity
        {
            if (Id > 0)
            {
                var oldEntity = await _context.Set<T>().FirstOrDefaultAsync(u => u.Id == Id);
                if (oldEntity != null)
                {
                    _context.Set<T>().Remove(oldEntity);
                    return true;
                }
            }
            return false;
        }

        public async Task<IEnumerable<T>> GetAll<T>() where T : class, IEntity
        {
            return await _context.Set<T>().ToListAsync();
        }

        public async Task<T> GetById<T>(int Id) where T : class, IEntity
        {
            return await _context.Set<T>().FirstOrDefaultAsync(u => u.Id == Id);
        }

        public async Task<EntityEntry<T>> UpdateAsync<T>(T entity, int Id) where T : class, IEntity
        {
            if (Id > 0)
            {
                var oldEntity = await _context.Set<T>().FirstOrDefaultAsync(u => u.Id == Id);
                if (oldEntity != null)
                {
                    entity.Id = Id;
                    return _context.Set<T>().Update(oldEntity);
                    
                }
            }
            return null;
        }
    }
}
