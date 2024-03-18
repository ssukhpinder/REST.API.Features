using Asp.Versioning;
using Microsoft.AspNetCore.Mvc;
using REST.API.EFCore.Interface;
using REST.API.EFCore.Models;
using REST.API.GlobalExceptions;
using System.Net;

namespace REST.API.Features.Controllers.v1
{
    /// <summary>
    /// Controller class contains API methods for ef core integration
    /// </summary>
    /// <param name="logger"></param>
    /// <param name="commonService"></param>
    [ApiVersion(1)]
    [Route("api/v{v:apiVersion}/ef/core")]
    [ApiController]
    public class EfCoreController(ILogger<EfCoreController> logger, ICommonService commonService) : ControllerBase
    {
        private readonly ILogger<EfCoreController> _logger = logger;
        private readonly ICommonService _commonService = commonService;

        /// <summary>
        /// Get list of rows available in sample table via EF Core
        /// </summary>
        /// <returns></returns>
        [Route("")]
        [HttpGet]
        [ProducesResponseType<ResponseMetaData<List<SampleTable>>>(StatusCodes.Status200OK)]
        [ProducesResponseType<ResponseMetaData<List<SampleTable>>>(StatusCodes.Status400BadRequest)]
        [ProducesResponseType<ResponseMetaData<List<SampleTable>>>(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> Get()
        {
            var responseMetadata = new ResponseMetaData<List<SampleTable>>()
            {
                Status = HttpStatusCode.OK,
                IsError = false,
                Result = await _commonService.GetSampleTables()
            };
            return StatusCode((int)responseMetadata.Status, responseMetadata);
        }
    }
}
