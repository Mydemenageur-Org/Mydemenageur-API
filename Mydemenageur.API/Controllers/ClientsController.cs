using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Mydemenageur.DAL.Entities;
using Mydemenageur.DAL.Models.Clients;
using Mydemenageur.DAL.Models.Users;
using Mydemenageur.BLL.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Mydemenageur.API.Controllers
{
    [Route("api/[controller]")]
    [Authorize]
    [ApiController]
    public class ClientsController : ControllerBase
    {
        private readonly IClientsService _clientsService;

        public ClientsController(IClientsService clientsService)
        {
            _clientsService = clientsService;
        }

        /// <summary>
        /// Get a client
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        /// <response code="200">The client was get</response>
        [HttpGet("{id:length(24)}")]
        public async Task<ActionResult<Client>> GetClient(string id)
        {
            var client = await _clientsService.GetClientAsync(id);

            return Ok(client);
        }

        /// <summary>
        /// Get the user corresponding to a client
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        /// <response code="404">Their is no user corresponding to the client</response>
        /// <response code="200">Return the user corresponding to a client</response>
        [HttpGet("{id:length(24)}/user")]
        public async Task<ActionResult<User>> GetUser(string id)
        {
            var user = await _clientsService.GetUserAsync(id);

            if (user == null)
            {
                return NotFound();
            }

            return Ok(user);
        }

        /// <summary>
        /// Regsiter a client
        /// </summary>
        /// <param name="clientRegisterModel"></param>
        /// <returns></returns>
        /// <response code="200">Return the newly created client id</response>
        [AllowAnonymous]
        [HttpPost]
        public async Task<ActionResult<string>> RegisterClient([FromBody] ClientRegisterModel clientRegisterModel)
        {
            string id;

            try
            {
                id = await _clientsService.RegisterClientAsync(clientRegisterModel);

                return Ok(id);
            } catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        /// <summary>
        /// Update a client. Only admin can edit every clients
        /// </summary>
        /// <param name="id" exemple="5f1fe90a58c8ab093c4f772a"></param>
        /// <param name="clientUpdateModel"></param>
        /// <returns></returns>
        /// <response code="400">Their is an error in the request</response>
        /// <response code="403">You are not allowed to edit this client</response>
        /// <response code="200">The client has been updated</response>
        [HttpPut("{id:length(24)}")]
        public async Task<IActionResult> UpdateClient(string id, [FromBody] ClientUpdateModel clientUpdateModel)
        {
            var currentUserId = User.Identity.Name;

            try
            {
                if (User.IsInRole(Roles.Admin))
                {
                    await _clientsService.UpdateClientFromAdminAsync(id, clientUpdateModel);
                }
                else
                {
                    await _clientsService.UpdateClientAsync(currentUserId, id, clientUpdateModel);
                }

                return Ok();
            }
            catch (UnauthorizedAccessException e)
            {
                return Unauthorized($"Can't update the client: {e.Message}");
            }
            catch (Exception e)
            {
                return BadRequest($"Can't update the client: {e.Message}");
            }
        }

        /// <summary>
        /// Delete a client. Only admin can delete client
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpDelete("{id:length(24)}")]
        public async Task<IActionResult> DeleteClient(string id)
        {
            try
            {
                if (User.IsInRole(Roles.Admin))
                {
                    await _clientsService.DeleteClientFromAdminAsync(id);

                    return Ok();
                }
                else
                {
                    return Unauthorized();
                }

            }
            catch (Exception e)
            {
                return BadRequest($"Can't delete the client: {e.Message}");
            }
        }
    }
}
