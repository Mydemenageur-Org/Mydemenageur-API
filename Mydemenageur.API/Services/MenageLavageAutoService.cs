
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
    public class MenageLavageAutoService
    {
            private readonly IDPMenageLavageAuto _dpMenageLavageAuto;

            public MenageLavageAutoService(IDPMenageLavageAuto dPMenageLavageAuto)
            {
                _dpMenageLavageAuto = dPMenageLavageAuto;
            }

            public async Task<IList<MenageLavageAutoModel>> GetAllMenage(string vehiculeType, Nullable<DateTime> dateEvent, string budget, string menageType, bool isPro, int size)
            {
                int iterator = 0;
                List<MenageLavageAuto> _menageLavageAutos = new List<MenageLavageAuto>();
                List<MenageLavageAutoModel> _menageLavageAutoModels = new List<MenageLavageAutoModel>();

                List<string> paramStr = new List<string>() { vehiculeType, budget, menageType };

                List<Expression<Func<MenageLavageAuto, bool>>> queries = new List<Expression<Func<MenageLavageAuto, bool>>>();

                queries.Add(w => w.VehiculeType == paramStr[0]);
                queries.Add(w => w.Menage.Budget == paramStr[2]);
                queries.Add(w => w.Menage.MenageType == paramStr[3]);

                IMongoQueryable<MenageLavageAuto> _men = _dpMenageLavageAuto.Obtain();

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

                _menageLavageAutos = await _men.ToListAsync();

                foreach (MenageLavageAuto men in _menageLavageAutos)
                {
                    MenageLavageAutoModel tmp = men.ToModelMen();
                    _menageLavageAutoModels.Add(tmp);
                }

                return _menageLavageAutoModels;

            }


            public async Task<MenageLavageAuto> GetMenageById(string id)
            {
                MenageLavageAuto _men = await _dpMenageLavageAuto.GetMenageById(id).FirstOrDefaultAsync();
                return _men;
            }

            public async Task<string> CreateMenageDomicile(MenageLavageAutoModel men)
            {
                MenageLavageAuto menage = new MenageLavageAuto()
                {
                    Menage = men.Menage,
                    VehiculeType = men.VehiculeType
                };

                await _dpMenageLavageAuto.GetCollection().InsertOneAsync(menage);

                return menage.Id;
            }

            public async Task UpdateMenageDomicile(string id, MenageLavageAutoModel menageDomicile)
            {
                MenageLavageAuto _men = await _dpMenageLavageAuto.GetMenageById(id).FirstOrDefaultAsync();
                if (_men != null) throw new Exception("The service you are requesting does not exist");
                if (_men.Menage.UserId != menageDomicile.Menage.UserId) throw new Exception("You are not allowed to make this request");

                var update = Builders<MenageLavageAuto>.Update
                    .Set(db => db.Menage, menageDomicile.Menage)
                    .Set(db => db.VehiculeType, menageDomicile.VehiculeType);

                await _dpMenageLavageAuto.GetCollection().UpdateOneAsync(db => db.Id == id, update);
            }

            public async Task DeleteMenageDomicile(string id)
            {
                if (id != null)
                {
                    MenageLavageAuto _dem = await _dpMenageLavageAuto.GetMenageById(id).FirstOrDefaultAsync();
                    if (_dem == null) throw new Exception("The service you are requesting does not exist");

                    await _dpMenageLavageAuto.GetCollection().DeleteManyAsync(db => db.Id == id);
                }
            }
    }
}
