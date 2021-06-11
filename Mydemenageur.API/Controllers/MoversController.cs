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
        /// <response code="400">Return an error if the Mover doesn't exist</response>
        /// <response code="403">Return an error if you doesn't have authorization</response>
        /// <returns></returns>
        [HttpPut("{id:length(24)}")]
        public async Task<IActionResult> UpdateMover(string id, [FromBody] MoverUpdateModel moverUpdateModel)
        {
            var currentUserId = User.Identity.Name;

            try
            {
                await _moversService.UpdateMoverAsync(currentUserId, id, moverUpdateModel);

                return Ok();
            }
            catch (UnauthorizedAccessException e)
            {
                return Forbid($"Can't update the mover: {e.Message}");
            }
            catch (Exception e)
            {
                return BadRequest($"Can't update the mover: {e.Message}");
            }
            
        }

        /// <summary>
        /// Delete a mover. Only admin can delete mover
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpDelete("{id:length(24)}")]
        public async Task<IActionResult> DeleteMover(string id)
        {
            try
            {
                if (User.IsInRole(Roles.Admin))
                {
                    await _moversService.DeleteMoverFromAdminAsync(id);
                    return Ok();
                }

                return Unauthorized();

            }
            catch (Exception e)
            {
                return BadRequest($"Can't delete the mover: {e.Message}");
            }
        }
    }
}
