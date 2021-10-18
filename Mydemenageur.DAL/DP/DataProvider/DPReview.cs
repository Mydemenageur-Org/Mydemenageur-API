using Mydemenageur.DAL.DP.Interface;
using Mydemenageur.DAL.Contexts;
using Mydemenageur.DAL.Settings;
using Microsoft.Extensions.Options;
using MongoDB.Driver;
using Mydemenageur.DAL.Entities;
using MongoDB.Driver.Linq;

namespace Mydemenageur.DAL.DP.DataProvider
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
