using ArtistInfoTracksAPI.ExtensionMapper;
using ArtistInfoTracksAPI.Models;
using ArtistInfoTracksAPI.Models.ArtistsModel;
using ArtistInfoTracksAPI.Models.DTO;
using ArtistInfoTracksAPI.Models.TrackModel;
using ArtistInfoTracksAPI.Repository.Interfaces;
using ArtistInfoTracksAPI.Repository.IRepository;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Identity.Client;
using System.Net;
using System.Text.Json;

namespace ArtistInfoTracksAPI.Endpoints
{
    public static class TrackEndpoints
    {
        public static void ConfigureTrackEndpoints(this WebApplication app)
        {
            app.MapGet("api/artist/{id}/tracks", GetAllTracksByArtistId).WithName("GetAllTracksByArtistId");
            app.MapPost("api/artist/{id}/track", AddTrackToArtist).WithName("AddTrackByArtistId");
        }

        private static async Task<IResult> GetAllTracksByArtistId(int artistId, ITrackRepository _trackRepository)
        {
            var response = new APIResponse();
            var tracks = await _trackRepository.GetTracksByArtistId(artistId);
            if (tracks != null)
            {
                response.StatusCode = HttpStatusCode.OK;
                response.isSuccess = true;
                response.Result = tracks;
                return Results.Ok(response);
            }
            return Results.BadRequest(response);


        }

        private async static Task<IResult> AddTrackToArtist(int artistId, TrackToCreateDTO trackCreateDTO, IArtistRepository _context)
        {
            var response = new APIResponse();

            // Ищем артиста по Id
            var artist = await _context.GetAsync(artistId);
            if (artist == null)
            {
                response.Result = "Artist not found";
                response.StatusCode = HttpStatusCode.NotFound;
                return Results.NotFound(response);
            }

            // Проверяем, существует ли трек с таким же именем у данного артиста
            var trackWithSameName = artist.Tracks.FirstOrDefault(t => t.Name == trackCreateDTO.Name);
            if (trackWithSameName != null)
            {
                response.Result = $"Track {trackWithSameName.Name} already exists for this artist";
                response.StatusCode = HttpStatusCode.BadRequest;
                return Results.BadRequest(response);
            }

            // Если трек уникален, создаём его и добавляем к артисту
            var newTrack = new Track
            {
                Name = trackCreateDTO.Name,
                ArtistId = artist.Id // Связываем трек с артистом
            };

            artist.Tracks.Add(newTrack);
            await _context.SaveAsync();

            response.Result = newTrack;
            response.isSuccess = true;
            response.StatusCode = HttpStatusCode.Created;

            return Results.Ok(response);
        }

    }
}