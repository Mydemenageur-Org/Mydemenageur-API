using MongoDB.Driver.Linq;
using Mydemenageur.API.Entities;
using System.Collections.Generic;
using System;
using System.Threading.Tasks;
using Mydemenageur.API.DP.Interface;
using Mydemenageur.API.Models.Services;
using Mydemenageur.API.Extensions;
using MongoDB.Driver;
using Mydemenageur.API.Services.Interfaces;
using System.Linq.Expressions;

namespace Mydemenageur.API.Services
{
    public class MenageDomicileService
    {
        private readonly IDPMenageDomicile _dpMenageDomicile;

        public MenageDomicileService(IDPMenageDomicile dPMenageDomicile)
        {
            _dpMenageDomicile = dPMenageDomicile;
        }

        public async Task<IList<Menage>>
    }
}
