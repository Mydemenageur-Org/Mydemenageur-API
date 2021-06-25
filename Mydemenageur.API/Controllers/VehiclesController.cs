using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Mydemenageur.API.Entities;
using Mydemenageur.API.Models.Vehicule;
using Mydemenageur.API.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Mydemenageur.API.Controllers
{
    [Route("api/[controller]")]
    [Authorize]
    [ApiController]
    public class VehiclesController : ControllerBase
    {

        private readonly IVehiclesService _vehiclesService;
        public VehiclesController(IVehiclesService VehiclesService,ISocietiesService societiesService)
        {
            _vehiclesService = VehiclesService;
        }

        /// <summary>
        /// To get specific vehicule
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        /// <response code="200">The vehicule was get</response>
        [HttpGet("{id:length(24)}")]
        public async Task<ActionResult<Vehicles>> GetVehicule(string id)
        {
            var vehicule = await _vehiclesService.GetVehiculeAsync(id);

            return Ok(vehicule);
        }

        /// <summary>
        /// To register a new vehicule
        /// </summary>
        /// <param name="toCreate"></param>
        /// <returns></returns>
        /// <response code="200">The vehicule was register</response>
        /// <response code="400">Bad request</response>
        [AllowAnonymous]
        [HttpPost]
        public async Task<ActionResult<string>> RegisterVehicule([FromBody] VehiclesRegisterModel toCreate)
        {
            try
            {
                string id = await _vehiclesService.AddVehiculeAsync(toCreate);

                return Ok(id);
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        /// <summary>
        /// To update a vehicule
        /// </summary>
        /// <param name="id"></param>
        /// <param name="vehiculeUpdateModel"></param>
        /// <returns></returns>
        /// <response code="200">The vehicule is updated</response>
        /// <response code="403">You are not authorize to update this vehicule</response>
        /// <response code="400">Bad request</response>
        [HttpPut("{id:length(24)}")]
        public async Task<IActionResult> UpdateVehicule(string id, [FromBody] VehiclesUpdateModel vehiculeUpdateModel)
        {
            var currentUserId = User.Identity.Name;

            try
            {
                await _vehiclesService.UpdateVehiculeAsync(currentUserId, id, vehiculeUpdateModel);

                return Ok();
            }
            catch(UnauthorizedAccessException e)
            {
                return Forbid($"Can't update the vehicule: {e.Message}");
            }
            catch (Exception e)
            {
                return BadRequest($"Can't update the vehicule: {e.Message}");
            }
        }

    }
}
