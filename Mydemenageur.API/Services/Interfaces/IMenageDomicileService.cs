using Mydemenageur.API.Entities;
using Mydemenageur.API.Models.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Mydemenageur.API.Services.Interfaces
{
    public interface IMenageDomicileService
    {
        public Task<IList<MenageDomicileModel>> GetAllMenageDomicile(string surface, string frequency, Nullable<DateTime> dateEvent, string budget, string menageType, bool isPro, int size);
        public Task<MenageDomicile> GetMenageById(string id);
        public Task<string> CreateMenageDomicile(MenageDomicileModel men);
        public Task UpdateMenageDomicile(string id, MenageDomicileModel menageDomicile);
        public Task DeleteMenageDomicile(string id);
    }
}
