using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Mydemenageur.DAL.Models.Demarche;
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
    public class DemarcheController: ControllerBase
    {
        private readonly IDemarcheService _demarcheService;

        public DemarcheController(IDemarcheService demarcheService)
        {
            _demarcheService = demarcheService;
        }

        /// <summary>
        /// To get all demarche card
        /// </summary>
        /// <returns></returns>
        /// <response code="200">Return the list of the demarche</response>returns></returns>
        [HttpGet]
        public async Task<ActionResult<IList<DemarcheModel>>> GetAllDemarches([FromQuery] Nullable<DateTime> dateMove = null, [FromQuery] bool hasAlreadyMoved = false, [FromQuery] bool energyNotification = false, [FromQuery] bool boxAndMobileNotification = false, [FromQuery] bool assurance = false, [FromQuery] bool alarmAndMonitors = false, [FromQuery] bool bankAccount = false, [FromQuery] bool grayCard = false, [FromQuery] bool devisDemenagement = false, [FromQuery] int size = 0)
        {
            return Ok(await _demarcheService.GetAllDemarches(dateMove, hasAlreadyMoved, energyNotification, boxAndMobileNotification,assurance,alarmAndMonitors,bankAccount,grayCard, devisDemenagement, size));
        }

        /// <summary>
        /// To create an announce
        /// </summary>
        /// <returns></returns>
        /// <response code="200">Return the id of the demarche announce created</response>returns></returns>
        [AllowAnonymous]
        [HttpPost]
        public async Task<ActionResult<string>> CreateDemarche(DemarcheModel demPro)
        {
            return Ok(await _demarcheService.CreateDemarche(demPro));
        }

        /// <summary>
        /// To update a demarche announce
        /// </summary>
        /// <returns></returns>
        [HttpPut("{id:length(24)}")]
        public async Task<IActionResult> UpdateDemarche(string id, DemarcheModel demPro)
        {
            try
            {
                await _demarcheService.UpdateDemarche(id, demPro);
            }
            catch (Exception err)
            {
                return BadRequest(err);
            }

            return Ok();
        }

        /// <summary>
        /// To delete a demarche announce
        /// </summary>
        /// <returns></returns>
        [HttpDelete("{id:length(24)}")]
        public async Task<IActionResult> DeleteDemarche(string id)
        {
            try
            {
                await _demarcheService.DeleteDemarche(id);
            }
            catch (Exception err)
            {
                return BadRequest(err);
            }

            return Ok();
        }
    }
}
