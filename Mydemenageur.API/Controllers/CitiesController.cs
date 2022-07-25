using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Mvc;
using Mydemenageur.DAL.Models;
using Mydemenageur.BLL.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using AutoMapper;

namespace Mydemenageur.API.Controllers
{
    [Route("api/[controller]")]
    public class CitiesController: ControllerBase
    {
        private readonly ICitiesService _citiesService;

        public CitiesController(ICitiesService citiesService)
        {
            _citiesService = citiesService;
        }

        /// <summary>
        /// Get all cities
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public async Task<ActionResult<IList<City>>> GetAllCities()
        {
            IList<City> result = await _citiesService.GetAllCities();

            return Ok(result);
        }

        /// <summary>
        /// Create a city
        /// </summary>
        /// <param name="cityName"></param>
        /// <returns></returns>
        [HttpPost]
        public async Task<ActionResult<City>> CreateCity(string cityName)
        {
            City result = await _citiesService.CreateNewCity(cityName);

            return Ok(result);
        }
        
        /// <summary>
        /// Search a city
        /// </summary>
        /// <param name="cityName"></param>
        /// <returns></returns>
        [HttpPost("search")]
        public async Task<ActionResult<City>> SearchCity([FromBody] City cityToSearch)
        {
            City result = await _citiesService.SearchCity(cityToSearch);

            return Ok(result);
        }
    }
}
