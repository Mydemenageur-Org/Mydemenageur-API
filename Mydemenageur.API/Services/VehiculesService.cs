using MongoDB.Driver;
using Mydemenageur.API.Entities;
using Mydemenageur.API.Models.Vehicule;
using Mydemenageur.API.Services.Interfaces;
using Mydemenageur.API.Settings.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Mydemenageur.API.Services
{
    public class VehiclesService : IVehiclesService
    {
        private readonly IMongoCollection<Vehicles> _Vehicles;

        public VehiclesService(IMongoSettings mongoSettings)
        {
            var mongoClient = new MongoClient(mongoSettings.ConnectionString);
            var database = mongoClient.GetDatabase(mongoSettings.DatabaseName);

            _Vehicles = database.GetCollection<Vehicles>(mongoSettings.VehiclesCollectionName);
        }

        public async Task<List<Vehicles>> GetVehiclesAsync()
        {
            var Vehicles = await _Vehicles.FindAsync(Vehicles => true);
            return await Vehicles.ToListAsync();
        }

        public async Task<Vehicles> GetVehiculeAsync(string id)
        {
            var vehicule = await _Vehicles.FindAsync<Vehicles>(vehicule => vehicule.Id == id);

            return await vehicule.FirstOrDefaultAsync();
        }

        public async Task<string> AddVehiculeAsync(VehiclesRegisterModel toRegister)
        {
            string id = await RegisterToDatabase(toRegister);
            return id;
        }

        public async Task UpdateVehiculeAsync(string currentUserId, string id, VehiclesUpdateModel toUpdate)
        {
            var vehicule = await GetVehiculeAsync(id);

            if (vehicule == null) throw new Exception("The vehicule doesn't exist");

            if (vehicule.SocietyId == id) throw new UnauthorizedAccessException("Your are not the manager of this society");

            var update = Builders<Vehicles>.Update
                .Set(dbVehicule => dbVehicule.VehiclesNumber, toUpdate.VehiclesNumber)
                .Set(dbVehicule => dbVehicule.HasTarpaulinVehicule, toUpdate.HasTarpaulinVehicule)
                .Set(dbVehicule => dbVehicule.PTAC_TarpaulinVehicule, toUpdate.PTAC_TarpaulinVehicule)
                .Set(dbVehicule => dbVehicule.HasHardWallVehicule, toUpdate.HasHardWallVehicule)
                .Set(dbVehicule => dbVehicule.PTAC_HardWallVehicule, toUpdate.PTAC_HardWallVehicule)
                .Set(dbVehicule => dbVehicule.CanTransportHorse, toUpdate.CanTransportHorse)
                .Set(dbVehicule => dbVehicule.CanTransportVehicule, toUpdate.CanTransportVehicule)
                .Set(dbVehicule => dbVehicule.TotalCapacity, toUpdate.TotalCapacity);

            await _Vehicles.UpdateOneAsync(dbVehicule =>
                dbVehicule.Id == id,
                update
            );
        }

        public async Task DeleteVehicule(string id, string userId)
        {
            var vehicule = await GetVehiculeAsync(id);

            if (vehicule == null) throw new Exception("The vehicule doesn't exist");

            if (vehicule.SocietyId == id) throw new UnauthorizedAccessException("Your are not the manager of this society");

            await _Vehicles.DeleteOneAsync<Vehicles>(vehicule => vehicule.Id == id);
        }

        private async Task<string> RegisterToDatabase(VehiclesRegisterModel toRegister)
        {
            Vehicles dbVehicule = new()
            {
                VehiclesNumber = toRegister.VehiclesNumber,
                HasTarpaulinVehicule = toRegister.HasTarpaulinVehicule,
                PTAC_TarpaulinVehicule = toRegister.PTAC_TarpaulinVehicule,
                HasHardWallVehicule = toRegister.HasHardWallVehicule,
                PTAC_HardWallVehicule = toRegister.PTAC_HardWallVehicule,
                CanTransportHorse = toRegister.CanTransportHorse,
                CanTransportVehicule = toRegister.CanTransportVehicule,
                TotalCapacity = toRegister.TotalCapacity
            };

            await _Vehicles.InsertOneAsync(dbVehicule);

            return dbVehicule.Id;
        }

    }
}
