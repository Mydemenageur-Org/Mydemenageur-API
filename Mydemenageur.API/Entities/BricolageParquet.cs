using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Mydemenageur.API.Entities
{
    public class BricolageParquet
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string Id { get; set; }
        [BsonRepresentation(BsonType.ObjectId)]
        public string BricolageId { get; set; }
        public string NumberNeeded { get; set; }
        public string Surface { get; set; }
        public string Label { get; set; }
        public string Category { get; set; }
        public bool isPlinthes { get; set; }
        public bool isCut { get; set; }
    }
}
