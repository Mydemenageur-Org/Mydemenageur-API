using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Mydemenageur.API.Entities;
using Mydemenageur.API.Models.Users;
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
    public class UsersController : ControllerBase
    {
        private readonly IUsersService _usersService;

        public UsersController(IUsersService usersService)
        {
            _usersService = usersService;
        }

        /// <summary>
        /// Get all user. Only admin can use this
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        /// <response code="200">The user's were get</response>
        [HttpGet]
        public async Task<ActionResult<List<User>>> GetAllUser()
        {
            if (User.IsInRole(Roles.Admin))
            {
                var pastActions = await _usersService.GetUsersAsync();

                return Ok(pastActions);
            }
            else
            {
                return Forbid();
            }
        }

        /// <summary>
        /// Get a user
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        /// <response code="200">The user was get</response>
        [HttpGet("{id:length(24)}")]
        public async Task<ActionResult<User>> GetUser(string id)
        {
            return await _usersService.GetUserAsync(id);
        }

        /// <summary>
        /// To get all action of one user
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        /// <response code="200">Return all actions of a user</response>
        /// <response code="403">Actual user don't have access to this request</response>
        [HttpGet("{id:length(24)}/pastActions")]
        public async Task<ActionResult<List<PastAction>>> GetPastActionFromUser(string id)
        {

            if (User.IsInRole(Roles.Admin))
            {
                var pastActions = await _usersService.GetPastActionListFromUserAsync(id);

                return Ok(pastActions);
            }
            else
            {
                return Forbid();
            }
        }

        /// <summary>
        /// Update a user. Only admin can edit every users
        /// </summary>
        /// <param name="id" exemple="5f1fe90a58c8ab093c4f772a"></param>
        /// <param name="userUpdateModel"></param>
        /// <returns></returns>
        /// <response code="400">Their is an error in the request</response>
        /// <response code="403">You are not allowed to edit this user</response>
        /// <response code="200">The user has been updated</response>
        [HttpPut("{id:length(24)}")]
        public async Task<IActionResult> UpdateUser(string id, [FromBody]UserUpdateModel userUpdateModel)
        {
            var currentUserId = User.Identity.Name;

            try
            {
                if (currentUserId != id)
                {
                    return Forbid("You can't edit that user : you are not the user you want to edit");
                }

                await _usersService.UpdateUserAsync(id, userUpdateModel);

                return Ok();
            }
            catch (Exception e)
            {
                return BadRequest($"Can't update the user: {e.Message}");
            }
        }

    }
}
