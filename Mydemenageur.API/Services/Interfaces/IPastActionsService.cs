using Mydemenageur.API.Entities;
using Mydemenageur.API.Models.PastAction;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Mydemenageur.API.Services.Interfaces
{
    public interface IPastActionsService
    {

        Task<PastAction> GetPastActionAsync(string id);

        Task<User> GetUserAsync(string id);

        Task<string> RegisterPastActionAsync(PastActionRegisterModel toRegister);



    }
}
