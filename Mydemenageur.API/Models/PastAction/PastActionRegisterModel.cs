using MongoDB.Bson.Serialization.Attributes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Mydemenageur.API.Models.PastAction
{
    public class PastActionRegisterModel
    {
        /// <summary>
        /// The icon of the action 
        /// </summary>
        [BsonRepresentation(MongoDB.Bson.BsonType.ObjectId)]
        public string ActionIcon { get; set; }

        /// <summary>
        /// The title of action
        /// </summary>
        public string Title { get; set; }

        /// <summary>
        /// The description of the action
        /// </summary>
        public string Description { get; set; }

        /// <summary>
        /// The user's id 
        /// </summary>
        [BsonRepresentation(MongoDB.Bson.BsonType.ObjectId)]
        public string UserId { get; set; }
    }
}
