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
    public class VehiculesService : IVehiculesService
    {
        private readonly IMongoCollection<Vehicules> _vehicules;
        private readonly IMongoCollection<Society> _societies;

        public VehiculesService(IMongoSettings mongoSettings)
        {
            var mongoClient = new MongoClient(mongoSettings.ConnectionString);
            var database = mongoClient.GetDatabase(mongoSettings.DatabaseName);

            _vehicules = database.GetCollection<Vehicules>(mongoSettings.VehiculesCollectionName);
        }

        public async Task<Vehicules> GetVehiculeAsync(string id)
        {
            var vehicule = await _vehicules.FindAsync<Vehicules>(vehicule => vehicule.Id == id);

            return await vehicule.FirstOrDefaultAsync();
        }

        public async Task<string> AddVehiculeAsync(VehiculesRegisterModel toRegister)
        {
            string id = await RegisterToDatabase(toRegister);
            return id;
        }

        public async Task UpdateVehiculeAsync(string currentUserId, string id, VehiculesUpdateModel toUpdate)
        {
            var vehicule = await GetVehiculeAsync(id);
            var society = await (await _societies.FindAsync<Society>(society => society.ManagerId == currentUserId)).FirstOrDefaultAsync();

            if (vehicule == null) throw new ArgumentException("The vehicule doesn't exist", nameof(id));

            if (society.VehiculeId == id) throw new UnauthorizedAccessException("Your are not the manager of this society");

            var update = Builders<Vehicules>.Update
                .Set(dbVehicule => dbVehicule.VehiculesNumber, toUpdate.VehiculesNumber)
                .Set(dbVehicule => dbVehicule.HasTarpaulinVehicule, toUpdate.HasTarpaulinVehicule)
                .Set(dbVehicule => dbVehicule.PTAC_TarpaulinVehicule, toUpdate.PTAC_TarpaulinVehicule)
                .Set(dbVehicule => dbVehicule.HasHardWallVehicule, toUpdate.HasHardWallVehicule)
                .Set(dbVehicule => dbVehicule.PTAC_HardWallVehicule, toUpdate.PTAC_HardWallVehicule)
                .Set(dbVehicule => dbVehicule.CanTransportHorse, toUpdate.CanTransportHorse)
                .Set(dbVehicule => dbVehicule.CanTransportVehicule, toUpdate.CanTransportVehicule)
                .Set(dbVehicule => dbVehicule.TotalCapacity, toUpdate.TotalCapacity);

            await _vehicules.UpdateOneAsync(dbVehicule =>
                dbVehicule.Id == id,
                update
            );
        }

        public async Task DeleteVehicule(string id, string userId)
        {
            await _vehicules.DeleteOneAsync<Vehicules>(vehicule => vehicule.Id == id);
        }

        private async Task<string> RegisterToDatabase(VehiculesRegisterModel toRegister)
        {
            Vehicules dbVehicule = new()
            {
                VehiculesNumber = toRegister.VehiculesNumber,
                HasTarpaulinVehicule = toRegister.HasTarpaulinVehicule,
                PTAC_TarpaulinVehicule = toRegister.PTAC_TarpaulinVehicule,
                HasHardWallVehicule = toRegister.HasHardWallVehicule,
                PTAC_HardWallVehicule = toRegister.PTAC_HardWallVehicule,
                CanTransportHorse = toRegister.CanTransportHorse,
                CanTransportVehicule = toRegister.CanTransportVehicule,
                TotalCapacity = toRegister.TotalCapacity
            };

            await _vehicules.InsertOneAsync(dbVehicule);

            return dbVehicule.Id;
        }
    }
}
