﻿using ArtistInfoTracksAPI.Models.ArtistsModel;
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
        Task<Track> GetAsync(int id);
        Task AddTrackToArtist(int artistId, TrackToCreateDTO trackToCreateDTO);
        Task<List<Track>> GetTracksByArtistId(int artistId);
  
        Task<Track> GetAsync(string name);
        Task CreateAsync(TrackDTO artistCreateDTO);

        //Task RemoveAsync(int id);
        Task RemoveAsync(Track track);
        Task UpdateAsync(TrackToUpdateDTO track);
        Task SaveAsync();
    }
}
