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

        /// <summary>
        /// To get housing with his id
        /// </summary>
        /// <param name="id"></param>
        /// <response code="200">Return the new housing</response>
        /// <returns></returns>
        [HttpGet("{id:length(24)}")]
        public async Task<ActionResult<Housing>> GetHousing(string id)
        {
            var housing = await _housingsService.GetHousingAsync(id);
            return Ok(housing);
        }

        /// <summary>
        /// To register new housing
        /// </summary>
        /// <param name="toCreate"></param>
        /// <response code="200">Return the id of new housing</response>
        /// <response code="400">Return an error if the body has an error</response>
        /// <returns></returns>
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

        /// <summary>
        /// To update a housing
        /// </summary>
        /// <param name="id"></param>
        /// <param name="housingUpdateModel"></param>
        /// <response code="200">Return ok</response>
        /// <response code="400">Return bad response housing doesn't exist</response>
        /// <response code="403">Return bad response you are not authorize to do this</response>
        /// <returns></returns>
        [HttpPut("{id:length(24)}")]
        public async Task<IActionResult> UpdateHousing(string id, [FromBody] HousingUpdateModel housingUpdateModel)
        {
            var currentUserId = User.Identity.Name;

            try
            {
                await _housingsService.UpdateHousingAsync(currentUserId, id, housingUpdateModel);

                return Ok();
            }
            catch (UnauthorizedAccessException e)
            {
                return Unauthorized($"Can't update the housin: {e.Message}");
            }
            catch (Exception e)
            {
                return BadRequest($"Can't update the housing: {e.Message}");
            }
        }

    }
}
