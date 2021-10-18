using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Mydemenageur.DAL.Entities
{
    public class Housing
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string Id { get; set; }

        /// <summary>
        /// The id of the user possessing the housing
        /// </summary>
        [BsonRepresentation(BsonType.ObjectId)]
        public string UserId { get; set; }

        /// <summary>
        /// The id of the move request
        /// </summary>
        [BsonRepresentation(BsonType.ObjectId)]
        public string MoveRequestId { get; set; }
        
        /// <summary>
        /// The housing type
        /// </summary>
        /// <example>House</example>
        public string HousingType { get; set; }
        /// <summary>
        /// The housing floor
        /// </summary>
        /// <example>5</example>
        public int HousingFloor { get; set; }
        /// <summary>
        /// If the housing have elevator
        /// </summary>
        public bool IsElevator { get; set; }
        /// <summary>
        /// The housing surface
        /// </summary>
        /// <example>36</example>
        public double Surface { get; set; }
        /// <summary>
        /// The housing Address
        /// </summary>
        /// <example>36 Rue les coquelicots</example>
        public string Address { get; set; }
        /// <summary>
        /// The housing town
        /// </summary>
        /// <example>Rennes</example>
        public string Town { get; set; }
        /// <summary>
        /// The houisng zipcode
        /// </summary>
        /// <example>35000</example>
        public string Zipcode { get; set; }
        /// <summary>
        /// The housing region
        /// </summary>
        /// <example>Bretagne</example>
        public string Region { get; set; }
        /// <summary>
        /// The housing country
        /// </summary>
        /// <example>France</example>
        public string Country { get; set; }
        /// <summary>
        /// If the housing is de start or the end
        /// </summary>
        /// <example>Start</example>
        /// <example>End</example>
        public string State { get; set; }

    }
}
