using MongoDB.Driver;
using Mydemenageur.DAL.Entities;
using Mydemenageur.DAL.Models.Demands;
using System;
using System.Threading.Tasks;
using Mydemenageur.DAL.DP.Interface;
using System.Collections.Generic;
using System.Linq.Expressions;
using MongoDB.Driver.Linq;
using Mydemenageur.BLL.Services.Interfaces;

namespace Mydemenageur.BLL.Services
{
    public class HelpService: IHelpService
    {
        private readonly IDPHelp _dpHelp;
        
        public HelpService(IDPHelp dpHelp)
        {
            _dpHelp = dpHelp;
        }

        public async Task<IList<Help>> GetAllHelpAnnounces(string type, string title, Nullable<DateTime> createdAt, string personNumber, Nullable<DateTime> timeNeeded, string start, string destination, bool isFlexible, bool isEmergency, Nullable<DateTime> plannifiedDate, string volume, bool askStart, bool askEnd, int size)
        {
            int iterator = 0;
            List<HelpModel> dtoAnnounces = new List<HelpModel>();
            List<string> paramsFilters = new List<string>() { type, title, personNumber, start, destination, volume };
            List<Expression<Func<Help, bool>>> queries = new List<Expression<Func<Help, bool>>>();

            queries.Add(w => w.Type == paramsFilters[0]);
            queries.Add(w => w.Title == paramsFilters[1]);
            queries.Add(w => w.PersonNumber == paramsFilters[2]);
            queries.Add(w => w.FromCity == paramsFilters[3]);
            queries.Add(w => w.ToCity == paramsFilters[4]);
            queries.Add(w => w.Volume == paramsFilters[5]);

            IMongoQueryable<Help> _helps = _dpHelp.Obtain();

            foreach (string filterLabel in paramsFilters)
            {
                string tmp = filterLabel;
                if (tmp != null)
                {
                    _helps = _helps.Where(queries[iterator]);
                }
                ++iterator;
            }

           if(createdAt != null)
            {
                _helps = _helps.Where(w => w.CreatedAt == createdAt);
            }
           
           if(timeNeeded != null)
            {
                _helps = _helps.Where(w => w.TimeNeeded == timeNeeded);
            }

           if(plannifiedDate != null)
            {
                _helps = _helps.Where(w => w.PlannifiedDate == plannifiedDate);
            }

            _helps = _helps.Where(w => w.IsFlexible == isFlexible);
            _helps = _helps.Where(w => w.IsEmergency == isEmergency);
            _helps = _helps.Where(w => w.AskHelpEndCity == askEnd);
            _helps = _helps.Where(w => w.AskHelpEndCity == askStart);

            if (size != 0)
            {
                _helps.Take(size);
            }

            return await _helps.ToListAsync();
        }

        public async Task<Help> GetHelpAnnounceById(string id)
        {
            return await _dpHelp.GetHelpById(id).FirstOrDefaultAsync();
        }

        public async Task<string> CreateHelpAnnounce(string type, string title, string personNumber, DateTime timeNeeded, string start, string destination, bool isFlexible, bool isEmergency, DateTime plannifiedDate, string volume, bool askStart, bool askEnd)
        {
            Help helpAnnounce = new Help()
            {
                Type = type,
                Title = title,
                CreatedAt = DateTime.Now,
                TimeNeeded = timeNeeded,
                FromCity = start,
                PersonNumber = personNumber,
                ToCity = destination,
                IsFlexible = isFlexible,
                IsEmergency = isEmergency,
                PlannifiedDate = plannifiedDate,
                Volume = volume,
                AskHelpEndCity = askEnd,
                AskHelpStartedCity = askStart
            };

            await _dpHelp.GetCollection().InsertOneAsync(helpAnnounce);

            return helpAnnounce.Id;
        }

        public async Task UpdateHelpAnnounce(string id, string type, string title, string personNumber, DateTime timeNeeded, string start, string destination, bool isFlexible, bool isEmergency, DateTime plannifiedDate, string volume, bool askStart, bool askEnd)
        {
            Help helpToBeUpdated = await _dpHelp.GetHelpById(id).FirstOrDefaultAsync();
            if (helpToBeUpdated == null) throw new Exception("The help announce does not exist");

            var update = Builders<Help>.Update
                .Set(db => db.Type, type)
                .Set(db => db.Title, title)
                .Set(db => db.PersonNumber, personNumber)
                .Set(db => db.TimeNeeded, timeNeeded)
                .Set(db => db.FromCity, start)
                .Set(db => db.ToCity, destination)
                .Set(db => db.IsFlexible, isFlexible)
                .Set(db => db.IsEmergency, isEmergency)
                .Set(db => db.PlannifiedDate, plannifiedDate)
                .Set(db => db.Volume, volume)
                .Set(db => db.AskHelpStartedCity, askStart)
                .Set(db => db.AskHelpEndCity, askEnd);

            await _dpHelp.GetCollection().UpdateOneAsync(db =>
                db.Id == id,
                update
            );
        }

        public async Task DeleteHelpAnnounce(string id)
        {
            if (id != null)
            {
                Help reviewToBeDeleted = await _dpHelp.GetHelpById(id).FirstOrDefaultAsync();
                if (reviewToBeDeleted == null) throw new Exception("The help announce does not exist");

                await _dpHelp.GetCollection().DeleteOneAsync<Help>(db => db.Id == id);
            }
        }
    }
}
