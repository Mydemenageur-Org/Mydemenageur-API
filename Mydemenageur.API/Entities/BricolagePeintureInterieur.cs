using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Mydemenageur.API.Entities
{
    public class BricolagePeintureInterieur
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string Id { get; set; }
        [BsonRepresentation(BsonType.ObjectId)]
        public string BricolageId { get; set; }
        public string numberNeeded { get; set; }
        public string Label { get; set; }
        public string Category { get; set; }
        public string SmallRoom { get; set; }
        public string MediumRoom { get; set; }
        public string LargeRoom { get; set; }
        public bool PasserDeuxCouche { get; set; }
    }
}
