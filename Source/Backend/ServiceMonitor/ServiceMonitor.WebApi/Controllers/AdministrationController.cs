﻿using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using ServiceMonitor.Core.BusinessLayer.Contracts;
using ServiceMonitor.WebApi.Requests;
using ServiceMonitor.WebApi.Responses;

namespace ServiceMonitor.WebApi.Controllers
{
#pragma warning disable CS1591
    [Route("api/v1/[controller]")]
    [ApiController]
    public class AdministrationController : ControllerBase
    {
        protected ILogger Logger;
        protected IAdministrationService Service;

        public AdministrationController(ILogger<AdministrationController> logger, IAdministrationService service)
        {
            Logger = logger;
            Service = service;
        }
#pragma warning restore CS1591

        /// <summary>
        /// Saves a result from service watch action
        /// </summary>
        /// <param name="request">Service status result</param>
        /// <returns>Ok if save it was successfully, Not found if service not exists else server internal error</returns>
        [HttpPost("ServiceEnvironmentStatusLog")]
        [ProducesResponseType(200)]
        [ProducesResponseType(404)]
        [ProducesResponseType(500)]
        public async Task<IActionResult> PostServiceStatusLogAsync([FromBody]ServiceEnvironmentStatusLogRequest request)
        {
            Logger?.LogDebug("'{0}' has been invoked", nameof(PostServiceStatusLogAsync));

            var response = await Service
                .CreateServiceEnvironmentStatusLogAsync(request.ToEntity(), request.ServiceEnvironmentID);

            return response.ToHttpResponse();
        }
    }
}
