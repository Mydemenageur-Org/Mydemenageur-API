using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Mydemenageur.DAL.Entities;
using Mydemenageur.DAL.Models.Services;
using Mydemenageur.BLL.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace Mydemenageur.API.Controllers
{
    [Route("api/[controller]")]
    [Authorize]
    [ApiController]
    public class CartonController: ControllerBase
    {
        private readonly ICartonService _cartonService;

        public CartonController(ICartonService cartonService)
        {
            _cartonService = cartonService;
        }

        /// <summary>
        /// To get all carton card
        /// </summary>
        /// <returns></returns>
        /// <response code="200">Return the list of the cartons</response>returns></returns>
        [HttpGet]
        public async Task<ActionResult<IList<CartonModel>>> GetCartons([FromQuery] bool isFlexible, [FromQuery]  string typeService, [FromQuery] string boxNb = null, [FromQuery] string boxSize = null, [FromQuery] string zipCode = null, [FromQuery] string city = null, [FromQuery] Nullable<DateTime> dateDisponibility = null, [FromQuery] Nullable<DateTime> startDisponibility = null, [FromQuery] Nullable<DateTime> endDisponibility = null, [FromQuery] int size = 0)
        {
            return Ok(await _cartonService.GetAllCartons(isFlexible, typeService, boxNb, boxSize, zipCode, city, dateDisponibility, startDisponibility, endDisponibility, size));
        }

        /// <summary>
        /// To create an announce
        /// </summary>
        /// <returns></returns>
        /// <response code="200">Return the id of the carton announce created</response>returns></returns>
        [AllowAnonymous]
        [HttpPost]
        public async Task<ActionResult<string>> CreateCarton(CartonModel cartonModel)
        {
            if (string.IsNullOrEmpty(cartonModel.AnnounceTitle) ||
                string.IsNullOrEmpty(cartonModel.City) ||
                string.IsNullOrEmpty(cartonModel.ZipCode) ||
                string.IsNullOrEmpty(cartonModel.BoxNumber) ||
                string.IsNullOrEmpty(cartonModel.BoxSize))
            {
                return BadRequest("You must enter params");
            }

            return Ok(await _cartonService.CreateCarton(cartonModel));
        }


        /// <summary>
        /// To update a carton announce
        /// </summary>
        /// <returns></returns>
        [HttpPut("{id:length(24)}")]
        public async Task<IActionResult> UpdateCartonAnnounce(string id, CartonModel carton)
        {
            try
            {
                await _cartonService.UpdateCarton(id, carton);
            }
            catch (Exception err)
            {
                return BadRequest(err);
            }

            return Ok();
        }

        /// <summary>
        /// To delete a carton announce
        /// </summary>
        /// <returns></returns>
        [HttpDelete("{id:length(24)}")]
        public async Task<IActionResult> DeleteCartonAnnounce(string id)
        {
            try
            {
                await _cartonService.DeleteService(id);
            }
            catch (Exception err)
            {
                return BadRequest(err);
            }

            return Ok();
        }
    }
}
