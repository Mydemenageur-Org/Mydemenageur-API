using MongoDB.Bson;
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
        private readonly IDPMyDemenageurUser _dpUser;

        public ReviewsService(IDPReview dpReview, IDPMyDemenageurUser dpMyDemenageurUser)
        {
            _dpUser = dpMyDemenageurUser;
            _dpReview = dpReview;
        }

        public async Task<Review> GetReview(string id)
        {
            Review review = await _dpReview.Obtain().Where(x => x.Id == id).FirstOrDefaultAsync();

            return review;
        }

        public async Task<IList<ReviewAllopulated>> GetAllReviews(int pageNumber = -1, int numberOfElementsPerPage = -1)
        {
            List<ReviewAllopulated> reviews = new List<ReviewAllopulated>();

            var cursor = _dpReview.GetCollection().Find(new BsonDocument())
                    .SortByDescending(review => review.CreatedAt);

            if (pageNumber >= 0 && numberOfElementsPerPage > 0)
            {
                cursor.Limit(numberOfElementsPerPage).Skip(pageNumber * numberOfElementsPerPage);
            }

            cursor.ToListAsync().Result.ForEach((review) => {
                var myDem = _dpUser.GetUserById(review.Deposer).FirstOrDefault();
                var reciever = _dpUser.GetUserById(review.Receiver).FirstOrDefault();
                ReviewAllopulated reviewsPopulated = new ReviewAllopulated
                {
                    Id = review.Id,
                    Deposer = myDem,
                    Receiver = reciever,
                    Note = review.Note,
                    Description = review.Description,
                    CreatedAt = review.CreatedAt,
                    UpdatedAt = review.UpdatedAt,
                    Commentaires = review.Commentaires
                };
                reviews.Add(reviewsPopulated);
            });
            return reviews;
        }

        public async Task<IList<ReviewPopulated>> GetReviews(string id)
        {
            List<ReviewPopulated> reviews = new List<ReviewPopulated>();

            var cursor = _dpReview.Obtain().Where(x => x.Receiver == id);

            cursor.ToListAsync().Result.ForEach((review) => {
                 var myDem = _dpUser.GetUserById(review.Deposer).FirstOrDefault();
                 ReviewPopulated reviewsPopulated = new ReviewPopulated
                 {
                     Id = review.Id,
                     Deposer = myDem,
                     Receiver = review.Receiver,
                     Note = review.Note,
                     Description = review.Description,
                     CreatedAt = review.CreatedAt,
                     UpdatedAt = review.UpdatedAt,
                     Commentaires = review.Commentaires
                 };
                 reviews.Add(reviewsPopulated);
             });
            return reviews;
        }
        public long CountReviews()
        {
            long reviewsNumber = _dpReview.Obtain().ToList().Count();
            return reviewsNumber;
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
