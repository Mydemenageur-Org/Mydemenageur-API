using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Mydemenageur.DAL.Settings.Interfaces
{
    public interface IMongoSettings
    {
        string UsersCollectionName { get; set; }
        string ClientsCollectionName { get; set; }
        string MoversCollectionName { get; set; }
        string SocietiesCollectionName { get; set; }
        string VehiclesCollectionName { get; set; }
        string HousingsCollectionName { get; set; }
        string MoveRequestsCollectionName { get; set; }
        string PastActionsCollectionName { get; set; }

        string ConnectionString { get; set; }
        string DatabaseName { get; set; }
    }
}
