using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Mydemenageur.DAL.Entities;
using Mydemenageur.DAL.Models.Services;
using Mydemenageur.BLL.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace Mydemenageur.API.Controllers
{
    [Route("api/[controller]")]
    [Authorize]
    [ApiController]
    public class DemenagementProController: ControllerBase
    {
        private readonly IDemenagementProService _demProService;

        public DemenagementProController(IDemenagementProService demProService)
        {
            _demProService = demProService;
        }

        /// <summary>
        /// To get all demenagement card
        /// </summary>
        /// <returns></returns>
        /// <response code="200">Return the list of the demenagement</response>returns></returns>
        [HttpGet]
        public async Task<ActionResult<IList<DemenagementProModel>>> GetDemenagementsPro([FromQuery] bool isStartingDateKnown, [FromQuery] bool isHouse, [FromQuery] bool hasMultipleFloors, [FromQuery] Nullable<DateTime> date = null, [FromQuery] string startZipCode = null, [FromQuery] string startAddr = null, [FromQuery] string fromCity = null, [FromQuery] string endZipCode = null, [FromQuery] string endAddr = null, [FromQuery] string toCity = null, [FromQuery] string volume = null, [FromQuery] string surface = null, [FromQuery] string serviceType = null, [FromQuery] string demenagementType = null, [FromQuery] int size = 0)
        {
            return Ok(await _demProService.GetAllDemenagement(date, isStartingDateKnown, isHouse, hasMultipleFloors, startZipCode, startAddr, fromCity, endZipCode, endAddr, toCity, volume, surface, serviceType, demenagementType, size));
        }

        /// <summary>
        /// To create an announce
        /// </summary>
        /// <returns></returns>
        /// <response code="200">Return the id of the demenagement announce created</response>returns></returns>
        [AllowAnonymous]
        [HttpPost]
        public async Task<ActionResult<string>> CreateDemenagementPro(DemenagementProModel demPro)
        {
            return Ok(await _demProService.CreateDemenagementPro(demPro));
        }

        /// <summary>
        /// To update a demenagement announce
        /// </summary>
        /// <returns></returns>
        [HttpPut("{id:length(24)}")]
        public async Task<IActionResult> UpdateDemenagementPro(string id, DemenagementProModel demPro)
        {
            try
            {
                await _demProService.UpdateDemenagementPro(id, demPro);
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
        public async Task<IActionResult> DeleteDemenagementPro(string id)
        {
            try
            {
                await _demProService.DeleteDemenagementPro(id);
            }
            catch (Exception err)
            {
                return BadRequest(err);
            }

            return Ok();
        }
    }
}
