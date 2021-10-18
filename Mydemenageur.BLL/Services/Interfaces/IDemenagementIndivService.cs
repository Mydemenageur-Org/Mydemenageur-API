using Mydemenageur.DAL.Entities;
using Mydemenageur.DAL.Models.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Mydemenageur.BLL.Services.Interfaces
{
    public interface IDemenagementIndivService
    {
        public Task<IList<DemenagementIndivModel>> GetAllDemenagement(Nullable<DateTime> date, bool askHelpStart, bool askHelpDest, bool isFlexibleDate, string fromCity, string toCity, string personNeeded, string volume, string serviceType, string demenagementType, int size);
        public Task<DemenagementIndividuel> GetDemenagementIndivById(string id);
        public Task<string> CreateDemenagementIndividuel(DemenagementIndivModel demIndiv);
        public Task UpdateDemenagementIndividuel(string id, DemenagementIndivModel demIndiv);
        public Task DeleteDemenagementIndividuel(string id);
    }
}
