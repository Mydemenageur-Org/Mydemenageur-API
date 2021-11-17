using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using System;
using System.Collections.Generic;

namespace Mydemenageur.DAL.Models.GenericService
{
    public class GenericService
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string Id { get; set; }

        [BsonRepresentation(BsonType.ObjectId)]
        public string UserId { get; set; }
        public bool IsGenericForm { get; set; }

        public string Name { get; set; }

        public DateTime Date { get; set; }

        public IList<GenericServiceField> Fields { get; set; }

    }
}
