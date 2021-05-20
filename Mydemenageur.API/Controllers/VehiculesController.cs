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

        /* A Finir
        [HttpPut("{id:length(24)}")]
        public async Task<IActionResult> UpdateVehicule(string id, [FromBody] VehiculeUpdateModel vehiculeUpdateModel)
        {
            var currentMoverId = User.Identity.Name;
            //var society = await _s

            try
            {
                if (currentMoverId != id)
                {
                    return Forbid("You can't edit that mover : you are not the mover you want to edit");
                }

                await _moversService.UpdateMoverAsync(id, moverUpdateModel);

                return Ok();
            }
            catch (Exception e)
            {
                return BadRequest($"Can't update the mover: {e.Message}");
            }
        }*/

    }
}
