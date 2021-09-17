using Mydemenageur.API.Entities;
using Mydemenageur.API.Models.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Mydemenageur.API.Services.Interfaces
{
    public interface IMenageRepassageService
    {
        public Task<IList<MenageRepassageModel>> GetAllMenage(string clotheNumber, string frequency, Nullable<DateTime> dateEvent, string budget, string menageType, bool isPro, int size);
        public Task<MenageRepassage> GetMenageById(string id);
        public Task<string> CreateMenageDomicile(MenageRepassageModel men);
        public Task UpdateMenageDomicile(string id, MenageRepassageModel menage);
        public Task DeleteMenageDomicile(string id);
    }
}
