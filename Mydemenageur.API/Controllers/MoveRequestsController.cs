using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Mydemenageur.API.Entities;
using Mydemenageur.API.Models.MoveRequest;
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
    public class MoveRequestsController : ControllerBase
    {

        private readonly IMoveRequestsService _moveRequestsService;

        public MoveRequestsController(IMoveRequestsService moveRequestsService)
        {
            _moveRequestsService = moveRequestsService;
        }
        
        /// <summary>
        /// To get all move request
        /// </summary>
        /// <returns></returns>
        /// <response code="200">All move request was get</response>
        [HttpGet]
        public async Task<ActionResult<MoveRequest>> GetMoveRequest()
        {
            var moveRequests = await _moveRequestsService.GetMoveRequestAsync();

            return Ok(moveRequests);
        }

        /// <summary>
        /// To get a specific move request
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        /// <response code="200">The move request was get</response>
        [HttpGet("{id:length(24)}")]
        public async Task<ActionResult<MoveRequest>> GetMoveRequest(string id)
        {
            var moveRequest = await _moveRequestsService.GetMoveRequestAsync(id);

            return Ok(moveRequest);
        }

        /// <summary>
        /// To register a new move request
        /// </summary>
        /// <param name="toCreate"></param>
        /// <returns></returns>
        /// <response code="200">The move request was register</response>
        /// <response code="400">Bad request</response>
        [AllowAnonymous]
        [HttpPost]
        public async Task<ActionResult<string>> RegisterMoveRequest([FromBody] MoveRequestRegisterModel toCreate)
        {
            try
            {
                string id = await _moveRequestsService.RegisterMoveRequestAsync(toCreate);

                return Ok(id);
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        /// <summary>
        /// To update a move request
        /// </summary>
        /// <param name="id"></param>
        /// <param name="toUpdate"></param>
        /// <returns></returns>
        /// <response code="200">The move request is updated</response>
        /// <response code="403">You are not authorize to update this move request</response>
        /// <response code="400">Bad request</response>
        [HttpPut("{id:length(24)}")]
        public async Task<IActionResult> UpdateMoveRequest(string id, [FromBody] MoveRequestUpdateModel toUpdate)
        {
            var currentUserId = User.Identity.Name;

            try
            {
                await _moveRequestsService.UpdateMoveRequestAsync(currentUserId, id, toUpdate);

                return Ok();
            }
            catch (UnauthorizedAccessException e)
            {
                return Forbid($"Can't update the move request: {e.Message}");
            }
            catch (Exception e)
            {
                return BadRequest($"Can't update the move request: {e.Message}");
            }
        }

    }
}
