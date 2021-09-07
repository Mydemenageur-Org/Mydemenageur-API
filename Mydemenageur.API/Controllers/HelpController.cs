using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Mydemenageur.API.Entities;
using Mydemenageur.API.Models.Demands;
using Mydemenageur.API.Services.Interfaces;
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
    public class HelpController: ControllerBase
    {
        private readonly IHelpService _helpService;

        public HelpController(IHelpService helpService)
        {
            _helpService = helpService;
        }

        /// <summary>
        /// To get all helps card
        /// </summary>
        /// <returns></returns>
        /// <response code="200">Return the list of the helps</response>returns></returns>
        [HttpGet]
        public async Task<ActionResult<IList<HelpModel>>> GetHelps([FromQuery] string type = null, [FromQuery] string title = null, [FromQuery] Nullable<DateTime> createdAt = null, [FromQuery] string personNumber = null, [FromQuery] Nullable<DateTime> timeNeeded = null, [FromQuery] string start = null, [FromQuery] string destination = null, [FromQuery] string budget = null, [FromQuery] List<Service> services = null, [FromQuery] int size = 0)
        {
            return Ok(await _helpService.GetAllHelpAnnounces(type, title, createdAt, personNumber, timeNeeded, start, destination,budget, services, size)); 
        }

        /// <summary>
        /// To get a help with an id
        /// </summary>
        /// <returns></returns>
        /// <response code="200">Return a specific help by an id</response>returns></returns>
        [HttpGet("{id:length(24)}")]
        public async Task<ActionResult<Help>> GetHelpById(string id)
        {
            if (String.IsNullOrEmpty(id)) return BadRequest("Id is null of empty");
            return Ok(await _helpService.GetHelpAnnounceById(id));
        }

        /// <summary>
        /// To create an announce
        /// </summary>
        /// <returns></returns>
        /// <response code="200">Return the id of the help announce created</response>returns></returns>
        [AllowAnonymous]
        [HttpPost]
        public async Task<ActionResult<string>> CreateHelp(string type, string title, string personNumber, DateTime timeNeeded, string start, string destination, string budget, List<Service> services)
        {
            if(string.IsNullOrEmpty(type) || 
                string.IsNullOrEmpty(title) || 
                string.IsNullOrEmpty(personNumber) || 
                string.IsNullOrEmpty(start) || 
                string.IsNullOrEmpty(destination) ||
                string.IsNullOrEmpty(budget))
            {
                return BadRequest("You must enter params");
            }

            return Ok(await _helpService.CreateHelpAnnounce(type, title, personNumber, timeNeeded, start, destination, budget, services));
        }

        /// <summary>
        /// To update a help announce
        /// </summary>
        /// <returns></returns>
        [HttpPut("{id:length(24)}")]
        public async Task<IActionResult> UpdateHelpAnnounce(string id, string type, string title, string personNumber, DateTime timeNeeded, string start, string destination, string budget, List<Service> service)
        {
            try
            {
                await _helpService.UpdateHelpAnnounce(id, type, title, personNumber, timeNeeded, start,destination, budget, service);
            }
            catch (Exception err)
            {
                return BadRequest(err);
            }

            return Ok();
        }

        /// <summary>
        /// To delete a help announce
        /// </summary>
        /// <returns></returns>
        [HttpDelete("{id:length(24)}")]
        public async Task<IActionResult> DeleteReview(string id)
        {
            try
            {
                await _helpService.DeleteHelpAnnounce(id);
            }
            catch (Exception err)
            {
                return BadRequest(err);
            }

            return Ok();
        }


    }
}
