using REST.API.GlobalExceptions.Constants;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

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
        public CustomApiException(string message, Exception ex) : base($"{CommonException.CustomExceptionPrefix} {message}", ex) { }
        public CustomApiException(HttpStatusCode statusCode, string message) : base($"{CommonException.CustomExceptionPrefix} {message}")
        {
            _statusCode = statusCode;
        }
        public CustomApiException(HttpStatusCode statusCode, Exception ex) : base($"{CommonException.CustomExceptionPrefix} {ex.Message}")
        {
            _statusCode = statusCode;
            _exceptionStrckTrace = ex.StackTrace;
        }
        public CustomApiException(HttpStatusCode statusCode, string message, Exception ex) : base($"{CommonException.CustomExceptionPrefix} {message}")
        {
            _statusCode = statusCode;
            _exceptionStrckTrace = ex.StackTrace;
        }
    }
}
