using Microsoft.Extensions.Options;
using MongoDB.Driver;
using MongoDB.Driver.Linq;
using Mydemenageur.DAL.Contexts;
using Mydemenageur.DAL.DP.Interface;
using Mydemenageur.DAL.Models.Reviews;
using Mydemenageur.DAL.Settings;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace Mydemenageur.DAL.DP.DataProvider
{
    public class DPReview : IDPReview
    {
        private readonly MyDemenageurContext _db;

        public DPReview(IOptions<MongoSettings> setting)
        {
            this._db = new MyDemenageurContext(setting);
        }

        public IMongoQueryable<Review> Obtain()
        {
            return this._db.Review.AsQueryable();
        }

        public IMongoCollection<Review> GetCollection()
        {
            return this._db.Review;
        }

        public IMongoQueryable<Review> GetReviewById(string idReview)
        {
            return _db.Review.AsQueryable().Where(w => w.Id == idReview);
        }

        public IMongoQueryable<Review> GetFiltered(Expression<Func<Review, bool>> predicate)
        {
            return _db.Review.AsQueryable()
                .Where(predicate);
        }
    }
}
