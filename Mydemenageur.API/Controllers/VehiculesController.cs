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
    public class VehiculesController : ControllerBase
    {

        private readonly IVehiculesService _vehiculesService;
        private readonly ISocietiesService _societiesService;
        public VehiculesController(IVehiculesService vehiculesService,ISocietiesService societiesService)
        {
            _vehiculesService = vehiculesService;
            _societiesService = societiesService;
        }

        /// <summary>
        /// To get specific vehicule
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        /// <response code="200">The vehicule was get</response>
        [HttpGet("{id:length(24)}")]
        public async Task<ActionResult<Vehicules>> GetVehicule(string id)
        {
            var vehicule = await _vehiculesService.GetVehiculeAsync(id);

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
        public async Task<ActionResult<string>> RegisterVehicule([FromBody] VehiculesRegisterModel toCreate)
        {
            try
            {
                string id = await _vehiculesService.AddVehiculeAsync(toCreate);

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
        public async Task<IActionResult> UpdateVehicule(string id, [FromBody] VehiculesUpdateModel vehiculeUpdateModel)
        {
            var currentMoverId = User.Identity.Name;

            try
            {
                if (currentMoverId != id)
                {
                    return Forbid("You can't edit that vehicule : you are not the vehicule you want to edit");
                }

                await _vehiculesService.UpdateVehiculeAsync(currentMoverId, id, vehiculeUpdateModel);

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
