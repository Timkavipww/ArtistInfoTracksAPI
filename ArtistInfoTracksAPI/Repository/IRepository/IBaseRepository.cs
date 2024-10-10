using ArtistInfoTracksAPI.Models.ArtistsModel;
using ArtistInfoTracksAPI.Models.DTO;
using ArtistInfoTracksAPI.Models.Interfaces;
using Microsoft.EntityFrameworkCore.ChangeTracking;

namespace ArtistInfoTracksAPI.Repository.IRepository
{
    public interface IBaseRepository
    {
        Task SaveChangesAsync();
        Task<EntityEntry<T>> Create<T>(T entity) where T : class, IEntity;
        Task<EntityEntry<T>> UpdateAsync<T>(T entity, int Id) where T : class, IEntity;
        Task<bool> Delete<T>(int Id) where T : class, IEntity;
        Task<IEnumerable<T>> GetAll<T>() where T : class, IEntity;
        Task<T> GetById<T>(int Id) where T: class, IEntity;
    }
}

        