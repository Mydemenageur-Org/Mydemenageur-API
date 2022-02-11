using Microsoft.AspNetCore.Mvc;
using Mydemenageur.DAL.Models.Users;
using Mydemenageur.BLL.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;

namespace Mydemenageur.API.Controllers
{
    [Route("api/[controller]")]
    public class GrosBrasController: ControllerBase
    {
        private readonly IGrosBrasService _grosBrasService;

        public GrosBrasController(IGrosBrasService grosBrasService)
        {
            _grosBrasService = grosBrasService;
        }

        /// <summary>
        /// Get all grosBras 
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public async Task<ActionResult<IList<GrosBrasPopulated>>> GetGrosBras([FromQuery] int pageNumber = -1, [FromQuery] int numberOfElementsPerPage = -1, [FromQuery] string cityLabel = "")
        {
            var queryParams = HttpContext.Request.Query;
            IList<GrosBrasPopulated> grosBras = await _grosBrasService.GetGrosBras(queryParams, pageNumber, numberOfElementsPerPage, cityLabel);

            return Ok(grosBras);
        }

        /// <summary>
        /// Get a specific gros bras profil by it's id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet("{id:length(24)}")]
        public async Task<ActionResult<GrosBrasPopulated>> GetGrosBrasById(string id)
        {
            GrosBrasPopulated profil = await _grosBrasService.GetGrosBrasById(id);

            return Ok(profil);
        }

        /// <summary>
        /// Get all grosBras 
        /// </summary>
        /// <returns></returns>
        [HttpGet("count")]
        public async Task<ActionResult<int>> GetGrosBrasCount()
        {
            var count = _grosBrasService.CountGrosBras();
            return Ok(count);
        }

        /// <summary>
        /// Get the top X gros bras
        /// </summary>
        /// <param name="numberOfGrosBras"></param>
        /// <returns></returns>
        [HttpGet("ranking")]
        public async Task<ActionResult<IList<GrosBrasPopulated>>> GetRanking([FromQuery] int numberOfGrosBras)
        {
            IList<GrosBrasPopulated> grosBras = await _grosBrasService.GetRanking(numberOfGrosBras);

            return Ok(grosBras);
        }

        /// <summary>
        /// Create a gros bras
        /// </summary>
        /// <param name="toCreate"></param>
        /// <returns></returns>
        [Authorize]
        [HttpPost]
        public async Task<ActionResult<string>> CreateGrosBras([FromBody] GrosBras toCreate)
        {
            string grosBrasId = await _grosBrasService.CreateGrosBras(toCreate);

            return Ok(grosBrasId);
        }

        /// <summary>
        /// Update a gros bras
        /// </summary>
        /// <param name="id"></param>
        /// <param name="toUpdate"></param>
        /// <returns></returns>
        [Authorize]
        [HttpPut("{id:length(24)}")]
        public async Task<IActionResult> UpdateGrosBras(string id, [FromBody] GrosBras toUpdate)
        {
            if (id != toUpdate.Id)
            {
                return BadRequest("The id and the gros bras id doesn't match");
            }

            await _grosBrasService.UpdateGrosBras(id, toUpdate);

            return Ok();
        }

    }
}
