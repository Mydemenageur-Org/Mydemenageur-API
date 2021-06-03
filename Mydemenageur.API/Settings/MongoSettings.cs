using Mydemenageur.API.Settings.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Mydemenageur.API.Settings
{
    public class MongoSettings : IMongoSettings
    {
        public string UsersCollectionName { get; set; }
        public string ClientsCollectionName { get; set; }
        public string MoversCollectionName { get; set; }
        public string SocietiesCollectionName { get; set; }
        public string VehiclesCollectionName { get; set; }
        public string HousingsCollectionName { get; set; }
        public string MoveRequestsCollectionName { get; set; }

        public string ConnectionString { get; set; }
        public string DatabaseName { get; set; }
    }
}
