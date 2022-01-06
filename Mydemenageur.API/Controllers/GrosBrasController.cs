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
        public async Task<ActionResult<IList<GrosBras>>> GetGrosBras()
        {
            IList<GrosBras> grosBras = await _grosBrasService.GetGrosBras();

            return Ok(grosBras);
        }

    }
}
