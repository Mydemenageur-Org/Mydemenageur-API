using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Mydemenageur.API.Entities;
using Mydemenageur.API.Models.Society;
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
    public class SocietiesController : ControllerBase
    {
        private readonly ISocietiesService _societiesService;
        public SocietiesController(ISocietiesService societiesService)
        {
            _societiesService = societiesService;
        }

        /// <summary>
        /// To get society with his id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet("{id:length(24)}")]
        public async Task<ActionResult<Society>> GetSociety(string id)
        {
            var society = await _societiesService.GetSocietyAsync(id);

            return Ok(society);
        }

        /// <summary>
        /// To get all movers of society
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet("{id:length(24)}/movers")]
        public async Task<ActionResult<List<Mover>>> GetAllMoverFromSociety(string id)
        {
            var movers = await _societiesService.GetAllMoverAsync(id);

            return Ok(movers);
        }

        /// <summary>
        /// To register a new society
        /// </summary>
        /// <param name="societyRegisterModel"></param>
        /// <returns></returns>
        [AllowAnonymous]
        [HttpPost]
        public async Task<ActionResult<string>> RegisterToDatabase([FromBody] SocietyRegisterModel societyRegisterModel)
        {
            try
            {
                string id = await _societiesService.RegisterSocietyAsync(societyRegisterModel);
                return Ok(id);
            }
            catch(Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        /// <summary>
        /// To update the society
        /// </summary>
        /// <param name="id"></param>
        /// <param name="societyUpdateModel"></param>
        /// <returns></returns>
        [HttpPut("{id:length(24)}")]
        public async Task<IActionResult> UpdateSociety(string id, [FromBody] SocietyUpdateModel societyUpdateModel)
        {
            var currentMoverId = User.Identity.Name;
            var society = await _societiesService.GetSocietyAsync(id);

            try
            {
                if (currentMoverId != society.ManagerId)
                {
                    return Forbid("You can't edit that society : you are not the manager of society");
                }

                await _societiesService.UpdateSocietyAsync(id, societyUpdateModel);

                return Ok();
            }
            catch (Exception e)
            {
                return BadRequest($"Can't update the society: {e.Message}");
            }
        }
    }
}
