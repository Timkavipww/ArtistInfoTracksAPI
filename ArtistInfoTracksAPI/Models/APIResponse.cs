using System.Net;

namespace ArtistInfoTracksAPI.Models
{
    public class APIResponse
    {
        public bool isSuccess { get; set; }
        public Object Result { get; set; }
        public HttpStatusCode StatusCode { get; set; }
        public List<string>ErrorMessages { get; set; }

        public APIResponse() 
        {
            ErrorMessages = new List<string>();
        }
    }
}
