using CinemaAPI.Core.CustomEntities;

namespace CinemaAPI.Api.Responses
{
    public class ApiResponse<T>
    {
        public T Data { get; set; }

        public Metadata Meta { get; set; }
    
        public ApiResponse(T data)
        {
            Data = data;
        }
    
    }
}
