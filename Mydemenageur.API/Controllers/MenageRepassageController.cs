using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Mydemenageur.DAL.Models.Services;
using Mydemenageur.BLL.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Mydemenageur.API.Controllers
{
    [Route("api/[controller]")]
    [Authorize]
    [ApiController]
    public class MenageRepassageController: ControllerBase
    {
        private readonly IMenageRepassageService _menage;

        public MenageRepassageController(IMenageRepassageService menage)
        {
            _menage = menage;
        }

        /// <summary>
        /// To get all menages card
        /// </summary>
        /// <returns></returns>
        /// <response code="200">Return the list of the menage</response>returns></returns>
        [HttpGet]
        public async Task<ActionResult<IList<MenageRepassageModel>>> GetAllMenage([FromQuery]string clotheNumber = null, [FromQuery] string frequency = null, [FromQuery] Nullable<DateTime> dateEvent = null, [FromQuery] string budget = null, [FromQuery] string menageType = null, [FromQuery] bool isPro = false, [FromQuery] int size = 0)
        {
            return Ok(await _menage.GetAllMenage(clotheNumber, frequency, dateEvent, budget, menageType, isPro , size));
        }

        /// <summary>
        /// To create an menage
        /// </summary>
        /// <returns></returns>
        /// <response code="200">Return the id of the menage announce created</response>returns></returns>
        [AllowAnonymous]
        [HttpPost]
        public async Task<ActionResult<string>> CreateDemenagementPro(MenageRepassageModel men)
        {
            return Ok(await _menage.CreateMenageDomicile(men));
        }

        /// <summary>
        /// To update a menagee announce
        /// </summary>
        /// <returns></returns>
        [HttpPut("{id:length(24)}")]
        public async Task<IActionResult> UpdateDemenagementIndiv(string id, MenageRepassageModel men)
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
