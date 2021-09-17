using Mydemenageur.API.Entities;
using Mydemenageur.API.Models.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Mydemenageur.API.Services.Interfaces
{
    public interface IMenageNettoyageVitre
    {
        public Task<IList<MenageNettoyageVitreModel>> GetAllMenage(string winNumber, Nullable<DateTime> dateEvent, string budget, string menageType, bool isPro, int size);
        public Task<MenageNettoyageVitre> GetMenageById(string id);
        public Task<string> CreateMenageDomicile(MenageNettoyageVitreModel men);
        public Task UpdateMenageDomicile(string id, MenageNettoyageVitreModel menageDomicile);
        public Task DeleteMenageDomicile(string id);
    }
}
