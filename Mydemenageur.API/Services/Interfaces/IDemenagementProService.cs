using Mydemenageur.API.Entities;
using Mydemenageur.API.Models.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Mydemenageur.API.Services.Interfaces
{
    public interface IDemenagementProService
    {
        public Task<IList<DemenagementProModel>> GetAllDemenagement(Nullable<DateTime> date, bool isStartingDateKnown, bool isHouse, bool hasMultipleFloors, string startZipCode, string startAddr, string fromCity, string endZipCode, string endAddr, string toCity, string volume, string surface, string serviceType, string demenagementType, int size);
        public Task<DemenagementPro> GetDemenagementProById(string id);
        public Task UpdateDemenagementPro(string id, DemenagementProModel demPro);
        public Task<string> CreateDemenagementPro(DemenagementProModel demsPro);
        public Task DeleteDemenagementPro(string id);
    }
}
