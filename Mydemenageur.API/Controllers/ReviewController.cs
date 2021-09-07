using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Mydemenageur.API.Entities;
using Mydemenageur.API.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Mydemenageur.API.Controllers
{
    [Route("api/[controller]")]
    [Authorize]
    [ApiController]
    public class ReviewController: ControllerBase
{
        private readonly IReviewService _reviewService;

        public ReviewController(IReviewService reviewService)
        {
            _reviewService = reviewService;
        }

        /// <summary>
        /// To get all reviews
        /// </summary>
        /// <returns></returns>
        /// <response code="200">Return the list of reviews</response>returns></returns>
        [HttpGet]
        public async Task<ActionResult<IList<Review>>> GetReviews([FromQuery] string grade, [FromQuery] string title = null, [FromQuery] string description = null, [FromQuery] string username = null, int size = 0)
        {
            return Ok(await _reviewService.GetAllReviews(title, description, grade, username, size));
        }

        /// <summary>
        /// To get a review with an id
        /// </summary>
        /// <returns></returns>
        /// <response code="200">Return a specific review by an id</response>returns></returns>
        [HttpGet("{id:length(24)}")]
        public async Task<ActionResult<Review>> GetReviewById(string id)
        {
            if(String.IsNullOrEmpty(id))
            {
                return BadRequest("Id is null or empty");
            }

            return Ok(await _reviewService.GetReviewById(id));
        }

        /// <summary>
        /// To create a review
        /// </summary>
        /// <returns></returns>
        /// <response code="200">Return the id of the review created</response>returns></returns>
        [AllowAnonymous]
        [HttpPost]
        public async Task<ActionResult<string>> CreateReview(string title, string description, string grade, string userId)
        {
            if(string.IsNullOrEmpty(title) || string.IsNullOrEmpty(description) || string.IsNullOrEmpty(grade) || string.IsNullOrEmpty(userId))
            {
                return BadRequest("You must enter params");
            }

            return Ok(await _reviewService.CreateReview(title, description, grade, userId));
        }

        /// <summary>
        /// To update a review
        /// </summary>
        /// <returns></returns>
        [HttpPut("{id:length(24)}")]
        public async Task<IActionResult> UpdateReview(string title, string description, string grade, string reviewId, string userId)
        {
            try
            {
                await _reviewService.UpdateReview(title, description, grade, reviewId, userId);
            } catch(Exception err)
            {
                return BadRequest(err);
            }

            return Ok();
        }


        /// <summary>
        /// To delete a review
        /// </summary>
        /// <returns></returns>
        [HttpDelete("{id:length(24)}")]
        public async Task<IActionResult> DeleteReview(string id)
        {
            try
            {
                await _reviewService.DeleteReview(id);
            } catch (Exception err)
            {
                return BadRequest(err);
            }

            return Ok();
        }
    }
}
