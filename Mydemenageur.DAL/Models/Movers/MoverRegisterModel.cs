using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Mydemenageur.DAL.Models.Movers
{
    public class MoverRegisterModel
    {
        [BsonRepresentation(BsonType.ObjectId)]
        /// <summary>
        /// The user id associate with the mover
        /// </summary>
        /// <example>6030deb57116e097987bcae5</example>
        public string UserId { get; set; }
        [BsonRepresentation(BsonType.ObjectId)]
        /// <summary>
        /// The file is list
        /// </summary>
        public List<string> FileId { get; set; }

    }
}
