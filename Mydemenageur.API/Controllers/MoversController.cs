using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Mydemenageur.API.Entities;
using Mydemenageur.API.Models.Movers;
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
    public class MoversController : ControllerBase
    {
        private readonly IMoversService _moversService;
        public MoversController(IMoversService moversService)
        {
            _moversService = moversService;
        }

        /// <summary>
        /// To get a mover with his id
        /// </summary>
        /// <param name="id"></param>
        /// <response code="200">Return the logged mover with valid token</response>
        /// <returns></returns>
        [HttpGet("{id:length(24)}")]
        public async Task<ActionResult<Mover>> GetMover(string id)
        {
            var mover = await _moversService.GetMoverAsync(id);

            return Ok(mover);
        }
        /// <summary>
        /// To get user with mover id
        /// </summary>
        /// <param name="id"></param>
        /// <response code="200">Return the user with valid token</response>
        /// <returns></returns>
        [HttpGet("{id:length(24)}/user")]
        public async Task<ActionResult<User>> GetUser(string id)
        {
            var user = await _moversService.GetUserAsync(id);

            if (user == null)
            {
                return NotFound();
            }

            return Ok(user);
        }

        /// <summary>
        /// To register mover
        /// </summary>
        /// <param name="toCreate"></param>
        /// <response code="200">Return the id of the new Mover</response>
        /// <response code="400">Return an error if the body is bad</response>
        /// <returns></returns>
        [AllowAnonymous]
        [HttpPost]
        public async Task<ActionResult<string>> RegisterMover([FromBody] MoverRegisterModel toCreate)
        {
            try
            {
                string id = await _moversService.RegisterMoverAsync(toCreate);

                return Ok(id);
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        /// <summary>
        /// To update mover
        /// </summary>
        /// <param name="id"></param>
        /// <param name="moverUpdateModel"></param>
        /// <response code="200">Return ok if the update is good</response>
        /// <response code="400">Return an error if the body is bad</response>
        /// <returns></returns>
        [HttpPut("{id:length(24)}")]
        public async Task<IActionResult> UpdateMover(string id, [FromBody] MoverUpdateModel moverUpdateModel)
        {
            var currentMoverId = User.Identity.Name;

            try
            {
                if (currentMoverId != id)
                {
                    return Forbid("You can't edit that mover : you are not the mover you want to edit");
                }

                await _moversService.UpdateMoverAsync(id, moverUpdateModel);

                return Ok();
            }
            catch (Exception e)
            {
                return BadRequest($"Can't update the mover: {e.Message}");
            }
        }
    }
}
