using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using System;

namespace Mydemenageur.API.Entities
{
    public class Review
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string Id { get; set; }

        /// <summary>
        /// The id of the user associated with the review
        /// </summary>
        /// <example>6030deb57116e097987bcae5</example>
        [BsonRepresentation(BsonType.ObjectId)]
        public string UserId { get; set; }

        /// <summary>
        /// The title review
        /// </summary>
        /// <example>Je recommande ce site !</example>
        public string Title { get; set; }

        /// <summary>
        /// The date when it has been created
        /// </summary>
        public DateTime CreatedAt { get; set; }

        /// <summary>
        /// The description of the review
        /// </summary>
        /// <example> Tout seul pour déménager c'était compliqué. Mais grâce aux gros bras, en 2h c'était plié</example>
        public string Description { get; set; }

        /// <summary>
        /// The grade of the review
        /// </summary>
        /// <example> 5 </example>
        public string Grade { get; set; }

    }
}
