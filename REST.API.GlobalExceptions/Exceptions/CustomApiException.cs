using REST.API.GlobalExceptions.Constants;
using System.Net;

namespace REST.API.GlobalExceptions.Exceptions
{
    /// <summary>
    /// Custom exception class wherein we can update the attributes of base class "Exception"
    /// </summary>   
    public class CustomApiException : Exception
    {
        private readonly HttpStatusCode _statusCode;
        private readonly string? _exceptionStrckTrace;

        public HttpStatusCode StatusCode => _statusCode;
        public string? ExceptionStackTrace => _exceptionStrckTrace;

        public CustomApiException() { }
        public CustomApiException(string message) : base(message) { }
        public CustomApiException(string message, Exception ex) : base($"{CommonExceptionConstants.CustomExceptionPrefix} {message}", ex) { }
        public CustomApiException(HttpStatusCode statusCode, string message) : base($"{CommonExceptionConstants.CustomExceptionPrefix} {message}")
        {
            _statusCode = statusCode;
        }
        public CustomApiException(HttpStatusCode statusCode, Exception ex) : base($"{CommonExceptionConstants.CustomExceptionPrefix} {ex.Message}")
        {
            _statusCode = statusCode;
            _exceptionStrckTrace = ex.StackTrace;
        }
        public CustomApiException(HttpStatusCode statusCode, string message, Exception ex) : base($"{CommonExceptionConstants.CustomExceptionPrefix} {message}")
        {
            _statusCode = statusCode;
            _exceptionStrckTrace = ex.StackTrace;
        }
    }
}
