﻿using Mydemenageur.API.Entities;
using Mydemenageur.API.Models.Vehicule;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Mydemenageur.API.Services.Interfaces
{
    public interface IVehiculesService
    {
        Task<List<Vehicules>> GetVehiculesAsync();
        Task<Vehicules> GetVehiculeAsync(string id);
        Task<string> AddVehiculeAsync(VehiculesRegisterModel toRegister);
        Task UpdateVehiculeAsync(string currentUserId, string id, VehiculesUpdateModel toUpdate);
        Task DeleteVehicule(string id, string userId);

    }
}