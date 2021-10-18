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
    public class DemenagementIndividuelController: ControllerBase
    {
        private readonly IDemenagementIndivService _demIndivService;

        public DemenagementIndividuelController(IDemenagementIndivService demIndivService)
        {
            _demIndivService = demIndivService;
        }

        /// <summary>
        /// To get all helps card
        /// </summary>
        /// <returns></returns>
        /// <response code="200">Return the list of the helps</response>returns></returns>
        [HttpGet]
        public async Task<ActionResult<IList<DemenagementIndivModel>>> GetDemenagements([FromQuery] bool askHelpStart, [FromQuery] bool askHelpDest, [FromQuery] bool isFlexibleDate, [FromQuery] string fromCity = null, [FromQuery] string toCity = null, [FromQuery] string personNeeded = null, [FromQuery] string volume = null, [FromQuery] string serviceType = null, [FromQuery] string demenagementType = null, [FromQuery] Nullable<DateTime> date = null, [FromQuery] int size = 0)
        {
            return Ok(await _demIndivService.GetAllDemenagement(date, askHelpStart,askHelpDest,isFlexibleDate,fromCity, toCity,personNeeded,volume,serviceType,demenagementType,size));
        }

        /// <summary>
        /// To create an announce
        /// </summary>
        /// <returns></returns>
        /// <response code="200">Return the id of the demenagement announce created</response>returns></returns>
        [AllowAnonymous]
        [HttpPost]
        public async Task<ActionResult<string>> CreateDemenagementPro(DemenagementIndivModel demPro)
        {
            return Ok(await _demIndivService.CreateDemenagementIndividuel(demPro));
        }

        /// <summary>
        /// To update a demenagement announce
        /// </summary>
        /// <returns></returns>
        [HttpPut("{id:length(24)}")]
        public async Task<IActionResult> UpdateDemenagementIndiv(string id, DemenagementIndivModel demIndiv)
        {
            try
            {
                await _demIndivService.UpdateDemenagementIndividuel(id, demIndiv);
            }
            catch (Exception err)
            {
                return BadRequest(err);
            }

            return Ok();
        }

        /// <summary>
        /// To delete a demenagement announce
        /// </summary>
        /// <returns></returns>
        [HttpDelete("{id:length(24)}")]
        public async Task<IActionResult> DeleteDemenagementIndiv(string id)
        {
            try
            {
                await _demIndivService.DeleteDemenagementIndividuel(id);
            }
            catch (Exception err)
            {
                return BadRequest(err);
            }

            return Ok();
        }
    }
}
