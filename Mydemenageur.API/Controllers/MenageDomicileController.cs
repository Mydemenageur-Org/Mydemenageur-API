using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Mydemenageur.API.Models.Services;
using Mydemenageur.API.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Mydemenageur.API.Controllers
{
    [Route("api/[controller]")]
    [Authorize]
    [ApiController]
    public class MenageDomicileController: ControllerBase
    {
        private readonly IMenageDomicileService _menage;

        public MenageDomicileController(IMenageDomicileService menage)
        {
            _menage = menage;
        }

        /// <summary>
        /// To get all menages card
        /// </summary>
        /// <returns></returns>
        /// <response code="200">Return the list of the menage</response>returns></returns>
        [HttpGet]
        public async Task<ActionResult<IList<MenageRepassageModel>>> GetAllMenage([FromQuery] string surface = null, [FromQuery] string frequency = null, [FromQuery] Nullable<DateTime> dateEvent = null, [FromQuery] string budget = null, [FromQuery] string menageType = null, [FromQuery] bool isPro = false, [FromQuery] int size = 0)
        {
            return Ok(await _menage.GetAllMenageDomicile(surface, frequency, dateEvent, budget, menageType, isPro, size));
        }

        /// <summary>
        /// To create an menage
        /// </summary>
        /// <returns></returns>
        /// <response code="200">Return the id of the menage announce created</response>returns></returns>
        [AllowAnonymous]
        [HttpPost]
        public async Task<ActionResult<string>> CreateDemenagementPro(MenageDomicileModel men)
        {
            return Ok(await _menage.CreateMenageDomicile(men));
        }

        /// <summary>
        /// To update a menagee announce
        /// </summary>
        /// <returns></returns>
        [HttpPut("{id:length(24)}")]
        public async Task<IActionResult> UpdateDemenagementIndiv(string id, MenageDomicileModel men)
        {
            try
            {
                await _menage.UpdateMenageDomicile(id, men);
            }
            catch (Exception err)
            {
                return BadRequest(err);
            }

            return Ok();
        }

        /// <summary>
        /// To delete a menage announce
        /// </summary>
        /// <returns></returns>
        [HttpDelete("{id:length(24)}")]
        public async Task<IActionResult> DeleteDemenagementIndiv(string id)
        {
            try
            {
                await _menage.DeleteMenageDomicile(id);
            }
            catch (Exception err)
            {
                return BadRequest(err);
            }

            return Ok();
        }
    }
}
