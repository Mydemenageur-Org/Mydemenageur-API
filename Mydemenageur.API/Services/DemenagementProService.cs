using MongoDB.Driver.Linq;
using Mydemenageur.API.Entities;
using System.Collections.Generic;
using System;
using System.Threading.Tasks;
using Mydemenageur.API.DP.Interface;
using Mydemenageur.API.Models.Services;
using Mydemenageur.API.Extensions;
using MongoDB.Driver;
using Mydemenageur.API.Services.Interfaces;
using System.Linq.Expressions;

namespace Mydemenageur.API.Services
{
    public class DemenagementProService: IDemenagementProService
    {
        private IDPDemenagementPro _dpDemenagementPro;

        public DemenagementProService(IDPDemenagementPro dPDemenagementPro)
        {
            _dpDemenagementPro = dPDemenagementPro;
        }

        public async Task<IList<DemenagementProModel>> GetAllDemenagement(Nullable<DateTime> date, bool isStartingDateKnown, bool isHouse, bool hasMultipleFloors, string startZipCode, string startAddr, string fromCity, string endZipCode, string endAddr, string toCity, string volume, string surface, string serviceType, string demenagementType, int size)
        {
            int iterator = 0;
            List<DemenagementPro> demPro = new List<DemenagementPro>();
            List<DemenagementProModel> demProModel = new List<DemenagementProModel>();

            List<string> paramStr = new List<string>() { startZipCode, startAddr, fromCity, endZipCode, endAddr, toCity, volume, surface, serviceType, demenagementType };

            List<Expression<Func<DemenagementPro, bool>>> queries = new List<Expression<Func<DemenagementPro, bool>>>();

            queries.Add(w => w.StartZipCode == paramStr[0]);
            queries.Add(w => w.StartAddress == paramStr[1]);
            queries.Add(w => w.Demenagement.FromCity == paramStr[2]);
            queries.Add(w => w.DestZipCode == paramStr[3]);
            queries.Add(w => w.DestAddress == paramStr[4]);
            queries.Add(w => w.Demenagement.ToCity == paramStr[5]);
            queries.Add(w => w.Volume == paramStr[6]);
            queries.Add(w => w.Surface == paramStr[7]);
            queries.Add(w => w.Demenagement.ServiceType == paramStr[8]);
            queries.Add(w => w.Demenagement.MoveType == paramStr[9]);

            IMongoQueryable<DemenagementPro> _demPro = _dpDemenagementPro.Obtain();

            foreach (string str in paramStr)
            {
                string tmp = str;
                if(tmp != null)
                {
                    _demPro = _demPro.Where(queries[iterator]);
                }
                ++iterator;
            }

            _demPro = _demPro.Where(w => w.IsStartingDateKnown == isStartingDateKnown);
            _demPro = _demPro.Where(w => w.IsHouse == isHouse);
            _demPro = _demPro.Where(w => w.HasMultipleFloors == hasMultipleFloors);

            if (date != null)
            {
                _demPro = _demPro.Where(w => w.Demenagement.MoveDate == date);
            }

            if(size != 0)
            {
                _demPro.Take(size);
            }

            demPro = await _demPro.ToListAsync();
            foreach(DemenagementPro dems in demPro)
            {
                DemenagementProModel buff = dems.ToModelDemPro();
                demProModel.Add(buff);
            }

            return demProModel;
        }

        public async Task<DemenagementPro> GetDemenagementProById(string id)
        {
            DemenagementPro demPro = await _dpDemenagementPro.GetDemenagementById(id).FirstOrDefaultAsync();
            if (demPro == null) throw new Exception("The service you are requesting does not exist");
            return demPro;
        }

        public async Task UpdateDemenagementPro(string id, DemenagementProModel demPro)
        {
            DemenagementPro _demPro = await _dpDemenagementPro.GetDemenagementById(id).FirstOrDefaultAsync();
            if (_demPro == null) throw new Exception("The service you are requesting does not exist");
            if (_demPro.Demenagement.UserId != demPro.Demenagement.UserId) throw new Exception("You are not allowed to make this request");

            var update = Builders<DemenagementPro>.Update
                .Set(db => db.Demenagement, demPro.Demenagement)
                .Set(db => db.IsHouse, demPro.IsHouse)
                .Set(db => db.HasMultipleFloors, demPro.HasMultipleFloors)
                .Set(db => db.StartAddress, demPro.StartAddress)
                .Set(db => db.StartZipCode, demPro.StartZipCode)
                .Set(db => db.DestZipCode, demPro.DestZipCode)
                .Set(db => db.DestAddress, demPro.DestAddress)
                .Set(db => db.IsStartingDateKnown, demPro.IsStartingDateKnown)
                .Set(db => db.Volume, demPro.Volume)
                .Set(db => db.Surface, demPro.Surface);
            await _dpDemenagementPro.GetCollection().UpdateOneAsync(db =>
                db.Id == id,
                update
            );
        }

        public async Task<string> CreateDemenagementPro(DemenagementProModel demsPro)
        {
            DemenagementPro _demPro = new DemenagementPro()
            {
                Demenagement = demsPro.Demenagement,
                IsHouse = demsPro.IsHouse,
                HasMultipleFloors = demsPro.HasMultipleFloors,
                StartZipCode = demsPro.StartZipCode,
                StartAddress = demsPro.StartAddress,
                DestZipCode = demsPro.DestZipCode,
                DestAddress = demsPro.DestAddress,
                IsStartingDateKnown = demsPro.IsStartingDateKnown,
                Volume = demsPro.Volume,
                Surface = demsPro.Surface
            };

            await _dpDemenagementPro.GetCollection().InsertOneAsync(_demPro);

            return _demPro.Id;
        }

        public async Task DeleteDemenagementPro(string id)
        {
            if(id != null)
            {
                DemenagementPro _demProToBeDeleted = await _dpDemenagementPro.GetDemenagementById(id).FirstOrDefaultAsync();
                if(_demProToBeDeleted == null) throw new Exception("The service you are requesting does not exist");

                await _dpDemenagementPro.GetCollection().DeleteOneAsync(db => db.Id == id);
            }
        }

    }
}
