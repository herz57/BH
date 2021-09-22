using System.Net;

namespace BH.Client.Models
{
    public class ApiResponse
    {
        public HttpStatusCode StatusCode { get; set; }

        public bool IsSuccess { get; set; }

        public ApiResponse(bool isSuccess, HttpStatusCode statusCode)
        {
            StatusCode = statusCode;
            IsSuccess = isSuccess;
        }
    }

    public class ApiResponse<T> : ApiResponse
    {
        public T Entity { get; set; }

        public ApiResponse(bool isSuccess, HttpStatusCode statusCode, T entity)
            : base(isSuccess, statusCode)
        {
            Entity = entity;
        }
    }
}
