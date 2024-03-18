using Asp.Versioning;
using Microsoft.AspNetCore.Mvc;
using REST.API.Features.Constants;
using REST.API.Features.Models;
using REST.API.GlobalExceptions;
using System.ComponentModel.DataAnnotations;

namespace REST.API.Features.Controllers.v1
{

    /// <summary>
    /// Controller class contains API methods to demonstrate Model Validation 
    /// </summary>
    /// <param name="logger"></param>
    [ApiController]
    [ApiVersion(1)]
    [Route("api/v{v:apiVersion}/model")]
    public class ModelValidationController(ILogger<ModelValidationController> logger) : ControllerBase
    {
        private readonly ILogger<ModelValidationController> _logger = logger;

        /// <summary>
        /// POST method to demonstrate global API validation 
        /// </summary>
        /// <remarks>
        /// 
        /// Sample Request:
        /// 
        ///     POST
        ///     {
        ///       "firstName": "",
        ///       "lastName": ""
        ///     }
        /// </remarks>
        /// <param name="modelValidationExample"></param>
        /// <returns></returns>
        [Route("validate")]
        [HttpPost]
        [ProducesResponseType<ResponseMetaData<string>>(StatusCodes.Status200OK)]
        [ProducesResponseType<ResponseMetaData<string>>(StatusCodes.Status400BadRequest)]
        [ProducesResponseType<ResponseMetaData<string>>(StatusCodes.Status500InternalServerError)]
        public IActionResult PostData([FromBody][Required] ModelValidationExample modelValidationExample)
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
