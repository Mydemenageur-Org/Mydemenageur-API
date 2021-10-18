using MongoDB.Driver.Linq;
using MongoDB.Driver;
using Mydemenageur.DAL.Entities;

namespace Mydemenageur.DAL.DP.Interface
{
    public interface IDPReview
    {
        public IMongoQueryable<Review> Obtain();
        public IMongoQueryable<Review> GetReviewById(string id);
        public IMongoCollection<Review> GetCollection();
    }
}
