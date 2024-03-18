using Asp.Versioning;
using Microsoft.AspNetCore.Mvc;
using REST.API.Features.Constants;
using REST.API.GlobalExceptions;
using REST.API.GlobalExceptions.Constants;
using REST.API.GlobalExceptions.Exceptions;
using System.Net;

namespace REST.API.Features.Controllers.v1
{

    /// <summary>
    /// Controller class contains API methods for inbuild and custom exceptions
    /// </summary>
    /// <param name="logger"></param>
    [ApiController]
    [ApiVersion(1)]
    [Route("api/v{v:apiVersion}/global/exception")]
    public class GlobalExceptionController(ILogger<GlobalExceptionController> logger) : ControllerBase
    {
        private readonly ILogger<GlobalExceptionController> _logger = logger;

        /// <summary>
        /// GET API method to throw an inbuilt exception to be intercepted via GlobalExceptionHandling.cs class
        /// </summary>
        /// <returns></returns>
        /// <exception cref="NotImplementedException"></exception>
        [Route("invoke")]
        [HttpGet]
        [ProducesResponseType<ResponseMetaData<string>>(StatusCodes.Status200OK)]
        [ProducesResponseType<ResponseMetaData<string>>(StatusCodes.Status400BadRequest)]
        [ProducesResponseType<ResponseMetaData<string>>(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> Get()
        {
            _logger.LogInformation(CommonApiConstants.Info.GlobalException);
            int x = 1; int y = 0;
            _ = x / y;
            var responseMetadata = new ResponseMetaData<string>()
            {
                Status = HttpStatusCode.OK,
                IsError = false
            };
            return StatusCode((int)responseMetadata.Status, responseMetadata);
        }


        /// <summary>
        /// GET API method to throw an custom exception to be intercepted via CustomExceptionHandling.cs class
        /// </summary>
        /// <returns></returns>
        /// <exception cref="CustomApiException"></exception>
        [Route("invoke/custom")]
        [HttpGet]
        [ProducesResponseType<ResponseMetaData<string>>(StatusCodes.Status200OK)]
        [ProducesResponseType<ResponseMetaData<string>>(StatusCodes.Status400BadRequest)]
        [ProducesResponseType<ResponseMetaData<string>>(StatusCodes.Status500InternalServerError)]
        public Task<IActionResult> GetCustom()
        {
            _logger.LogInformation(CommonApiConstants.Info.CustomException);
            throw new CustomApiException(CommonExceptionConstants.CustomSomeUnknownError);
        }
    }
}
