using Mydemenageur.API.Entities;
using Mydemenageur.API.Models.Users;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Mydemenageur.API.Services.Interfaces.UserRelated
{
    public interface IUserRelated
    {

        User GetUser(string id);
        void UpdateUser(string currentUserId, string userId, User toUpdate);

    }
}
