using System.Net;

namespace REST.API.GlobalExceptions
{
    /// <summary>
    /// Common wrapper class for all API response model where T can be a simple or complex data type
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public class ResponseMetaData<T>
    {
        public HttpStatusCode Status { get; set; }
        public string? Message { get; set; }
        public bool IsError { get; set; }
        public string? ErrorDetails { get; set; }
        public string? CorrealtionId { get; set; }
        public T? Result { get; set; }
    }
}
