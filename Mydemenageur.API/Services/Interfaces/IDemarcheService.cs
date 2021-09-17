using Mydemenageur.API.Entities;
using Mydemenageur.API.Models.Demarche;
using Mydemenageur.API.Models.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Mydemenageur.API.Services.Interfaces
{
    public interface IDemarcheService
    {
        public Task<IList<DemarcheModel>> GetAllDemarches(Nullable<DateTime> dateMove, bool hasAlreadyMoved, bool energyNotification, bool boxAndMobileNotification, bool assurance, bool alarmAndMonitors, bool bankAccount, bool grayCard, bool devisDemenagement, int size);
        public Task<Demarche> GetDemarcheById(string id);
        public Task<string> CreateDemarche(DemarcheModel men);
        public Task UpdateDemarche(string id, DemarcheModel demarche);
        public Task DeleteDemarche(string id);
    }
}
