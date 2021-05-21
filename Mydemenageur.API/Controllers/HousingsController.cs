using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Mydemenageur.API.Entities;
using Mydemenageur.API.Models.Housing;
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
    public class HousingsController : ControllerBase
    {

        private readonly IHousingsService _housingsService;
        public HousingsController(IHousingsService housingsService)
        {
            _housingsService = housingsService;
        }

        [HttpGet("{id:length(24)}")]
        public async Task<ActionResult<Housing>> GetHousing(string id)
        {
            var housing = await _housingsService.GetHousingAsync(id);
            return Ok(housing);
        }

        [AllowAnonymous]
        [HttpPost]
        public async Task<ActionResult<string>> RegisterHousing([FromBody] HousingRegisterModel toCreate)
        {
            try
            {
                string id = await _housingsService.RegisterHousingAsync(toCreate);

                return Ok(id);
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        [HttpPut("{id:length(24)}")]
        public async Task<IActionResult> UpdateHousing(string id, [FromBody] HousingUpdateModel housingUpdateModel)
        {
            var currentMoverId = User.Identity.Name;

            try
            {
                if (currentMoverId != id)
                {
                    return Forbid("You can't edit that housing : you are not the housing you want to edit");
                }

                await _housingsService.UpdateHousingAsync(currentMoverId, id, housingUpdateModel);

                return Ok();
            }
            catch (Exception e)
            {
                return BadRequest($"Can't update the housing: {e.Message}");
            }
        }

    }
}
