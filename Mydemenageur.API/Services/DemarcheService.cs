using Mydemenageur.API.Entities;
using System.Collections.Generic;
using System;
using System.Threading.Tasks;
using Mydemenageur.API.DP.Interface;
using Mydemenageur.API.Models.Demarche;
using System.Linq.Expressions;
using MongoDB.Driver.Linq;
using MongoDB.Driver;
using Mydemenageur.API.Extensions;
using Mydemenageur.API.Services.Interfaces;

namespace Mydemenageur.API.Services
{
    public class DemarcheService: IDemarcheService
    {
        private readonly IDPDemarche _dpDemarche;

        public DemarcheService(IDPDemarche dPDemarche)
        {
            _dpDemarche = dPDemarche;
        }

        public async Task<IList<DemarcheModel>> GetAllDemarches(Nullable<DateTime> dateMove, bool hasAlreadyMoved, bool energyNotification, bool boxAndMobileNotification, bool assurance, bool alarmAndMonitors, bool bankAccount, bool grayCard, bool devisDemenagement, int size)
        {
            int iterator = 0;
            List<Demarche> _menDomicile = new List<Demarche>();
            List<DemarcheModel> _menDomModel = new List<DemarcheModel>();

            List<bool> paramStr = new List<bool>() { hasAlreadyMoved, energyNotification, boxAndMobileNotification, assurance, alarmAndMonitors, bankAccount, grayCard , devisDemenagement };

            List<Expression<Func<Demarche, bool>>> queries = new List<Expression<Func<Demarche, bool>>>();

            queries.Add(w => w.HasAlreadyMovedIn == paramStr[0]);
            queries.Add(w => w.EnergyNotification == paramStr[1]);
            queries.Add(w => w.BoxAndMobileNotification == paramStr[2]);
            queries.Add(w => w.Assurance == paramStr[3]);
            queries.Add(w => w.AlarmAndMonitor == paramStr[4]);
            queries.Add(w => w.BankAccount == paramStr[5]);
            queries.Add(w => w.GrayCard == paramStr[6]);
            queries.Add(w => w.DevisDemenagement == paramStr[7]);

            IMongoQueryable<Demarche> _demarche = _dpDemarche.Obtain();

            foreach (bool s in paramStr)
            {
                _demarche = _demarche.Where(queries[iterator]);
                ++iterator;
            }

            if (dateMove != null)
            {
                _demarche = _demarche.Where(w => w.DemenagementDate == dateMove);
            }

            if (size != 0)
            {
                _demarche.Take(size);
            }

            _menDomicile = await _demarche.ToListAsync();

            foreach (Demarche men in _menDomicile)
            {
                DemarcheModel tmp = men.ToModelMen();
                _menDomModel.Add(tmp);
            }

            return _menDomModel;
        }

        public async Task<Demarche> GetDemarcheById(string id)
        {
            Demarche _demarche = await _dpDemarche.GetDemarcheById(id).FirstOrDefaultAsync();
            return _demarche;
        }

        public async Task<string> CreateDemarche(DemarcheModel men)
        {
            Demarche menage = new Demarche()
            {
                DemenagementDate = men.DemenagementDate,
                HasAlreadyMovedIn = men.HasAlreadyMovedIn,
                EnergyNotification = men.EnergyNotification,
                BoxAndMobileNotification = men.BoxAndMobileNotification,
                Assurance = men.Assurance,
                AlarmAndMonitor = men.AlarmAndMonitor,
                BankAccount = men.BankAccount,
                GrayCard = men.GrayCard,
                DevisDemenagement = men.DevisDemenagement,
                UserId = men.UserId,
                title = men.title,
                description = men.description,
                CreatedAt = DateTime.Now
            };

            await _dpDemarche.GetCollection().InsertOneAsync(menage);

            return menage.Id;
        }

        public async Task UpdateDemarche(string id, DemarcheModel demarche)
        {
            Demarche _men = await _dpDemarche.GetDemarcheById(id).FirstOrDefaultAsync();
            if (_men != null) throw new Exception("The service you are requesting does not exist");
            if (_men.UserId != demarche.UserId) throw new Exception("You are not allowed to make this request");

            var update = Builders<Demarche>.Update
                .Set(db => db.DemenagementDate, demarche.DemenagementDate)
                .Set(db => db.HasAlreadyMovedIn, demarche.HasAlreadyMovedIn)
                .Set(db => db.EnergyNotification, demarche.EnergyNotification)
                .Set(db => db.BoxAndMobileNotification, demarche.BoxAndMobileNotification)
                .Set(db => db.Assurance, demarche.Assurance)
                .Set(db => db.AlarmAndMonitor, demarche.AlarmAndMonitor)
                .Set(db => db.BankAccount, demarche.BankAccount)
                .Set(db => db.GrayCard, demarche.GrayCard)
                .Set(db => db.DevisDemenagement, demarche.DevisDemenagement)
                .Set(db => db.title, demarche.title)
                .Set(db => db.description, demarche.description);

            await _dpDemarche.GetCollection().UpdateOneAsync(db => db.Id == id, update);
        }

        public async Task DeleteDemarche(string id)
        {
            if (id != null)
            {
                Demarche _dem = await _dpDemarche.GetDemarcheById(id).FirstOrDefaultAsync();
                if (_dem == null) throw new Exception("The service you are requesting does not exist");

                await _dpDemarche.GetCollection().DeleteManyAsync(db => db.Id == id);
            }
        }
    }
}
