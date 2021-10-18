using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Mydemenageur.DAL.Entities
{
    public class BricolageFourniture
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string Id { get; set; }
        [BsonRepresentation(BsonType.ObjectId)]
        public string BricolageId { get; set; }
        public string NumberNeeded { get; set; }
        public string Label { get; set; }
        public string Category { get; set; }
        public bool CleanBox { get; set; }
        public bool IsIKEA { get; set; }
        public bool IsAssembling { get; set; }
        public string SmallFurniture { get; set; }
        public string MediumFurniture { get; set; }
        public string TallFurniture { get; set; }
        public string VeryTallFurniture { get; set; }
    }
}
