using Microsoft.AspNetCore.Authorization;
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
        public async Task<ActionResult<IList<GrosBrasPopulated>>> GetGrosBras([FromQuery] int pageNumber = -1, [FromQuery] int numberOfElementsPerPage = -1)
        {
            IList<GrosBrasPopulated> grosBras = await _grosBrasService.GetGrosBras(pageNumber, numberOfElementsPerPage);

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



    }
}
