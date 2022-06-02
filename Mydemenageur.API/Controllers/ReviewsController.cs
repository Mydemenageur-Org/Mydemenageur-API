using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Mydemenageur.BLL.Services.Interfaces;
using Mydemenageur.DAL.Models.Reviews;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Mydemenageur.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ReviewsController : ControllerBase
    {
        private readonly IReviewsService _reviewsService;

        public ReviewsController(IReviewsService reviewsService)
        {
            _reviewsService = reviewsService;
        }

        /// <summary>
        /// Get a review by ID
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet("{id:length(24)}")]
        public async Task<ActionResult<Review>> GetReview(string id)
        {
            Review review = await _reviewsService.GetReview(id);

            if (review == null)
            {
                return NotFound();
            }

            return Ok(review);
        }

        /// <summary>
        /// Get all reviews
        /// </summary>
        /// <returns></returns>
        [HttpGet("")]
        public async Task<ActionResult<IList<ReviewAllopulated>>> GetReviews([FromQuery] int pageNumber = -1, [FromQuery] int numberOfElementsPerPage = -1)
        {
            IList<ReviewAllopulated> reviews = await _reviewsService.GetAllReviews(pageNumber, numberOfElementsPerPage);

            return Ok(reviews);
        }

        [HttpPost("get-user-reviews")]
        public async Task<ActionResult<List<ReviewAllopulated>>> GetReviewsFromUser([FromBody] string id) 
        {
            bool count = false;
            List<ReviewAllopulated> result = await _reviewsService.GetReviewsFromUser(id, count);
            if(result.Count == 0)
            {
                return result;
            }

            return result;
        } 

        /// <summary>
        /// Get global schema
        /// </summary>
        /// <returns></returns>
        [HttpGet("default-schema")]
        public async Task<ActionResult<SchemaReview>> GetGlobalSchema()
        {
            SchemaReview reviews = await _reviewsService.GetSchemasStat();

            return Ok(reviews);
        }

        /// <summary>
        /// Get schema from a specific provider
        /// </summary>
        /// <returns></returns>
        [HttpGet("default-schema/{id:length(24)}")]
        public async Task<ActionResult<SchemaReview>> GetGlobalSchema(string providerId)
        {
            SchemaReview reviews = await _reviewsService.GetSchemaStatFromUser(providerId);

            return Ok(reviews);
        }



        /// <summary>
        /// Get all reviews from a user
        /// </summary>
        /// <returns></returns>
        [HttpGet("recipient/{id:length(24)}")]
        public async Task<ActionResult<IList<ReviewPopulated>>> GetReviews(string id)
        {
            IList<ReviewPopulated> reviews = await _reviewsService.GetReviews(id);

            return Ok(reviews);
        }
        
        /// <summary>
        /// Get all reviews between two users
        /// </summary>
        /// <returns></returns>
        [HttpGet("between/{receiver:length(24)}/{deposer:length(24)}")]
        public async Task<ActionResult<IList<ReviewPopulated>>> GetReviewsBetween(string receiver, string deposer)
        {
            IList<ReviewPopulated> reviews = await _reviewsService.GetReviewsBetween(receiver, deposer);
            return Ok(reviews);
        }

        /// <summary>
        /// Get the number of reviews
        /// </summary>
        /// <returns></returns>
        [HttpGet("count")]
        public ActionResult<int> GetReviewsCount()
        {
            var count = _reviewsService.CountReviews();
            return Ok(count);
        }

        /// <summary>
        /// Create a new review
        /// </summary>
        /// <param name="demandCreated"></param>
        /// <returns></returns>
        [HttpPost()]
        public async Task<ActionResult<Review>> CreateReview(Review review)
        {
            try
            {
                Review reviewCreated = await _reviewsService.CreateReview(review);

                return NoContent();
            }
            catch (Exception e)
            {
                return BadRequest($"Error during the request: {e.Message}");
            }
        }

        /// <summary>
        /// Update a review
        /// </summary>
        /// <param name="id"></param>
        /// <param name="reviewToUpdate"></param>
        /// <returns></returns>
        [HttpPut("{id:length(24)}")]
        public async Task<ActionResult<string>> UpdateReview(string id, [FromBody] ReviewUpdater reviewToUpdate)
        {
            if (reviewToUpdate.Id != id)
            {
                return BadRequest("The id of the generic review doesn't match the resource id");
            }

            // TODO: improve security here
            try
            {
                string result = await _reviewsService.UpdateReview(reviewToUpdate);

                return Ok(result);
            }
            catch (Exception e)
            {
                return BadRequest($"Error during the request: {e.Message}");
            }

            ///// <summary>
            ///// delete a demand
            ///// </summary>
            ///// <param name="id"></param>
            ///// <returns></returns>
            //[HttpDelete()]
            //public async Task<ActionResult<string>> DeleteDemand(string id)
            //{

            //}
        }


    }
}
