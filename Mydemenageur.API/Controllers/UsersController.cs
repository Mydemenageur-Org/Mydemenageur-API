﻿using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Mvc;
using Mydemenageur.DAL.Models.Users;
using Mydemenageur.BLL.Services.Interfaces;
using System;
using System.Collections.Generic;
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
        public async Task<ActionResult<IList<MyDemenageurUser>>> GetUsers()
        {
            IList<MyDemenageurUser> users = await _usersService.GetUsers();

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
        /// <param name="role"></param>
        /// <returns></returns>
        [HttpPut("{id:length(24)}/update-user-role")]
        public async Task<ActionResult<string>> UpdateUserRole(string id, [FromBody] string role)
        {
            string result = await _usersService.UpdateUserRole(id, role);

            return Ok(result);
        }
        
        /// <summary>
        /// Update myDemenageurUser and User role type
        /// </summary>
        /// <param name="id"></param>
        /// <param name="roleType"></param>
        /// <returns></returns>
        [HttpPut("{id:length(24)}/update-user-roletype")]
        public async Task<ActionResult<string>> UpdateUserRoleType(string id, [FromBody] string roleType)
        {
            string result = await _usersService.UpdateUserRoleType(id, roleType);

            return Ok(result);
        }

        /// <summary>
        /// Get a specific user by it's id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet("{id:length(24)}")]
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
        /// Get a user's profile picture
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet("{id:length(24)}/profile-picture")]
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
    }
}
