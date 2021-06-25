using Mydemenageur.API.Entities;
using Mydemenageur.API.Models.Vehicule;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Mydemenageur.API.Services.Interfaces
{
    public interface IVehiclesService
    {
        Task<List<Vehicles>> GetVehiclesAsync();
        Task<Vehicles> GetVehiculeAsync(string id);
        Task<string> AddVehiculeAsync(VehiclesRegisterModel toRegister);
        Task UpdateVehiculeAsync(string currentUserId, string id, VehiclesUpdateModel toUpdate);
        Task DeleteVehicule(string id, string userId);

    }
}
