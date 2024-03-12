using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using REST.API.GlobalExceptions;
using REST.API.GlobalExceptions.Constants;
using REST.API.GlobalExceptions.Exceptions;
using System.Net;

namespace REST.API.Features.Controllers
{
    /// <summary>
    /// Controller class contains API methods for inbuild and custom exceptions
    /// </summary>
    [Route("api/global/exception")]
    [ApiController]
    public class GlobalExceptionController : ControllerBase
    {
        public GlobalExceptionController()
        {

        }

        /// <summary>
        /// GET API method to throw an inbuilt exception to be intercepted via GlobalExceptionHandling.cs class
        /// </summary>
        /// <returns></returns>
        /// <exception cref="NotImplementedException"></exception>
        [Route("invoke")]
        [HttpGet]
        [ProducesResponseType<string>(StatusCodes.Status200OK)]
        [ProducesResponseType<string>(StatusCodes.Status400BadRequest)]
        [ProducesResponseType<string>(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> Get()
        {
            int x = 1; int y = 0;
            int z = x / y;
            var responseMetadata = new ResponseMetaData<string>()
            {
                Status = System.Net.HttpStatusCode.OK,
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
        public Task<IActionResult> GetCustom()
        {
            throw new CustomApiException(CommonException.CustomSomeUnknownError);
        }
    }
}
