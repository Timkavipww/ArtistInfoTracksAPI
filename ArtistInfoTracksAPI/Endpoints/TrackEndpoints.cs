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
using System.Data.Common;
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
            app.MapPost("api/artist/{id}/track", CreateTrack).WithName("AddTrackByArtistId");
            app.MapDelete("api/artist/{id}/track", DeleteTrackById).WithName("DeleteTrackById");
            app.MapPut("api/track", UpdateTrackById).WithName("UpdateTrackById");
            app.MapGet("api/track", GetAllTracks).WithName("GetAllTracks");
        }
        //Get Track by ArtistId
        private static async Task<IResult> GetAllTracksByArtistId(int artistId, ITrackRepository _trackRepository, IArtistRepository _artistRepository)
        {
            var response = new APIResponse();

            try
            {
                var artist = _artistRepository.GetAllAsync();

                var exisartist = artist.Result.Where(u => u.Id == artistId).ToList();

                var tracks = await _trackRepository.GetAllAsync(artistId);
                if (exisartist.Count == 0)
                {
                    response.NotFound();
                    return Results.NotFound(response);
                }
                if (tracks != null)
                {
                    response.Success(tracks);
                    return Results.Ok(response);
                }
                return Results.BadRequest(response);
            }
            catch (DbException dbEx)
            {
                response.dbException(dbEx);
                return Results.BadRequest(response);
            }
            catch (Exception ex)
            {
                response.fatalException(ex);
                return Results.BadRequest(response);
            }
        }
        //Create Track
        private async static Task<IResult> CreateTrack([FromBody] TrackToCreateDTO trackFromBody, IArtistRepository _context)
        {
            var response = new APIResponse();

            try
            {
                var artist = await _context.GetAsync(trackFromBody.ArtistId);

                if (artist == null)
                {
                    response.NotFound();
                    return Results.NotFound(response);
                }

                if (artist.Tracks.FirstOrDefault(t => t.Name == trackFromBody.Name) != null)
                {
                    response.Result = $"Track {trackFromBody.Name} already exists for this artist";
                    return Results.BadRequest(response);
                }

                trackFromBody.fromTrackToCreateDTOtoEntity().UpdateFrom(trackFromBody);

                artist.Tracks.Add(trackFromBody.fromTrackToCreateDTOtoEntity());
                await _context.SaveAsync();

                response.Created(trackFromBody);

                return Results.Ok(response);
            }
            catch (DbException dbEx)
            {
                response.dbException(dbEx);
                return Results.BadRequest(response);
            }
            catch (Exception ex)
            {
                response.fatalException(ex);
                return Results.BadRequest(response);
            }
            
        }
        //Delete Track by Id
        private static async Task<IResult> DeleteTrackById(int trackId, ITrackRepository _context)
        {
            var response = new APIResponse();

            try
            {
                var track = await _context.GetAsync(trackId);
                if(track == null)
                {
                    response.NotFound();
                    return Results.NotFound();
                }
                await _context.RemoveAsync(track.Id);
                await _context.SaveAsync();
                response.Success();
                response.Result = $"deleted track {track.Name}";
                return Results.Ok(response);

            }
            catch (DbException dbEx)
            {
                response.dbException(dbEx);
                return Results.BadRequest(response);
            }
            catch (Exception ex)
            {
                response.fatalException(ex);
                return Results.BadRequest(response);
            }

        }
        //Update Track by Id
        private static async Task<IResult> UpdateTrackById([FromBody] TrackToUpdateDTO trackFromBody, ITrackRepository _context)
        {
            var response = new APIResponse();

            try
            {
                if (trackFromBody == null)
                {
                    response.StatusCode = HttpStatusCode.BadRequest;
                    response.ErrorMessages.Add("Invalid track data.");
                    return Results.BadRequest(response);
                }

                var track = await _context.GetAsync(trackFromBody.Id);  


                if (track == null)
                {
                    response.StatusCode = HttpStatusCode.NotFound;
                    response.ErrorMessages.Add($"Track with ID {trackFromBody.Id} not found.");
                    return Results.NotFound(response);
                }

                track.Update(trackFromBody);

                response.Success();
                response.Result = $@"updated{trackFromBody.Name} ";
                await _context.UpdateAsync(track);
                await _context.SaveAsync();

                return Results.Ok(response);
            }
            catch (DbException dbEx)
            {
                response.ErrorMessages.Add($"{dbEx.Message}");
                return Results.BadRequest(response);
            }
            catch (Exception ex)
            {
                response.ErrorMessages.Add($"{ex.Message}");
                return Results.BadRequest(response);
            }
        }
        //Get Track by Id
        private async static Task<IResult> GetTrackById(int id, ITrackRepository _context)
        {
            var response = new APIResponse(){ };

            try
            {
                var track = await _context.GetAsync(id);

                if (track == null)
                {
                    response.Result = "incorrect id";
                    response.StatusCode = HttpStatusCode.NotFound;
                    return Results.NotFound(response);
                }

                response.isSuccess = true;
                response.StatusCode = HttpStatusCode.OK;
                response.Result = track.toDTO();

                return Results.Ok(response);
            }
            catch (DbException dbEx)
            {
                response.dbException(dbEx);
                return Results.BadRequest(response);
            }
            catch (Exception ex)
            {
                response.fatalException(ex);
                return Results.BadRequest(response);
            }

        }
        //Get Track by Name
        private async static Task<IResult> GetTrackByName(string name, ITrackRepository _context)
        {
            var response = new APIResponse();

            try
            {
                var track = await _context.GetAsync(name);
                if (track == null)
                {
                    response.NotFound();
                    return Results.NotFound(response);
                }

                response.Success();
                response.Result = track.toDTO();
                return Results.Ok(response);
            }
            catch (DbException dbEx)
            {
                response.dbException(dbEx);
                return Results.BadRequest(response);
            }
            catch (Exception ex)
            {
                response.fatalException(ex);
                return Results.BadRequest(response);
            }
            
        }
        //Get All Tracks
        private static async Task<IResult> GetAllTracks(ITrackRepository _context)
        {
            var response = new APIResponse() { };

            try
            {
                response.Result = await _context.GetAllAsync();
                return Results.Ok(response);
            }
            catch (DbException dbEx)
            {
                response.dbException(dbEx);
                return Results.BadRequest(response);
            }
            catch (Exception ex)
            {
                response.fatalException(ex);
                return Results.BadRequest(response);
            }

        }
    }
}