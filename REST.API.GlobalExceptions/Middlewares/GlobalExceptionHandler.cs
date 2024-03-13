using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using REST.API.GlobalExceptions.Constants;
using REST.API.GlobalExceptions.Exceptions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace REST.API.GlobalExceptions.Middlewares
{
    /// <summary>
    /// Global middleware which will be invoked if any exception is triggered within the application
    /// </summary>
    public class GlobalExceptionHandler
    {
        private readonly RequestDelegate _next;
        private readonly ILogger<GlobalExceptionHandler> _logger;

        public GlobalExceptionHandler(
            RequestDelegate next,
            ILogger<GlobalExceptionHandler> logger)
        {
            _next = next;
            _logger = logger;
        }

        public async Task InvokeAsync(HttpContext context)
        {
            try
            {
                await _next(context);
            }
            catch (Exception exception)
            {
                ResponseMetaData<string> responseMetadata = new()
                {
                    Status = HttpStatusCode.InternalServerError,
                    IsError = true,
                    ErrorDetails = CommonExceptionConstants.SomeUnknownError
                };

                var serializedResponseMetadata = JsonConvert.SerializeObject(responseMetadata);
                _logger.LogError(exception, "Exception occurred: {Message}", JsonConvert.SerializeObject(serializedResponseMetadata));

                context.Response.StatusCode = StatusCodes.Status500InternalServerError;
                context.Response.ContentType = "application/json";
                await context.Response.WriteAsync(serializedResponseMetadata);
            }
        }

    }
}
