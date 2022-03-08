using Microsoft.AspNetCore.Mvc;
using Mydemenageur.DAL.Models.Users;
using Mydemenageur.BLL.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Threading.Tasks;
using AutoMapper;

namespace Mydemenageur.API.Controllers
{
    [Route("api/[controller]")]
    public class GrosBrasController: ControllerBase
    {
        private readonly IGrosBrasService _grosBrasService;
        private readonly IMapper _mapper;

        public GrosBrasController(IGrosBrasService grosBrasService, IMapper mapper)
        {
            _grosBrasService = grosBrasService;
            _mapper = mapper;
        }

        /// <summary>
        /// Get all grosBras 
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public async Task<ActionResult<IList<GrosBrasPopulated>>> GetGrosBras([FromQuery] int pageNumber = -1, [FromQuery] int numberOfElementsPerPage = -1, [FromQuery] string cityLabel = "")
        {
            var queryString = HttpContext.Request.QueryString;
            IList<GrosBrasPopulated> grosBras = await _grosBrasService.GetGrosBras(queryString, pageNumber, numberOfElementsPerPage, cityLabel);

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
        /// Create a grosBras
        /// </summary>
        /// <param name="grosBrasSubmit"></param>
        /// <returns></returns>
        [HttpPost("create")]
        public async Task<ActionResult<string>> CreateGrosBras([FromBody] GrosBrasSubmit grosBrasSubmit)
        {
            try
            {
                GrosBras grosBras = _mapper.Map<GrosBras>(grosBrasSubmit);
                string grosBrasId = await _grosBrasService.CreateGrosBras(grosBras, grosBrasSubmit.CityName);
                
                return Ok(grosBrasId);
            }
            catch (Exception e)
            {
                return BadRequest($"Error during the request: {e.Message}");
            }
           
        }
        
        /// <summary>
        /// Update a grosBras
        /// </summary>
        /// <param name="grosBrasSubmit"></param>
        /// <returns></returns>
        [HttpPost("update")]
        public async Task<ActionResult<string>> UpdateGrosBras([FromBody] GrosBrasSubmit grosBrasSubmit)
        {
            try
            {
                GrosBras grosBras = _mapper.Map<GrosBras>(grosBrasSubmit);
                string grosBrasId = await _grosBrasService.UpdateGrosBras(grosBras, grosBrasSubmit.CityName);
                
                return Ok(grosBrasId);
            }
            catch (Exception e)
            {
                return BadRequest($"Error during the request: {e.Message}");
            }
        }
    }
}
