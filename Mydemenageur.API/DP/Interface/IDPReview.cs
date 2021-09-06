using MongoDB.Driver.Linq;
using MongoDB.Driver;
using Mydemenageur.API.Entities;

namespace Mydemenageur.API.DP.Interface
{
    public interface IDPReview
    {
        public IMongoQueryable<Review> Obtain();
        public IMongoQueryable<Review> GetReviewById(string id);
        public IMongoCollection<Review> GetCollection();
    }
}
