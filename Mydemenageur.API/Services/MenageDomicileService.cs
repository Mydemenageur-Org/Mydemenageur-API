
using Mydemenageur.API.Entities;
using System.Collections.Generic;
using System;
using System.Threading.Tasks;
using Mydemenageur.API.DP.Interface;
using Mydemenageur.API.Models.Services;
using System.Linq.Expressions;
using MongoDB.Driver.Linq;
using MongoDB.Driver;
using Mydemenageur.API.Extensions;

namespace Mydemenageur.API.Services
{
    public class MenageDomicileService
    {
        private readonly IDPMenageDomicile _dpMenageDomicile;

        public MenageDomicileService(IDPMenageDomicile dPMenageDomicile)
        {
            _dpMenageDomicile = dPMenageDomicile;
        }

        public async Task<IList<MenageDomicileModel>> GetAllMenageDomicile(string surface, string frequency, Nullable<DateTime> dateEvent, string budget, string menageType, bool isPro, int size)
        {
            int iterator = 0;
            List<MenageDomicile> _menDomicile = new List<MenageDomicile>();
            List<MenageDomicileModel> _menDomModel = new List<MenageDomicileModel>();

            List<string> paramStr = new List<string>() { surface, frequency, budget, menageType };

            List<Expression<Func<MenageDomicile, bool>>> queries = new List<Expression<Func<MenageDomicile, bool>>>();

            queries.Add(w => w.Surface == paramStr[0]);
            queries.Add(w => w.Frequency == paramStr[1]);
            queries.Add(w => w.Menage.Budget == paramStr[2]);
            queries.Add(w => w.Menage.MenageType == paramStr[3]);

            IMongoQueryable<MenageDomicile> _men = _dpMenageDomicile.Obtain();

            foreach (string s in paramStr)
            {
                string buff = s;
                if (buff != null)
                {
                    _men = _men.Where(queries[iterator]);
                }
                ++iterator;
            }

            _men = _men.Where(w => w.Menage.IsPro == isPro);

            if(dateEvent != null)
            {
                _men = _men.Where(w => w.Menage.DateEvent == dateEvent);
            }

            if(size != 0)
            {
                _men.Take(size);
            }

            _menDomicile = await _men.ToListAsync();

            foreach (MenageDomicile men in _menDomicile)
            {
                MenageDomicileModel tmp = men.ToModelMen();
                _menDomModel.Add(tmp);
            }

            return _menDomModel;

        }

        public async Task<MenageDomicile> GetMenageById(string id)
        {
            MenageDomicile _men = await _dpMenageDomicile.GetMenageById(id).FirstOrDefaultAsync();
            return _men;
        }

        public async Task<string> CreateMenageDomicile(MenageDomicileModel men)
        {
            MenageDomicile menage = new MenageDomicile()
            {
                Menage = men.Menage,
                Surface = men.Surface,
                AdditionnalNeeds = men.AdditionnalNeeds,
                Frequency = men.Frequency
            };

            await _dpMenageDomicile.GetCollection().InsertOneAsync(menage);

            return menage.Id;
        }

        public async Task UpdateMenageDomicile(string id, MenageDomicileModel menageDomicile)
        {
            MenageDomicile _men = await _dpMenageDomicile.GetMenageById(id).FirstOrDefaultAsync();
            if (_men != null) throw new Exception("The service you are requesting does not exist");
            if (_men.Menage.UserId != menageDomicile.Menage.UserId) throw new Exception("You are not allowed to make this request");

            var update = Builders<MenageDomicile>.Update
                .Set(db => db.Menage, menageDomicile.Menage)
                .Set(db => db.Surface, menageDomicile.Surface)
                .Set(db => db.AdditionnalNeeds, menageDomicile.AdditionnalNeeds)
                .Set(db => db.Frequency, menageDomicile.Frequency);

            await _dpMenageDomicile.GetCollection().UpdateOneAsync(db => db.Id == id, update);
        }

        public async Task DeleteMenageDomicile(string id)
        {
            if (id != null)
            {
                MenageDomicile _dem = await _dpMenageDomicile.GetMenageById(id).FirstOrDefaultAsync();
                if (_dem == null) throw new Exception("The service you are requesting does not exist");

                await _dpMenageDomicile.GetCollection().DeleteManyAsync(db => db.Id == id);
            }
        }

    }
}
