using Microsoft.AspNetCore.Mvc;
using Mydemenageur.BLL.Services.Interfaces;
using Mydemenageur.DAL.Models.Demands;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Mydemenageur.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DemandController : ControllerBase
    {
        private readonly IDemandService _demandService;

        public DemandController(IDemandService demandService)
        {
            _demandService = demandService;
        }

        /// <summary>
        /// Get a demand by ID
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet("{id:length(24)}")]
        public async Task<ActionResult<Demand>> GetDemand(string id)
        {
            Demand demand = await _demandService.GetDemand(id);

            if (demand == null)
            {
                return NotFound();
            }

            return Ok(demand);
        }

        /// <summary>
        /// Get all demands
        /// </summary>
        /// <returns></returns>
        [HttpGet()]
        public async Task<ActionResult<IList<Demand>>> GetDemands()
        {
            IList<Demand> demands = await _demandService.GetDemands();

            return Ok(demands);
        }

        /// <summary>
        /// Get all demands from the recipient
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet("{id:length(24)}/recipient")]
        public async Task<ActionResult<IList<DemandMessage>>> GetDemandsFromRecipient(string id)
        {
            IList<DemandMessage> demand = await _demandService.GetRecipientDemands(id);

            return Ok(demand);
        }


        /// <summary>
        /// Get all demands from the sender
        /// </summary>
        /// <param name="senderId"></param>
        /// <returns></returns>
        [HttpGet("{id:length(24)}/sender")]
        public async Task<ActionResult<IList<Demand>>> GetDemandsFromSender(string senderId)
        {
            IList<Demand> demand = await _demandService.GetSenderDemands(senderId);

            if (demand.Count() == 0)
            {
                return NotFound();
            }

            return Ok(demand);
        }

        /// <summary>
        /// GCreate a new demand
        /// </summary>
        /// <param name="demandCreated"></param>
        /// <returns></returns>
        [HttpPost()]
        public async Task<ActionResult<Demand>> CreateDemand(DemandCreation demandCreated)
        {
            try
            {
                Demand demand = await _demandService.CreateDemand(demandCreated);
                if(demand == null)
                {
                    return BadRequest("Not enough tokens");
                }
                return NoContent();
            }
            catch (Exception e)
            {
                return BadRequest($"Error during the request: {e.Message}");
            }
        }

        /// <summary>
        /// Update a demand
        /// </summary>
        /// <param name="id"></param>
        /// <param name="demandToUpdate"></param>
        /// <returns></returns>
        [HttpPut("{id:length(24)}")]
        public async Task<ActionResult<DemandMessage>> UpdateDemand(string id, [FromBody] DemandCreation demandToUpdate)
        {
            if (demandToUpdate.Id != id)
            {
                return BadRequest("The id of the generic demand doesn't match the resource id");
            }

            // TODO: improve security here
            try
            {
                var result = await _demandService.UpdateDemand(demandToUpdate);

                return Ok(result);
            }
            catch (Exception e)
            {
                return BadRequest($"Error during the request: {e.Message}");
            }

            ///// <summary>
            ///// delete a demand
            ///// </summary>
            ///// <param name="id"></param>
            ///// <returns></returns>
            //[HttpDelete()]
            //public async Task<ActionResult<string>> DeleteDemand(string id)
            //{

            //}
        }
    }
}
