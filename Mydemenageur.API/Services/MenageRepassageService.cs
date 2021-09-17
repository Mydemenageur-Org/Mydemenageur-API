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
    public class MenageRepassageService
    {
        private readonly IDPMenageRepassage _dpMenageRepassage;

        public MenageRepassageService(IDPMenageRepassage dpMenageRepassage)
        {
            _dpMenageRepassage = dpMenageRepassage;
        }

        public async Task<IList<MenageRepassageModel>> GetAllMenage(string clotheNumber, string frequency, Nullable<DateTime> dateEvent, string budget, string menageType, bool isPro, int size)
        {
            int iterator = 0;
            List<MenageRepassage> _menageRepassages = new List<MenageRepassage>();
            List<MenageRepassageModel> menageRepassages = new List<MenageRepassageModel>();

            List<string> paramStr = new List<string>() { clotheNumber, frequency, budget, menageType };

            List<Expression<Func<MenageRepassage, bool>>> queries = new List<Expression<Func<MenageRepassage, bool>>>();

            queries.Add(w => w.ClotheNumber == paramStr[0]);
            queries.Add(w => w.Frequency == paramStr[1]);
            queries.Add(w => w.Menage.Budget == paramStr[2]);
            queries.Add(w => w.Menage.MenageType == paramStr[3]);

            IMongoQueryable<MenageRepassage> _men = _dpMenageRepassage.Obtain();

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

            if (dateEvent != null)
            {
                _men = _men.Where(w => w.Menage.DateEvent == dateEvent);
            }

            if (size != 0)
            {
                _men.Take(size);
            }

            _menageRepassages = await _men.ToListAsync();

            foreach (MenageRepassage men in _menageRepassages)
            {
                MenageRepassageModel tmp = men.ToModelMen();
                menageRepassages.Add(tmp);
            }

            return menageRepassages;

        }


        public async Task<MenageRepassage> GetMenageById(string id)
        {
            MenageRepassage _men = await _dpMenageRepassage.GetMenageById(id).FirstOrDefaultAsync();
            return _men;
        }

        public async Task<string> CreateMenageDomicile(MenageRepassageModel men)
        {
            MenageRepassage menage = new MenageRepassage()
            {
                Menage = men.Menage,
                ClotheNumber = men.ClotheNumber,
                Frequency = men.Frequency
            };

            await _dpMenageRepassage.GetCollection().InsertOneAsync(menage);

            return menage.Id;
        }

        public async Task UpdateMenageDomicile(string id, MenageRepassageModel menage)
        {
            MenageRepassage _men = await _dpMenageRepassage.GetMenageById(id).FirstOrDefaultAsync();
            if (_men != null) throw new Exception("The service you are requesting does not exist");
            if (_men.Menage.UserId != menage.Menage.UserId) throw new Exception("You are not allowed to make this request");

            var update = Builders<MenageRepassage>.Update
                .Set(db => db.Menage, menage.Menage)
                .Set(db => db.ClotheNumber, menage.ClotheNumber)
                .Set(db => db.Frequency, menage.Frequency);

            await _dpMenageRepassage.GetCollection().UpdateOneAsync(db => db.Id == id, update);
        }

        public async Task DeleteMenageDomicile(string id)
        {
            if (id != null)
            {
                MenageRepassage _dem = await _dpMenageRepassage.GetMenageById(id).FirstOrDefaultAsync();
                if (_dem == null) throw new Exception("The service you are requesting does not exist");

                await _dpMenageRepassage.GetCollection().DeleteManyAsync(db => db.Id == id);
            }
        }
    }
}
