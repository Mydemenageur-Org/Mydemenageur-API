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

        [HttpGet("{id:length(24)}")]
        public async Task<ActionResult<Vehicules>> GetVehicule(string id)
        {
            var vehicule = await _vehiculesService.GetVehiculeAsync(id);

            return Ok(vehicule);
        }

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

        [HttpPut("{id:length(24)}")]
        public async Task<IActionResult> UpdateVehicule(string id, [FromBody] VehiculeUpdateModel vehiculeUpdateModel)
        {
            var currentMoverId = User.Identity.Name;

            try
            {
                if (currentMoverId != id)
                {
                    return Forbid("You can't edit that vehicule : you are not the vehicule you want to edit");
                }

                await _vehiculesService.UpdateVehiculeAsync(id, vehiculeUpdateModel);

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
