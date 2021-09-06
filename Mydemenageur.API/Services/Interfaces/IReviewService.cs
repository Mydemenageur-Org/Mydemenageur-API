using Mydemenageur.API.Entities;
using Mydemenageur.API.Models.Reviews;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Mydemenageur.API.Services.Interfaces
{
    public interface IReviewService
    {
        public Task<IList<ReviewModel>> GetAllReviews(string title, string description, string grade, string username, int size);
        public Task<ReviewModel> GetReviewById(string id);
        public Task UpdateReview(string title, string description, string grade, string reviewId, string userId);
        public Task DeleteReview(string id);
        public Task<string> CreateReview(string title, string descriptiob, string grade, string userId);
    }
}
