using MongoDB.Driver;
using Mydemenageur.DAL.Entities;
using Mydemenageur.DAL.Models.Vehicule;
using Mydemenageur.BLL.Services.Interfaces;
using Mydemenageur.DAL.Settings.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Mydemenageur.BLL.Services
{
    public class VehiclesService : IVehiclesService
    {
        private readonly IMongoCollection<Vehicles> _Vehicles;
        private readonly IMongoCollection<Society> _societies;

        public VehiclesService(IMongoSettings mongoSettings)
        {
            var mongoClient = new MongoClient(mongoSettings.ConnectionString);
            var database = mongoClient.GetDatabase(mongoSettings.DatabaseName);

            _Vehicles = database.GetCollection<Vehicles>(mongoSettings.VehiclesCollectionName);
            _societies = database.GetCollection<Society>(mongoSettings.SocietiesCollectionName);
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
            if (!SocietyExist(toRegister.SocietyId)) throw new Exception("The society doesn't exist");

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
                .Set(dbVehicule => dbVehicule.HasTarpaulinVehicle, toUpdate.HasTarpaulinVehicule)
                .Set(dbVehicule => dbVehicule.PTAC_TarpaulinVehicle, toUpdate.PTAC_TarpaulinVehicule)
                .Set(dbVehicule => dbVehicule.HasHardWallVehicle, toUpdate.HasHardWallVehicule)
                .Set(dbVehicule => dbVehicule.PTAC_HardWallVehicle, toUpdate.PTAC_HardWallVehicule)
                .Set(dbVehicule => dbVehicule.CanTransportHorse, toUpdate.CanTransportHorse)
                .Set(dbVehicule => dbVehicule.CanTransportVehicle, toUpdate.CanTransportVehicule)
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
                SocietyId = toRegister.SocietyId,
                VehiclesNumber = toRegister.VehiclesNumber,
                HasTarpaulinVehicle = toRegister.HasTarpaulinVehicule,
                PTAC_TarpaulinVehicle = toRegister.PTAC_TarpaulinVehicule,
                HasHardWallVehicle = toRegister.HasHardWallVehicule,
                PTAC_HardWallVehicle = toRegister.PTAC_HardWallVehicule,
                CanTransportHorse = toRegister.CanTransportHorse,
                CanTransportVehicle = toRegister.CanTransportVehicule,
                TotalCapacity = toRegister.TotalCapacity
            };

            await _Vehicles.InsertOneAsync(dbVehicule);

            return dbVehicule.Id;
        }

        private bool SocietyExist(string societyId)
        {
            return _societies.AsQueryable<Society>().Any(dbVehicles =>
                dbVehicles.Id == societyId
            );
        }

    }
}
