using MongoDB.Driver;
using MongoDB.Driver.Linq;
using Mydemenageur.BLL.Services.Interfaces;
using Mydemenageur.DAL.DP.Interface;
using Mydemenageur.DAL.Models.Reviews;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Mydemenageur.BLL.Services
{
    public class ReviewsService : IReviewsService
    {
        private readonly IDPReview _dpReview;
        //private readonly IDPMyDemenageurUser _dpUser;

        public ReviewsService(IDPReview dpReview)
        {
            _dpReview = dpReview;
        }

        public async Task<Review> GetReview(string id)
        {
            Review review = await _dpReview.Obtain().Where(x => x.Id == id).FirstOrDefaultAsync();

            return review;
        }

        public async Task<IList<Review>> GetReviews()
        {
            IList<Review> reviews = await _dpReview.Obtain().ToListAsync();
            return reviews;
        }

        public async Task<Review> CreateReview(Review review)
        {
            await _dpReview.GetCollection().InsertOneAsync(review);


            return review;
        }

        public async Task<string> UpdateReview(Review review)
        {
            await _dpReview.GetCollection().ReplaceOneAsync(
                dpReview => dpReview.Id == review.Id,
                review
            );

            return review.Id;
        }

        public async Task<string> DeleteReview(string id)
        {
            return null;
        }
    }
}
