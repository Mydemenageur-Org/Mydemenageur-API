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
    public class DemandController: ControllerBase
    {
        private readonly IDemandService _demandService;

        public DemandController(IDemandService demandService)
        {
            _demandService = demandService;
        }


    }
}
