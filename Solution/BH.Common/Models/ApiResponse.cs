using System.Net;

namespace BH.Common.Models
{
    public class ApiResponse
    {
        public HttpStatusCode StatusCode { get; set; } = HttpStatusCode.OK;

        public bool IsSuccess { get; set; } = true;


        public ApiResponse()
        {
        }

        public ApiResponse(bool isSuccess, HttpStatusCode statusCode)
        {
            StatusCode = statusCode;
            IsSuccess = isSuccess;
        }
    }

    public class ApiResponse<T> : ApiResponse
    {
        public T Content { get; set; }


        public ApiResponse()
            : base()
        {
        }

        public ApiResponse(bool isSuccess, HttpStatusCode statusCode)
            : base(isSuccess, statusCode)
        {
        }

        public ApiResponse(bool isSuccess, HttpStatusCode statusCode, T content)
            : base(isSuccess, statusCode)
        {
            Content = content;
        }

        public ApiResponse(T content)
        {
            Content = content;
        }
    }
}
