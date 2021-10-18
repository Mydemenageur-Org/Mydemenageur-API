using Mydemenageur.DAL.Entities;
using Mydemenageur.DAL.Models.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Mydemenageur.BLL.Services.Interfaces
{
    public interface IMenageLavageAutoService
    {
        public Task<IList<MenageLavageAutoModel>> GetAllMenage(string vehiculeType, Nullable<DateTime> dateEvent, string budget, string menageType, bool isPro, int size);
        public Task<MenageLavageAuto> GetMenageById(string id);
        public Task<string> CreateMenageDomicile(MenageLavageAutoModel men);
        public Task UpdateMenageDomicile(string id, MenageLavageAutoModel menageDomicile);
        public Task DeleteMenageDomicile(string id);
    }
}
