using MongoDB.Driver;
using Mydemenageur.API.Entities;

namespace Mydemenageur.API.DP.Interface
{
    public interface IDPReview
    {
        public IMongoCollection<Review> Obtain();
    }
}
