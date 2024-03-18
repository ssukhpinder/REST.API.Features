using Asp.Versioning;
using Microsoft.AspNetCore.Mvc;
using REST.API.Features.Constants;
using REST.API.GlobalExceptions;

namespace REST.API.Features.Controllers.v2
{
    /// <summary>
    /// Controller class contains API methods to demonstrate versioning 
    /// </summary>
    /// <param name="logger"></param>
    [ApiController]
    [ApiVersion(2)]
    [Route("api/v{v:apiVersion}/[controller]")]
    public class VersionController(ILogger<VersionController> logger) : ControllerBase
    {
        private readonly ILogger<VersionController> _logger = logger;


        /// <summary>
        /// Api method to return success message from controller version 2
        /// </summary>
        /// <returns></returns>
        [Route("check")]
        [HttpGet]
        [ProducesResponseType<ResponseMetaData<string>>(StatusCodes.Status200OK)]
        [ProducesResponseType<ResponseMetaData<string>>(StatusCodes.Status400BadRequest)]
        [ProducesResponseType<ResponseMetaData<string>>(StatusCodes.Status500InternalServerError)]
        public IActionResult Get()
        {
            var responseMetadata = new ResponseMetaData<string>()
            {
                Status = System.Net.HttpStatusCode.OK,
                IsError = false,
                Message = CommonApiConstants.Responses.ResponseV1
            };
            return StatusCode((int)responseMetadata.Status, responseMetadata);
        }
    }
}
