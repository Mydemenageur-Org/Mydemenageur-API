using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Mydemenageur.API.Entities;
using Mydemenageur.API.Models.PastAction;
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
    public class PastActionController : ControllerBase
    {

        private readonly IPastActionsService _pastActionsService;

        public PastActionController(IPastActionsService pastActionsService)
        {
            _pastActionsService = pastActionsService;
        }

        /// <summary>
        /// To get a past action 
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        /// <response code="200"W>Return the past action with this id</response>
        [HttpGet("{id:length(24)}")]
        public async Task<ActionResult<PastAction>> GetPastAction(string id)
        {
            var pastAction = await _pastActionsService.GetPastActionAsync(id);

            return Ok(pastAction);
        }

        /// <summary>
        /// To get the list of past action of one user
        /// </summary>
        /// <param name="userId"></param>
        /// <returns></returns>
        /// <reponse code="200">Return list of past action of this user id</reponse>
        [HttpGet("{userId:length(24)}")]
        public async Task<ActionResult<List<PastAction>>> GetPastActionListFromUser(string userId)
        {
            var pastActionList = await _pastActionsService.GetPastActionListFromUserAsync(userId);

            return Ok(pastActionList);
        }

        /// <summary>
        /// To register a new action of user
        /// </summary>
        /// <param name="toRegister"></param>
        /// <returns></returns>
        /// <response code="200">Return the id of the new action</response>
        /// <response code ="400">They are an error the the request</response>
        [AllowAnonymous]
        [HttpPost]
        public async Task<ActionResult<string>> RegisterNewAction([FromBody] PastActionRegisterModel toRegister)
        {
            string id;

            try
            {
                id = await _pastActionsService.RegisterPastActionAsync(toRegister);

                return Ok(id);
            }catch(Exception e)
            {
                return BadRequest(e.Message);
            }
        }

    }
}
