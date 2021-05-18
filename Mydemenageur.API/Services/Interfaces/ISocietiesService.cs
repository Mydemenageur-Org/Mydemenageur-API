﻿using Mydemenageur.API.Entities;
using Mydemenageur.API.Models.Society;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Mydemenageur.API.Services.Interfaces
{
    public interface ISocietiesService
    {

        Task<Society> GetSocietyAsync(string id);
        Task<List<Mover>> GetAllMoverAsync(string id);
        Task RegisterSocietyAsync(SocietyRegisterModel societyRegisterModel);
        Task UpdateSocietyAsync(string id, SocietyUpdateModel societyUpdateModel);
        Task DeleteSocietyAsync(string id);

    }
}