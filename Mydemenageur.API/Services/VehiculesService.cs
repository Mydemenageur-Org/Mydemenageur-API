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
        private readonly IMongoCollection<Vehicule> _vehicules;

        public VehiculesService(IMongoSettings mongoSettings)
        {
            var mongoClient = new MongoClient(mongoSettings.ConnectionString);
            var database = mongoClient.GetDatabase(mongoSettings.DatabaseName);

            _vehicules = database.GetCollection<Vehicule>(mongoSettings.VehiculesCollectionName);
        }

        public async Task<Vehicule> GetVehiculeAsync(string id)
        {
            var vehicule = await _vehicules.FindAsync<Vehicule>(vehicule => vehicule.Id == id);

            return await vehicule.FirstOrDefaultAsync();
        }

        public async Task<string> AddVehiculeAsync(VehiculeRegisterModel toRegister)
        {
            string id = await RegisterToDatabase(toRegister);
            return id;
        }

        public async Task UpdateVehiculeAsync(string id, VehiculeUpdateModel toUpdate)
        {
            var vehicule = await GetVehiculeAsync(id);

            if (vehicule == null) throw new ArgumentException("The vehicule doesn't exist", nameof(id));

            var update = Builders<Vehicule>.Update
                .Set(dbVehicule => dbVehicule.VehiculeNumber, toUpdate.VehiculeNumber)
                .Set(dbVehicule => dbVehicule.TarpaulinVehicule, toUpdate.TarpaulinVehicule)
                .Set(dbVehicule => dbVehicule.PTAC_TarpaulinVehicule, toUpdate.PTAC_TarpaulinVehicule)
                .Set(dbVehicule => dbVehicule.HardWallVehicule, toUpdate.HardWallVehicule)
                .Set(dbVehicule => dbVehicule.PTAC_HardWallVehicule, toUpdate.PTAC_HardWallVehicule)
                .Set(dbVehicule => dbVehicule.HorseTransport, toUpdate.HorseTransport)
                .Set(dbVehicule => dbVehicule.VehiculeTransport, toUpdate.VehiculeTransport)
                .Set(dbVehicule => dbVehicule.TotalCapacity, toUpdate.TotalCapacity);

            await _vehicules.UpdateOneAsync(dbVehicule =>
                dbVehicule.Id == id,
                update
            );
        }

        public async Task DeleteVehicule(string id, string userId)
        {
            await _vehicules.DeleteOneAsync<Vehicule>(vehicule => vehicule.Id == id);
        }

        private async Task<string> RegisterToDatabase(VehiculeRegisterModel toRegister)
        {
            Vehicule dbVehicule = new()
            {
                VehiculeNumber = toRegister.VehiculeNumber,
                TarpaulinVehicule = toRegister.TarpaulinVehicule,
                PTAC_TarpaulinVehicule = toRegister.PTAC_TarpaulinVehicule,
                HardWallVehicule = toRegister.HardWallVehicule,
                PTAC_HardWallVehicule = toRegister.PTAC_HardWallVehicule,
                HorseTransport = toRegister.HorseTransport,
                VehiculeTransport = toRegister.VehiculeTransport,
                TotalCapacity = toRegister.TotalCapacity
            };

            await _vehicules.InsertOneAsync(dbVehicule);

            return dbVehicule.Id;
        }
    }
}
