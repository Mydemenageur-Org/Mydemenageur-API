using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using Mydemenageur.API.Models.Users;

namespace Mydemenageur.API.Models.Reviews
{
    public class ReviewModel
    {
        /// <summary>
        /// Review id
        /// </summary>
        [BsonRepresentation(BsonType.ObjectId)]
        public string Id { get; set; }
        /// <summary>
        /// It corresonds to the author of the review
        /// </summary>
        public RegisterModel ProfilePicture { get; set; }
    }
}
