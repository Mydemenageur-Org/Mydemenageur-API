using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using System;
using System.Collections.Generic;
using Mydemenageur.DAL.Models.Users;

namespace Mydemenageur.DAL.Models.GenericService
{
    public class GenericServicePopulated
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string Id { get; set; }

        public MyDemenageurUser User { get; set; }
        public bool IsGenericForm { get; set; }

        public string Category { get; set; }
        public string Name { get; set; }

        public DateTime Date { get; set; }

        public List<GenericServiceField> Fields { get; set; }
        public bool IsDone { get; set; } = false;
    }
}
