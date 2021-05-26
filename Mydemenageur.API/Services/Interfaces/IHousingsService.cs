using Mydemenageur.API.Entities;
using Mydemenageur.API.Models.Housing;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Mydemenageur.API.Services.Interfaces
{
    public interface IHousingsService
    {

        Task<List<Housing>> GetHousingAsync();

        Task<Housing> GetHousingAsync(string id);

        Task<string> RegisterHousingAsync(HousingRegisterModel housingRegisterModel);

        Task UpdateHousingAsync(string currentUserId, string id, HousingUpdateModel housingUpdateModel);

        Task DeleteHousingAsync(string id, string userId);

    }
}
