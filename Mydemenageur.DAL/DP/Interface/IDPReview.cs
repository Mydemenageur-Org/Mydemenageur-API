using MongoDB.Driver;
using MongoDB.Driver.Linq;
using Mydemenageur.DAL.Models.Reviews;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace Mydemenageur.DAL.DP.Interface
{
    public interface IDPReview
    {
        public IMongoQueryable<Review> Obtain();

        public IMongoCollection<Review> GetCollection();

        public IMongoQueryable<Review> GetReviewById(string idReview);
        public IMongoQueryable<Review> GetFiltered(Expression<Func<Review, bool>> predicate);
    }
}
