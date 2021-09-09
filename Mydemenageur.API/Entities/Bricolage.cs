using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Mydemenageur.API.Entities
{
    public class Bricolage
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string Id { get; set; }
        public string BricolageType { get; set; }
        public string Address { get; set; }
        public string Budget { get; set; }
        public DateTime CreatedAt {get; set;}
        public bool IsPro { get; set; }
        public DateTime DateEvent { get; set; }
    }
}
