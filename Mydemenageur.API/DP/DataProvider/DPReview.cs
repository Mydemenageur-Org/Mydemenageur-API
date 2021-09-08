using Mydemenageur.API.DP.Interface;
using Mydemenageur.API.Contexts;
using Mydemenageur.API.Settings;
using Microsoft.Extensions.Options;
using MongoDB.Driver;
using Mydemenageur.API.Entities;
using MongoDB.Driver.Linq;

namespace Mydemenageur.API.DP.DataProvider
{
    public class DPReview: IDPReview
    {
        private readonly MyDemenageurContext _db;

        public DPReview(IOptions<MongoSettings> setting)
        {
            _db = new MyDemenageurContext(setting);
        }

        public IMongoQueryable<Review> Obtain()
        {
            return _db.Review.AsQueryable();
        }

        public IMongoCollection<Review> GetCollection()
        {
            return _db.Review;
        }

        public IMongoQueryable<Review> GetReviewById(string id)
        {
            return _db.Review.AsQueryable().Where(w => w.Id == id);
        }
    }
}
