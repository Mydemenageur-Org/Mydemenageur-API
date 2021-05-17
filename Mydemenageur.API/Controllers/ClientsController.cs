using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Mydemenageur.API.Entities;
using Mydemenageur.API.Models.Clients;
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
        /// Update a client. Only admin can edit every clients
        /// </summary>
        /// <param name="id" exemple="5f1fe90a58c8ab093c4f772a"></param>
        /// <param name="clientUpdateModel"></param>
        /// <returns></returns>
        /// <response code="400">Their is an error in the request</response>
        /// <response code="403">You are not allowed to edit this client</response>
        /// <response code="200">The client has been updated</response>
        [HttpPut("{id:length(24)}/update")]
        public async Task<IActionResult> UpdateClient(string id, [FromBody] ClientUpdateModel clientUpdateModel)
        {
            var currentClientId = User.Identity.Name;

            try
            {
                if (currentClientId != id)
                {
                    return Forbid("You can't edit that client : you are not the client you want to edit");
                }

                await _clientsService.UpdateClientAsync(id, clientUpdateModel);

                return Ok();
            }
            catch (Exception e)
            {
                return BadRequest($"Can't update the client: {e.Message}");
            }
        }
    }
}
