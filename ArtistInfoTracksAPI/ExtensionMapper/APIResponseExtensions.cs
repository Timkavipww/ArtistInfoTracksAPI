using ArtistInfoTracksAPI.Models;
using Microsoft.Extensions.Diagnostics.HealthChecks;
using System.Data.Common;
using System.Net;

namespace ArtistInfoTracksAPI.ExtensionMapper
{
    public static class APIResponseExtensions
    {
        public static APIResponse Success(this APIResponse response)
        {
            response.isSuccess = true;
            response.StatusCode = HttpStatusCode.OK;
            return response;
        }
        public static APIResponse Success(this APIResponse response, object entity)
        {
            response.Result = entity;
            response.isSuccess = true;
            response.StatusCode = HttpStatusCode.OK;
            return response;
        }
        public static APIResponse NotFound(this APIResponse response)
        {
            response.isSuccess = false;
            response.StatusCode = HttpStatusCode.NotFound;
            return response;
        }
        public static APIResponse dbException(this APIResponse response, DbException dbEx)
        {
            response.Result += "db error";
            response.ErrorMessages.Add($"{dbEx.Message}");
            return response;
        } 
        public static APIResponse fatalException(this APIResponse response, Exception ex)
        {
            response.Result += "fatal error";
            response.ErrorMessages.Add($"{ex.Message}");
            return response;
        }
        public static APIResponse Created(this APIResponse response)
        {
            response.isSuccess = true;
            response.StatusCode = HttpStatusCode.Created;
            return response;
        }
        public static APIResponse Created(this APIResponse response, object entity)
        {
            response.isSuccess = true;
            response.StatusCode = HttpStatusCode.Created;
            response.Result = entity;
            return response;
        }
        


    }
}
