﻿using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;
using Mydemenageur.DAL.Models.Users;
using Mydemenageur.BLL.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;

namespace Mydemenageur.API.Controllers
{
    [Route("api/[controller]")]
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    [ApiController]
    public class UsersController : ControllerBase
    {
        private readonly IUsersService _usersService;
        private readonly IMapper _mapper;

        public UsersController(IUsersService usersService, IMapper mapper)
        {
            _usersService = usersService;

            _mapper = mapper;
        }

        /// <summary>
        /// Get all users 
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [AllowAnonymous]
        public async Task<ActionResult<IList<MyDemenageurUser>>> GetUsers()
        {
            IList<MyDemenageurUser> users = await _usersService.GetUsers();

            return Ok(users);
        }

        [HttpGet("filter")]
        public async Task<ActionResult<IList<MyDemenageurUser>>> GetUsersFiltered([FromQuery] int pageNumber = -1, [FromQuery] int numberOfElementsPerPage = -1)
        {
            var queryString = HttpContext.Request.QueryString;
            IList<MyDemenageurUser> users = await _usersService.GetUsersFiltered(queryString, pageNumber, numberOfElementsPerPage);

            return Ok(users);
        }

        /// <summary>
        /// Get a Provider from user id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet("{id:length(24)}/gros-bras")]
        public async Task<ActionResult<GrosBrasPopulated>> GetGrosBrasFromUserId(string id)
        {
            GrosBrasPopulated grosBras = await _usersService.GetGrosBrasFromUserId(id);

            return Ok(grosBras);
        }

        /// <summary>
        /// Update myDemenageurUser and User role
        /// </summary>
        /// <param name="id"></param>
        /// <param name="data"></param>
        /// <returns></returns>
        [HttpPut("{id:length(24)}/update-role")]
        public async Task<ActionResult<string>> UpdateUserRole(string id, [FromBody] MyDemenageurUserRole data)
        {
            string result = await _usersService.UpdateUserRole(id, data);

            return Ok(result);
        }

        /// <summary>
        /// Get a specific user by it's id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet("{id:length(24)}")]
        [AllowAnonymous]
        public async Task<ActionResult<MyDemenageurUser>> GetUser(string id)
        {
            MyDemenageurUser user = await _usersService.GetUser(id);

            if (user == null)
            {
                return NotFound();
            }

            return Ok(user);
        }
        
        /// <summary>
        /// Get a specific user by his email
        /// </summary>
        /// <param name="email"></param>
        /// <returns></returns>
        [HttpGet("email/{email}")]
        [AllowAnonymous]
        public async Task<ActionResult<MyDemenageurUser>> GetUserByEmail(string email)
        {
            MyDemenageurUser user = await _usersService.GetUserByEmail(email);

            if (user == null)
                return NotFound();

            return Ok(user);
        }

        /// <summary>
        /// Get a user's profile picture
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet("{id:length(24)}/profile-picture")]
        [AllowAnonymous]
        public async Task<ActionResult<byte[]>> GetProfilePicture(string id)
        {
            byte[] profilePicture = await _usersService.GetProfilePicture(id);

            if (profilePicture == null) return NotFound();

            return Ok(profilePicture);
        }
        
        /// <summary>
        /// Update a user
        /// </summary>
        /// <param name="id"></param>
        /// <param name="toUpdate"></param>
        /// <returns></returns>
        [HttpPut("{id:length(24)}")]
        public async Task<IActionResult> UpdateUser(string id, [FromBody] UserUpdateModel toUpdate)
        {
            if (toUpdate.Id != id)
            {
                return BadRequest("The id of the user doesn't match the resource id");
            }

            // TODO: improve security
            try
            {
                MyDemenageurUser user = _mapper.Map<MyDemenageurUser>(toUpdate);

                await _usersService.UpdateUser(toUpdate.ProfilePicture, toUpdate.Password, user);

                return Ok();
            }
            catch (Exception e)
            {
                return BadRequest($"Error during the request: {e.Message}");
            }
        }

        /// <summary>
        /// Get a user with its stripe id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet("/stripe/{id:length(24)}")]
        public async Task<ActionResult<MyDemenageurUser>> GetByStripeId(string id)
        {
            try
            {
                MyDemenageurUser user = await _usersService.GetByStripeId(id);

                return Ok(user);
            }
            catch (Exception e)
            {
                return BadRequest($"Error during the request: {e.Message}");
            }
            
        }

        /// <summary>
        /// Get user's total tokens
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet("{id:length(24)}/tokens")]
        public async Task<ActionResult<int>> GetTokens(string id)
        {
            var result = await _usersService.GetTotalTokens(id);
            
            return Ok(result);
        }

        /// <summary>
        /// Update tokens from a user
        /// </summary>
        /// <param name="id"></param>
        /// <param name="tokens"></param>
        /// <returns></returns>
        [HttpPut("{id:length(24)}/tokens")]
        public async Task<ActionResult<int>> UpdateTokens(string id, MyDemenageurUserTokens tokens)
        {
            var result = await _usersService.UpdateTokens(id, tokens);
            
            return Ok(result);
        }

        [HttpPut("{id:length(24)}/notifications")]
        public async Task<IActionResult> UpdateNotif(string id, MyDemenageurUserNotif notif)
        {
            try
            {
                await _usersService.UpdateNotif(id, notif);

                return Ok();
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }
        
        /// <summary>
        /// Delete a user
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpPost("delete")]
        public async Task<IActionResult> DeleteUser()
        {
            try
            {
                await _usersService.DeleteUser(User.Claims.First(u => u.Type == "id").Value);

                return Ok();
            }
            catch (Exception e)
            {
                return BadRequest($"Error during the request: {e.Message}");
            }
        }
    }
}
