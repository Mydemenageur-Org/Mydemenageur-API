
using Mydemenageur.DAL.Entities;
using System.Collections.Generic;
using System;
using System.Threading.Tasks;
using Mydemenageur.DAL.DP.Interface;
using Mydemenageur.DAL.Models.Services;
using System.Linq.Expressions;
using MongoDB.Driver.Linq;
using MongoDB.Driver;
using Mydemenageur.BLL.Extensions;

namespace Mydemenageur.BLL.Services
{
    public class MenageNettoyageVitreService
    {
        private readonly IDPMenageNettoyageVitre _dPMenageNettoyageVitre;

        public MenageNettoyageVitreService(IDPMenageNettoyageVitre dPMenageNettoyageVitre)
        {
            _dPMenageNettoyageVitre = dPMenageNettoyageVitre;
        }

        public async Task<IList<MenageNettoyageVitreModel>> GetAllMenage(string winNumber, Nullable<DateTime> dateEvent, string budget, string menageType, bool isPro, int size)
        {
            int iterator = 0;
            List<MenageNettoyageVitre> _menageNettoyageVitres = new List<MenageNettoyageVitre>();
            List<MenageNettoyageVitreModel> menageNettoyageVitreModels = new List<MenageNettoyageVitreModel>();

            List<string> paramStr = new List<string>() { winNumber, budget, menageType };

            List<Expression<Func<MenageNettoyageVitre, bool>>> queries = new List<Expression<Func<MenageNettoyageVitre, bool>>>();

            queries.Add(w => w.WindowNumber == paramStr[0]);
            queries.Add(w => w.Menage.Budget == paramStr[1]);
            queries.Add(w => w.Menage.MenageType == paramStr[2]);

            IMongoQueryable<MenageNettoyageVitre> _men = _dPMenageNettoyageVitre.Obtain();

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

            _menageNettoyageVitres = await _men.ToListAsync();

            foreach (MenageNettoyageVitre men in _menageNettoyageVitres)
            {
                MenageNettoyageVitreModel tmp = men.ToModelMen();
                menageNettoyageVitreModels.Add(tmp);
            }

            return menageNettoyageVitreModels;

        }


        public async Task<MenageNettoyageVitre> GetMenageById(string id)
        {
            MenageNettoyageVitre _men = await _dPMenageNettoyageVitre.GetMenageById(id).FirstOrDefaultAsync();
            return _men;
        }

        public async Task<string> CreateMenageDomicile(MenageNettoyageVitreModel men)
        {
            MenageNettoyageVitre menage = new MenageNettoyageVitre()
            {
                Menage = men.Menage,
                WindowNumber = men.WindowNumber
            };

            await _dPMenageNettoyageVitre.GetCollection().InsertOneAsync(menage);

            return menage.Id;
        }

        public async Task UpdateMenageDomicile(string id, MenageNettoyageVitreModel menageDomicile)
        {
            MenageNettoyageVitre _men = await _dPMenageNettoyageVitre.GetMenageById(id).FirstOrDefaultAsync();
            if (_men != null) throw new Exception("The service you are requesting does not exist");
            if (_men.Menage.UserId != menageDomicile.Menage.UserId) throw new Exception("You are not allowed to make this request");

            var update = Builders<MenageNettoyageVitre>.Update
                .Set(db => db.Menage, menageDomicile.Menage)
                .Set(db => db.WindowNumber, menageDomicile.WindowNumber);

            await _dPMenageNettoyageVitre.GetCollection().UpdateOneAsync(db => db.Id == id, update);
        }

        public async Task DeleteMenageDomicile(string id)
        {
            if (id != null)
            {
                MenageNettoyageVitre _dem = await _dPMenageNettoyageVitre.GetMenageById(id).FirstOrDefaultAsync();
                if (_dem == null) throw new Exception("The service you are requesting does not exist");

                await _dPMenageNettoyageVitre.GetCollection().DeleteManyAsync(db => db.Id == id);
            }
        }
    }
}
