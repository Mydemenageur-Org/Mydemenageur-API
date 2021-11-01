using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Mydemenageur.BLL.Services.Interfaces;
using Mydemenageur.DAL.Models.GenericService;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Mydemenageur.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class GenericServicesController : ControllerBase
    {
        private readonly IGenericServicesService _genericServicesService;

        public GenericServicesController(IGenericServicesService genericServicesService)
        {
            _genericServicesService = genericServicesService;
        }

        /// <summary>
        /// Get a service by ID
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet("{id:length(24)}")]
        public async Task<ActionResult<GenericService>> GetGenericService(string id)
        {
            GenericService genericService = await _genericServicesService.GetGenericService(id);

            if (genericService == null)
            {
                return NotFound();
            }

            return Ok(genericService);
        }

        /// <summary>
        /// Get all generic services. Can be filtered by name
        /// </summary>
        /// <param name="name"></param>
        /// <returns></returns>
        [HttpGet]
        public async Task<ActionResult<IList<GenericService>>> GetGenericServices([FromQuery] string name)
        {
            IList<GenericService> genericServices = await _genericServicesService.GetGenericServices(name);

            return Ok(genericServices);
        }

        /// <summary>
        /// Create a generic service
        /// </summary>
        /// <param name="toCreate"></param>
        /// <returns></returns>
        [HttpPost]
        public async Task<ActionResult<string>> CreateGenericService([FromBody] GenericService toCreate)
        {
            try
            {
                string id = await _genericServicesService.CreateGenericService(toCreate);

                return Ok(id);
            }
            catch (Exception e)
            {
                return BadRequest($"Error during the request: {e.Message}");
            }
        }

        [HttpPut("{id:length(24)}")]
        public async Task<IActionResult> UpdateGenericService(string id, [FromBody] GenericService toUpdate)
        {
            if (toUpdate.Id != id)
            {
                return BadRequest("The id of the generic service doesn't match the resource id");
            }

            // TODO: improve security here
            try
            {
                await _genericServicesService.UpdateGenericService(toUpdate);

                return Ok();
            }
            catch (Exception e)
            {
                return BadRequest($"Error during the request: {e.Message}");
            }
        }
    }
}
