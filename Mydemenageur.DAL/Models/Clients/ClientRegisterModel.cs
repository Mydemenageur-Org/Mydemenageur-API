using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace Mydemenageur.DAL.Models.Clients
{
    public class ClientRegisterModel
    {
        /// <summary>
        /// The id of the user associated with the client
        /// </summary>
        /// <example>6030deb57116e097987bcae5</example>
        [BsonRepresentation(BsonType.ObjectId)]
        public string UserId { get; set; }
    }
}
