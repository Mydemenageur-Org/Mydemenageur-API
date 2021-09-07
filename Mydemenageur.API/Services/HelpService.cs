using MongoDB.Driver;
using Mydemenageur.API.Entities;
using Mydemenageur.API.Models.Demands;
using System;
using System.Threading.Tasks;
using Mydemenageur.API.DP.Interface;
using System.Collections.Generic;
using System.Linq.Expressions;
using MongoDB.Driver.Linq;

namespace Mydemenageur.API.Services
{
    public class HelpService: IHelpService
    {
        private readonly IDPHelp _dpHelp;
        
        public HelpService(IDPHelp dpHelp)
        {
            _dpHelp = dpHelp;
        }

        public async Task<IList<HelpModel>> GetAllHelpAnnounces(string type, string title, DateTime createdAt, string personNumber, DateTime timeNeeded, string start, string destination, string budget, List<Service> services, int size)
        {
            int iterator = 0;
            List<HelpModel> dtoAnnounces = new List<HelpModel>();
            List<string> paramsFilters = new List<string>() { type, title, personNumber, start, destination, budget };
            List<Expression<Func<Help, bool>>> queries = new List<Expression<Func<Help, bool>>>();

            queries.Add(w => w.Type == paramsFilters[0]);
            queries.Add(w => w.Title == paramsFilters[1]);
            queries.Add(w => w.PersonNumber == paramsFilters[2]);
            queries.Add(w => w.FromCity == paramsFilters[3]);
            queries.Add(w => w.ToCity == paramsFilters[4]);
            queries.Add(w => w.Budget == paramsFilters[5]);

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

            


        }

        public async Task<HelpModel> GetHelpAnnounceById(string id)
        {

        }

        public async Task<string> CreateHelpAnnounce(string type, string title, DateTime createdAt, string personNumber, DateTime timeNeeded, string start, string destination, string budget, List<Service> services)
        {

        }

        public async Task UpdateHelpAnnounce(string id)
        {

        }

        public async Task DeleteHelpAnnounce(string id)
        {

        }
    }
}
