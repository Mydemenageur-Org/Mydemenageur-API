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
        public Task<IList<ReviewPopulated>> GetReviewsBetween(string receiver, string deposer);
        public Task<Review> CreateReview(Review review);
        public Task<string> UpdateReview(Review review);
        public Task<string> DeleteReview(string id);
        public Task<IList<ReviewAllopulated>> GetAllReviews(int pageNumber = -1, int numberOfElementsPerPage = -1);
        public long CountReviews();
        public Task<SchemaReview> GetSchemasStat();
        public Task<SchemaReview> GetSchemaStatFromUser(string id);
        public Task<List<ReviewAllopulated>> GetReviewsFromUser(string mdUserId, bool count);
    }
}
