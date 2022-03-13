using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Mydemenageur.BLL.Services.Interfaces;
using Mydemenageur.DAL.Models.GenericService;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Mydemenageur.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
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
        /// <param name="fields"></param>
        /// <returns></returns>
        [HttpGet("{id:length(24)}")]
        public async Task<ActionResult<GenericServicePopulated>> GetGenericService(string id, [FromQuery] string fields)
        {
            IList<string> fieldsList = fields != null ? fields.Split(',') : new List<string>();
            GenericServicePopulated genericService = await _genericServicesService.GetGenericService(id, fieldsList);

            if (genericService == null)
            {
                return NotFound();
            }

            return Ok(genericService);
        }

        /// <summary>
        /// Get all generic services. Can be filtered by name
        /// </summary>
        /// <param name="pageNumber"></param>
        /// <param name="numberOfElementsPerPage"></param>
        /// <returns></returns>
        [HttpGet]
        public async Task<ActionResult<IList<GenericService>>> GetGenericServices([FromQuery] int pageNumber = -1, [FromQuery] int numberOfElementsPerPage = -1)
        {
            var queryParams = HttpContext.Request.Query;
            IList<GenericService> genericServices = await _genericServicesService.GetGenericServices(queryParams, pageNumber, numberOfElementsPerPage);

            return Ok(genericServices);
        }

        /// <summary>
        /// Get the number of generic services, Can be filtered by name
        /// </summary>
        /// <returns></returns>
        [HttpGet("count")]
        public async Task<ActionResult<int>> GetGenericServicesCount()
        {
            var queryParams = HttpContext.Request.Query;
            long count = await _genericServicesService.GetGenericServicesCount(queryParams);

            return Ok(count);
        }

        /// <summary>
        /// Get a service by name
        /// </summary>
        /// <param name="name"></param>
        /// <returns></returns>
        [HttpGet("base")]
        public async Task<ActionResult<GenericService>> GetBaseGenericService([FromQuery] string name)
        {
            GenericService genericService = await _genericServicesService.GetBaseGenericService(name);

            if (genericService == null)
            {
                return NotFound();
            }

            return Ok(genericService);
        }

        /// <summary>
        /// Create a generic service
        /// </summary>
        /// <param name="toCreate"></param>
        /// <returns></returns>
        [HttpPost]
        public async Task<ActionResult<GenericService>> CreateGenericService([FromBody] GenericService toCreate)
        {
            try
            {
                GenericService service = await _genericServicesService.CreateGenericService(toCreate);

                return Ok(service);
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

        /// <summary>
        /// Delete a generic service
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpPut("{id:length(24)}/delete")]
        public async Task<IActionResult> DeleteGenericServide(string id)
        {
            try
            {
                await _genericServicesService.DeleteGenericService(id);

                return Ok();
            }
            catch (Exception e)
            {
                return BadRequest($"Error during the request: {e.Message}");
            }
        }
    }
}
