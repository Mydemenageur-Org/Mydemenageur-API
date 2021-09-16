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
    public class DemenagementIndivService
    {
        private readonly IDPDemenagementParticulier _dpDemenagementIndiv;

        public DemenagementIndivService(IDPDemenagementParticulier dPDemenagementIndiv)
        {
            _dpDemenagementIndiv = dPDemenagementIndiv;
        }

        public async Task<IList<DemenagementIndivModel>> GetAllDemenagement(Nullable<DateTime> date, bool askHelpStart, bool askHelpDest, bool isFlexibleDate, string fromCity, string toCity, string personNeeded, string volume, string serviceType, string demenagementType, int size)
        {
            int iterator = 0;
            List<DemenagementIndividuel> _demIndiv = new List<DemenagementIndividuel>();
            List<DemenagementIndivModel> _demIndivModel = new List<DemenagementIndivModel>();

            List<string> paramStr = new List<string>() { fromCity, toCity, personNeeded, volume, serviceType, demenagementType };

            List<Expression<Func<DemenagementIndividuel, bool>>> queries = new List<Expression<Func<DemenagementIndividuel, bool>>>();

            queries.Add(w => w.Demenagement.FromCity == paramStr[0]);
            queries.Add(w => w.Demenagement.ToCity == paramStr[1]);
            queries.Add(w => w.PersonnNeeded == paramStr[2]);
            queries.Add(w => w.Volume == paramStr[3]);
            queries.Add(w => w.Demenagement.ServiceType == paramStr[4]);
            queries.Add(w => w.Demenagement.MoveType == paramStr[5]);

            IMongoQueryable<DemenagementIndividuel> _dem = _dpDemenagementIndiv.Obtain();

            foreach(string s in paramStr)
            {
                string buff = s;
                if(buff != null)
                {
                    _dem = _dem.Where(queries[iterator]);
                }
                ++iterator;
            }

            _dem = _dem.Where(w => w.AskHelpStart == askHelpStart);
            _dem = _dem.Where(w => w.AskHelpDest == askHelpDest);
            _dem = _dem.Where(w => w.IsFlexibleDate == isFlexibleDate);

            if(date != null)
            {
                _dem = _dem.Where(w => w.Demenagement.MoveDate == date);
            }

            if(size != 0)
            {
                _dem.Take(size);
            }

            _demIndiv = await _dem.ToListAsync();

            foreach(DemenagementIndividuel dem in _demIndiv)
            {
                DemenagementIndivModel tmp = dem.ToModelDemIndiv();
                _demIndivModel.Add(tmp);
            }

            return _demIndivModel;
        }

        public async Task<DemenagementIndividuel> GetDemenagementIndivById(string id)
        {
            DemenagementIndividuel _demIndiv = await _dpDemenagementIndiv.GetDemenagementIndivById(id).FirstOrDefaultAsync();
            return _demIndiv;
        }

        public async Task<string> CreateDemenagementIndividuel(DemenagementIndivModel demIndiv)
        {
            DemenagementIndividuel _dem  = new DemenagementIndividuel()
            {
                Demenagement = demIndiv.Demenagement,
                AskHelpDest = demIndiv.AskHelpDest,
                AskHelpStart = demIndiv.AskHelpStart,
                IsFlexibleDate = demIndiv.IsFlexibleDate,
                PersonnNeeded = demIndiv.PersonnNeeded,
                Volume = demIndiv.Volume
            };

            await _dpDemenagementIndiv.GetCollection().InsertOneAsync(_dem);

            return _dem.Id;
        }

        public async Task UpdateDemenagementIndividuel(string id, DemenagementIndivModel demIndiv)
        {
            DemenagementIndividuel _demIndiv = await _dpDemenagementIndiv.GetDemenagementIndivById(id).FirstOrDefaultAsync();
            if (_demIndiv != null) throw new Exception("The service you are requesting does not exist");
            if (_demIndiv.Demenagement.UserId != demIndiv.Demenagement.UserId) throw new Exception("You are not allowed to make this request");

            var update = Builders<DemenagementIndividuel>.Update
                .Set(db => db.Demenagement, demIndiv.Demenagement)
                .Set(db => db.AskHelpDest, demIndiv.AskHelpDest)
                .Set(db => db.AskHelpStart, demIndiv.AskHelpStart)
                .Set(db => db.IsFlexibleDate, demIndiv.IsFlexibleDate)
                .Set(db => db.PersonnNeeded, demIndiv.PersonnNeeded)
                .Set(db => db.Volume, demIndiv.Volume);

            await _dpDemenagementIndiv.GetCollection().UpdateOneAsync(db => db.Id == id, update);

        }

        public async Task DeleteDemenagementIndividuel(string id)
        {
            if(id != null)
            {
                DemenagementIndividuel _dem = await _dpDemenagementIndiv.GetDemenagementIndivById(id).FirstOrDefaultAsync();
                if (_dem == null) throw new Exception("The service you are requesting does not exist");

                await _dpDemenagementIndiv.GetCollection().DeleteManyAsync(db => db.Id == id);
            }
        }

    }
}
