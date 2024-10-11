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
using System.Diagnostics;
using System.Net;
using System.Text.Json;

namespace ArtistInfoTracksAPI.Endpoints
{
    public static class TrackEndpoints
    {
        public static void ConfigureTrackEndpoints(this WebApplication app)
        {
            app.MapGet("api/artist/{id}/track", GetAllTracksByArtistId).WithName("GetAllTracksByArtistId");
            app.MapGet("api/artist/{id}/track/{trackId}", GetTrackById).WithName("GetTrackById");
            app.MapGet("api/artist/{id}/track/name/{name}", GetTrackByName).WithName("GetTrackByName");
            app.MapPost("api/artist/{id}/track", AddTrackToArtist).WithName("AddTrackByArtistId");
            app.MapDelete("api/artist/{id}/track", DeleteTrackById).WithName("DeleteTrackById");
            app.MapPut("api/artist/{id}/track", UpdateTrackById).WithName("UpdateTrackById");
            app.MapGet("api/track", GetAllTracks).WithName("GetAllTracks");
        }

        private static async Task<IResult> GetAllTracksByArtistId(int artistId, ITrackRepository _trackRepository, IArtistRepository _artistRepository)
        {
            var response = new APIResponse();
            var artist = _artistRepository.GetAllAsync();
            var exisartist = artist.Result.Where(u => u.Id == artistId).ToList();

            var tracks = await _trackRepository.GetAllAsync(artistId);
            if (exisartist.Count == 0)
            {
                response.StatusCode = HttpStatusCode.NotFound;
                response.Result = $"artist not found w id {artistId}";
                return Results.NotFound(response);
            }
            if (tracks != null)
            {
                response.StatusCode = HttpStatusCode.OK;
                response.isSuccess = true;
                response.Result = tracks;
                return Results.Ok(response);
            }
            return Results.BadRequest(response);


        }

        private async static Task<IResult> AddTrackToArtist([FromBody] TrackToCreateDTO track, IArtistRepository _context)
        {
            var response = new APIResponse();

            var artist = await _context.GetAsync(track.ArtistId);

            if (artist == null)
            {
                response.Result = "Artist not found";
                response.StatusCode = HttpStatusCode.NotFound;
                return Results.NotFound(response);
            }

            var trackWithSameName = artist.Tracks.FirstOrDefault(t => t.Name == track.Name);
            if (trackWithSameName != null)
            {
                response.Result = $"Track {trackWithSameName.Name} already exists for this artist";
                response.StatusCode = HttpStatusCode.BadRequest;
                return Results.BadRequest(response);
            }

            var newTrack = new Track {};
            newTrack.Name = track.Name != "string" ? track.Name : newTrack.Name;
            newTrack.ArtistId = track.ArtistId > 0 ? track.ArtistId : newTrack.ArtistId;
            newTrack.Lyrics = track.Lyrics != "string" ? track.Lyrics : newTrack.Lyrics;
            newTrack.AlbumName = track.AlbumName != "string" ? track.AlbumName : newTrack.AlbumName;
            newTrack.Title = track.Title != "string" ? track.Title : newTrack.Title;

            artist.Tracks.Add(newTrack);
            await _context.SaveAsync();

            response.Result = newTrack;
            response.isSuccess = true;
            response.StatusCode = HttpStatusCode.Created;

            return Results.Ok(response);
        }

        private static async Task<IResult> DeleteTrackById(int artistID, int trackId, IArtistRepository _context)
        {
            var response = new APIResponse();

            var artist = await _context.GetAsync(artistID);
            if(artist == null)
            {
                response.StatusCode= HttpStatusCode.NotFound;
                response.Result = "not found artist";
                return Results.NotFound(response);
            }
            var track = artist.Tracks.FirstOrDefault(u => u.Id == trackId);

            artist.Tracks.RemoveAt(trackId);
            response.Result = $"deleted track {track.Name}";
            return Results.Ok(response);

        }

        private static async Task<IResult> UpdateTrackById(int artistId, int trackId, [FromBody] TrackToUpdateDTO trackFromBody, IArtistRepository _contextArtist, ITrackRepository _contextTrack)
        {
            var response = new APIResponse();

            // Получаем артиста и проверяем на наличие
            var artist = await _contextArtist.GetAsync(artistId);
            if (artist == null)
            {
                response.StatusCode = HttpStatusCode.NotFound;
                return Results.NotFound(response);
            }

            // Проверяем входные данные
            if (trackFromBody == null)
            {
                return Results.BadRequest(response);
            }

            // Обновляем трек
            var track = await _contextTrack.GetAsync(trackId); // Предполагается, что у вас есть метод для получения трека
            if (track == null)
            {
                response.StatusCode = HttpStatusCode.NotFound;
                return Results.NotFound(response);
            }

            // Обновление полей с использованием тернарного оператора
            track.AlbumName = trackFromBody.AlbumName != "string" ? trackFromBody.AlbumName : track.AlbumName;
            track.Title = trackFromBody.Title != "string" ? trackFromBody.Title : track.Title;
            track.Lyrics = trackFromBody.Lyrics != "string" ? trackFromBody.Lyrics : track.Lyrics;
            track.Name = trackFromBody.Name != "string" ? trackFromBody.Name : track.Name;

            // Сохраняем изменения
            await _contextTrack.SaveAsync();

            response.isSuccess = true;
            response.StatusCode = HttpStatusCode.OK;
            response.Result = $"Track updated: {track.Title} by artist {artist.Name}";

            return Results.Ok(response);
        }

        private async static Task<IResult> GetTrackById(int id, ITrackRepository _context)
        {
            var response = new APIResponse(){ };
            if (id < 0)
            {
                response.Result = "incorrect id";
                response.StatusCode = HttpStatusCode.NotFound;
                return Results.NotFound(response);
            }
            var track = await _context.GetAsync(id);
            response.Result = track;
            return Results.Ok(response);
        }
        private async static Task<IResult> GetTrackByName(string name, ITrackRepository _context)
        {
            var response = new APIResponse() { };
            if (name == null)
            {
                response.Result = "not found";
                response.StatusCode= HttpStatusCode.NotFound;
                return Results.NotFound(response);
            }
            var track = await _context.GetAsync(name);
            response.Result = track;
            return Results.Ok(response);
        }

        private static async Task<IResult> GetAllTracks(ITrackRepository _context)
        {
            var response = new APIResponse() { };

            response.Result = await _context.GetAllAsync();
            return Results.Ok(response);
        }
    }
}