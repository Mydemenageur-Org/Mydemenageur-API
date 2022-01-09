using Mydemenageur.DAL.Models.Reviews;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Mydemenageur.BLL.Services.Interfaces
{
    public interface IReviewsService
    {
        public Task<Review> GetReview(string id);
        public Task<IList<ReviewPopulated>> GetReviews(string id);
        public Task<Review> CreateReview(Review review);
        public Task<string> UpdateReview(Review review);
        public Task<string> DeleteReview(string id);
        public Task<IList<ReviewAllopulated>> GetAllReviews();
    }
}
