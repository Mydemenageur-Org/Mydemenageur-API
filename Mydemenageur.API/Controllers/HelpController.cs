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
        public HelpController()
        {

        }

        /// <summary>
        /// To get all helps card
        /// </summary>
        /// <returns></returns>
        /// <response code="200">Return the list of the helps</response>returns></returns>
        [HttpGet]
        public async Task<ActionResult<IList<HelpModel>>> GetHelps(string type, string title, DateTime createdAt, string personNumber, DateTime timeNeeded, string start, string destination, string budget, string[] services)
        {

        }

        /// <summary>
        /// To get a help with an id
        /// </summary>
        /// <returns></returns>
        /// <response code="200">Return a specific help by an id</response>returns></returns>
        [HttpGet("{id:length(24)}")]
        public async Task<ActionResult<Help>> GetHelpById(string id)
        {

        }

        /// <summary>
        /// To create an announce
        /// </summary>
        /// <returns></returns>
        /// <response code="200">Return the id of the help announce created</response>returns></returns>
        [AllowAnonymous]
        [HttpPost]
        public async Task<ActionResult<string>> CreateHelp()
        {

        }

        /// <summary>
        /// To update a help announce
        /// </summary>
        /// <returns></returns>
        [HttpPut("{id:length(24)}")]
        public async Task<IActionResult> UpdateHelpAnnounce()
        {

        }

        /// <summary>
        /// To delete a help announce
        /// </summary>
        /// <returns></returns>
        [HttpDelete("{id:length(24)}")]
        public async Task<IActionResult> DeleteReview(string id)
        {

        }


    }
}
