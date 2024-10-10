using ArtistInfoTracksAPI.Data;
using ArtistInfoTracksAPI.ExtensionMapper;
using ArtistInfoTracksAPI.Models;
using ArtistInfoTracksAPI.Models.ArtistsModel;
using ArtistInfoTracksAPI.Models.DTO;
using ArtistInfoTracksAPI.Repository.Interfaces;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ApiExplorer;
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

        private async static Task<IResult> GetAllArtists(IArtistRepository _context)
        {
            var response = new APIResponse();

            try
            {

                var artists = await _context.GetAllAsync();
                response.Result = artists;
                response.isSuccess = true;
                response.StatusCode = HttpStatusCode.OK;

                return Results.Ok(response);
            }
            catch (Exception ex)
            {

                return Results.BadRequest(response.ErrorMessages.FirstOrDefault() + ex.Message);
            }
        }

        private async static Task<IResult> CreateArtist([FromBody] ArtistCreateDTO artistCreateDTO, IArtistRepository _context)
        {
            var response = new APIResponse() { };


            if (artistCreateDTO != null)
            {

                await _context.CreateAsync(artistCreateDTO);
                await _context.SaveAsync();

                response.Result = artistCreateDTO;
                response.isSuccess = true;
                response.StatusCode = HttpStatusCode.Created;

                return Results.Ok(response);
            } else
            {
                return Results.BadRequest(response);
            }
        }
        private async static Task<IResult> GetArtistById(int id, IArtistRepository _context)
        {
            var response = new APIResponse();
            var gotArtist = await _context.GetAsync(id);
            if (gotArtist != null)
            {
                response.isSuccess = true;
                response.StatusCode = HttpStatusCode.OK;
                response.Result = gotArtist;


                return Results.Ok(response);
            }
            else
            {

                return Results.BadRequest(response);
            }

        }
        private async static Task<IResult> GetArtistByName(string name, IArtistRepository _context)
        {
            var response = new APIResponse();
            var gotArtist = await _context.GetAsync(name);

            if (gotArtist != null)
            {
                    response.isSuccess = true;
                    response.StatusCode = HttpStatusCode.OK;
                    response.Result = gotArtist;

                    return Results.Ok(response);
                
            }
            return Results.BadRequest(response);

        }

        private async static Task<IResult> RemoveAsyncArtist(IArtistRepository _context, [FromBody] ArtistToDeleteDTO artistFromBody)
        {
            var response = new APIResponse() {
                isSuccess = false,
                Result = "not found",
                StatusCode = HttpStatusCode.NotFound };
            var artist = artistFromBody.toEntity();
            var artistTo = await _context.GetAsync(artist.Id);
            if (artistTo != null)
            {
                await _context.RemoveAsync(artistTo);
                await _context.SaveAsync();
                return Results.Ok(response);
            } else
            {
                response.StatusCode = HttpStatusCode.OK;
                response.Result = "deleted";
                response.isSuccess = true;
                return Results.Ok(response);
            }
        }

        private async static Task<IResult> UpdateArtist(int id, [FromBody] ArtistToUpdateDTO artistToUpdateDTO, IArtistRepository _context)
        {
            var response = new APIResponse() { };

            var artist = await _context.GetAsync(id);


           if(artist == null || artistToUpdateDTO == null || (artistToUpdateDTO.Name == "string" && artistToUpdateDTO.Description == "string"))
            {
                return Results.BadRequest(response);
            }

            if (artistToUpdateDTO.Name == "string")
            {
                response.Result = $"new description is {artistToUpdateDTO.Description}";
                response.isSuccess = true;
                response.StatusCode = HttpStatusCode.OK;
                artist.Description = artistToUpdateDTO.Description;
                await _context.SaveAsync();
                return Results.Ok(response);
            }

            if (artistToUpdateDTO.Description == "string")
            {
                response.Result = $"new name is {artistToUpdateDTO.Name}";
                response.isSuccess = true;
                response.StatusCode = HttpStatusCode.OK;
                artist.Name = artistToUpdateDTO.Name;
                await _context.SaveAsync();
                return Results.Ok(response);
            }

            if (!(artistToUpdateDTO.Name == "string" && artistToUpdateDTO.Description == "string"))
            {
                artist.Name = artistToUpdateDTO.Name;
                artist.Description = artistToUpdateDTO.Description;
                await _context.SaveAsync();
                return Results.Ok(response);
            }
            return Results.BadRequest(response);
            
        }
    }
}
