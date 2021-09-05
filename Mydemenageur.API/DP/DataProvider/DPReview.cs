using Mydemenageur.API.DP.Interface;
using Mydemenageur.API.Contexts;
using Mydemenageur.API.Settings;
using Microsoft.Extensions.Options;
using MongoDB.Driver;
using Mydemenageur.API.Entities;


namespace Mydemenageur.API.DP.DataProvider
{
    public class DPReview: IDPReview
    {
        private readonly MyDemenageurContext _db;

        public DPReview(IOptions<MongoSettings> setting)
        {
            _db = new MyDemenageurContext(setting);
        }

        public IMongoCollection<Review> Obtain()
        {
            return _db.Review;
        }
    }
}
