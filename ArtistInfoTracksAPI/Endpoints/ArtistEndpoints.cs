using ArtistInfoTracksAPI.Data;
using ArtistInfoTracksAPI.ExtensionMapper;
using ArtistInfoTracksAPI.Models;
using ArtistInfoTracksAPI.Models.ArtistsModel;
using ArtistInfoTracksAPI.Models.DTO;
using ArtistInfoTracksAPI.Models.TrackModel;
using ArtistInfoTracksAPI.Repository.Interfaces;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ApiExplorer;
using System.Data.Common;
using System.Net;
using System.Runtime.CompilerServices;

namespace ArtistInfoTracksAPI.Endpoints
{
    public static class ArtistEndpoints
    {
        public static void ConfigureArtistsEndpoints(this WebApplication app)
        {
            app.MapGet("api/artist", GetAllArtists).WithName("GetArtists").Produces<APIResponse>(200);
            app.MapPost("api/artist", CreateArtist).WithName("CreateArtist").Produces(400).Produces<APIResponse>(200);
            app.MapGet("api/artist/{id:int}", GetArtistById).WithName("GetAsyncById").Produces(400).Produces<APIResponse>(200);
            app.MapGet("api/artist/name/{name}", GetArtistByName).WithName("GetAsyncByName").Produces(400).Produces<APIResponse>(200);
            app.MapDelete("api/artist", RemoveAsyncArtist).WithName("Remove").Produces(400).Produces<APIResponse>(200);
            app.MapPut("api/artist", UpdateArtist).WithName("UpdateArtist").Produces(400).Produces<APIResponse>(200);
        }
        //Get all Artists
        private static async Task<IResult> GetAllArtists(IArtistRepository _context)
        {
            var response = new APIResponse();

            try
            {
                var artists = await _context.GetAllAsync();
                response.Success(artists);

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
        //Create Artist
        private static async Task<IResult> CreateArtist([FromBody] ArtistCreateDTO artistCreateDTO, IArtistRepository _context)
        {
            var response = new APIResponse() { };

            try
            {
                var artists = await _context.GetAllAsync();
                if (artists.FirstOrDefault(u => u.Name == artistCreateDTO.Name) != null)
                {
                    response.Result = $"{artistCreateDTO.Name} already exists";
                    return Results.BadRequest(response);
                }
                if (artistCreateDTO != null)
                {
                    await _context.CreateAsync(artistCreateDTO);
                    await _context.SaveAsync();

                    response.Created(artistCreateDTO);

                    return Results.Ok(response);
                }
                else
                {
                    return Results.BadRequest(response);
                }
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
        //Get Artist by Id
        private static async Task<IResult> GetArtistById(int id, IArtistRepository _context)
        {
            var response = new APIResponse();
            try
            {
                var gotArtist = await _context.GetAsync(id);
                if (gotArtist != null)
                {
                    response.Success(gotArtist);

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
        //Get Artist by Name
        private static async Task<IResult> GetArtistByName(string name, IArtistRepository _context)
        {
            var response = new APIResponse();
            try
            {
                var gotArtist = await _context.GetAsync(name);

                if (gotArtist != null)
                {
                    response.Success(gotArtist);

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
        //Remove Artist by Id
        private static async Task<IResult> RemoveAsyncArtist(IArtistRepository _context, int id)
        {
            var response = new APIResponse();
            try
            {
                var artist = await _context.GetAsync(id);
                if (artist != null)
                {
                    await _context.RemoveAsync(artist);
                    await _context.SaveAsync();
                    response.Success(artist);

                    return Results.Ok(response);
                }
                response.NotFound();
                return Results.NotFound(response);
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
        //Update Artist
        private async static Task<IResult> UpdateArtist([FromBody] ArtistToUpdateDTO artistFromBody, IArtistRepository _context)
        {
            var response = new APIResponse();

            try
            {
                if (artistFromBody == null)
                {
                    response.ErrorMessages.Add("Artist data is required.");
                    return Results.BadRequest(response);
                }

                var artist = await _context.GetAsync(artistFromBody.Id);

                if (artist == null)
                {
                    response.NotFound();
                    return Results.NotFound(response);
                }

                artist.Update(artistFromBody);

                response.Success();
                response.Result = $"Updated artist {artist.Name}.";

                await _context.UpdateAsync(artist);
                await _context.SaveAsync();

                return Results.Ok(response);
            }
            catch (DbException dbEx)
            {
                response.ErrorMessages.Add($"Database error: {dbEx.Message}");
                return Results.BadRequest(response);
            }
            catch (Exception ex)
            {
                response.ErrorMessages.Add($"Unexpected error: {ex.Message}");
                return Results.BadRequest(response);
            }

        }
    }
}
