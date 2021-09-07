using MongoDB.Driver.Linq;
using Mydemenageur.API.Entities;
using Mydemenageur.API.Models.Reviews;
using System.Collections.Generic;
using Mydemenageur.API.Services.Interfaces;
using System;
using System.Linq;
using System.Threading.Tasks;
using Mydemenageur.API.DP.Interface;
using System.Linq.Expressions;
using MongoDB.Driver;

namespace Mydemenageur.API.Services
{
    public class ReviewService: IReviewService
    {
        private readonly IDPReview _dpReview;
        private readonly IDPUser _dpUser;

        public ReviewService(IDPReview dpReview, IDPUser dpUser)
        {
            _dpReview = dpReview;
            _dpUser = dpUser;
        }

        public async Task<IList<ReviewModel>> GetAllReviews(string title, string description, string grade, string username, int size)
        {
            int iterator = 0;
            List<ReviewModel> dtoReview = new List<ReviewModel>();
            List<string> paramFilters = new List<string> { title, description, grade, username };
            List<Expression<Func<Review, bool>>> queries = new List<Expression<Func<Review, bool>>>();

            queries.Add(w => w.Title == paramFilters[0]);
            queries.Add(w => w.Description == paramFilters[1]);
            queries.Add(w => w.Grade == paramFilters[2]);

            IMongoQueryable<Review> _reviews = _dpReview.Obtain();

            foreach(string filterLabel in paramFilters)
            {
                string tmp = filterLabel;
                if(tmp != null) {
                    _reviews = _reviews.Where(queries[iterator]);
                }
                ++iterator;
            }

            if(size != 0)
            {
                _reviews.Take(size);
            }

            List<Review> reviews = _reviews.ToList();
            if(reviews.Count < 1)
            {
                return null;
            }

            foreach (Review r in reviews)
            {
                var user = _dpUser.GetFiltered(w => w.Username == username).ToList();
                ReviewModel tmpReview = new ReviewModel()
                {
                    Id = r.Id,
                    CreatedAt = r.CreatedAt,
                    Description = r.Description,
                    Grade = r.Grade,
                    FirstName = user[0].FirstName,
                    LastName = user[0].LastName
                };
                dtoReview.Add(tmpReview);
            }

            return dtoReview.ToList();
        }

        public async Task<ReviewModel> GetReviewById(string id)
        {
            Review _review = await _dpReview.GetReviewById(id).FirstOrDefaultAsync();
            if (_review == null) throw new Exception("The review does not exist");
            var user = _dpUser.GetUserById(id).FirstOrDefault();
            ReviewModel dtoReview = new ReviewModel()
            {
                Id = _review.Id,
                CreatedAt = _review.CreatedAt,
                Description = _review.Description,
                Grade = _review.Grade,
                Title = _review.Title,
                FirstName = user.FirstName,
                LastName = user.LastName
            };
            return dtoReview;
        }

        public async Task<string> CreateReview(string title, string description, string grade, string userId)
        {
            Review review = new Review()
            {
                UserId = userId,
                CreatedAt = DateTime.Now,
                Title = title,
                Description = description,
                Grade = grade
            };

            await _dpReview.GetCollection().InsertOneAsync(review);

            return review.Id;
        }

        public async Task UpdateReview(string title, string description, string grade, string reviewId, string userId)
        {
            Review reviewToBeUpdated = await _dpReview.GetReviewById(reviewId).FirstOrDefaultAsync();
            if (reviewToBeUpdated == null) throw new Exception("The review does not exist");
            if (userId != reviewToBeUpdated.UserId) throw new Exception("You cannot modify this review");

            var update = Builders<Review>.Update
                .Set(db => db.Title, title)
                .Set(db => db.Description, description)
                .Set(db => db.Grade, grade);


            await _dpReview.GetCollection().UpdateOneAsync(db =>
                db.Id == reviewId,
                update
            );
        }

        public async Task DeleteReview(string id)
        {
            if(id != null)
            {
                Review reviewToBeDeleted = await _dpReview.GetReviewById(id).FirstOrDefaultAsync();
                if (reviewToBeDeleted == null) throw new Exception("The review does not exist");

                await _dpReview.GetCollection().DeleteOneAsync<Review>(db => db.Id == id);
            }
        }
    }
}
