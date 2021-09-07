using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Mydemenageur.API.Entities;
using Mydemenageur.API.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Mydemenageur.API.Controllers
{
    [Route("api/[controller]")]
    [Authorize]
    [ApiController]
    public class ServiceController: ControllerBase
    {
        private readonly IServiceProposedService _serviceProposedService;

        public ServiceController(IServiceProposedService serviceProposedService)
        {
            _serviceProposedService = serviceProposedService;
        }

        /// <summary>
        /// To get all services proposed
        /// </summary>
        /// <returns></returns>
        /// <response code="200">Return the list of services</response>returns></returns>
        [HttpGet]
        public async Task<ActionResult<IList<Service>>> GetReviews()
        {
            return Ok(await _serviceProposedService.GetAllServices());
        }

        /// <summary>
        /// To get a service with an id
        /// </summary>
        /// <returns></returns>
        /// <response code="200">Return a specific service by an id</response>returns></returns>
        [HttpGet("{id:length(24)}")]
        public async Task<ActionResult<Review>> GetServiceById(string id)
        {
            if (String.IsNullOrEmpty(id))
            {
                return BadRequest("Id is null or empty");
            }

            return Ok(await _serviceProposedService.GetServiceById(id));
        }

        /// <summary>
        /// To create a service
        /// </summary>
        /// <returns></returns>
        /// <response code="200">Return the id of the service created</response>returns></returns>
        /// [AllowAnonymous]
        [HttpPost]
        public async Task<ActionResult<string>> CreateService(string label, byte file)
        {
            if (string.IsNullOrEmpty(label))
            {
                return BadRequest("You must enter params");
            }

            return Ok(await _serviceProposedService.CreateService(label, file));
        }

        /// <summary>
        /// To update a service
        /// </summary>
        /// <returns></returns>
        [HttpPut("{id:length(24)}")]
        public async Task<IActionResult> UpdateReview(string id, string label, byte file)
        {
            try
            {
                await _serviceProposedService.UpdateService(id, label, file);
            }
            catch (Exception err)
            {
                return BadRequest(err);
            }

            return Ok();
        }

        /// <summary>
        /// To delete a service
        /// </summary>
        /// <returns></returns>
        [HttpDelete("{id:length(24)}")]
        public async Task<IActionResult> DeleteReview(string id)
        {
            try
            {
                await _serviceProposedService.DeleteService(id);
            }
            catch (Exception err)
            {
                return BadRequest(err);
            }

            return Ok();
        }
    }
}
