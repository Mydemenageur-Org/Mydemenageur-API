using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Mydemenageur.API.Entities
{
    public class DemenagementIndividuel
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string Id { get; set; }
        [BsonRepresentation(BsonType.ObjectId)]
        public string DemenagementId { get; set; }
        public bool AskHelpStart { get; set; }
        public bool AskHelpDest { get; set; }
        public bool IsFlexibleDate { get; set; }
        public string PersonnNeeded { get; set; }
        public string Volume { get; set; }

    }
}
