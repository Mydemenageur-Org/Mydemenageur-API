using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Mydemenageur.API.Entities
{
    public class MoveRequest
    {

        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string Id { get; set; }
        /// <summary>
        /// The user id who request the move
        /// </summary>
        [BsonRepresentation(BsonType.ObjectId)]
        public string UserId { get; set; }
        /// <summary>
        /// The move request title
        /// </summary>
        /// <example>Déménagement d'une maison 4 pièces</example>
        public string Title { get; set; }
        /// <summary>
        /// The volume of move request
        /// </summary>
        /// <example>25</example>
        public float MoveRequestVolume { get; set; }
        /// <summary>
        /// If you need funitures to move
        /// </summary>
        public bool NeedFurnitures { get; set; }
        /// <summary>
        /// If you need assembly to move
        /// </summary>
        public bool NeedAssembly { get; set; }
        /// <summary>
        /// If you need diassembly to move
        /// </summary>
        public bool NeedDiassembly { get; set; }
        /// <summary>
        /// The minimum move request date
        /// </summary>
        public DateTime MinimumRequestDate { get; set; }
        /// <summary>
        /// The maximum move request date
        /// </summary>
        public DateTime MaximumRequestDate { get; set; }
        /// <summary>
        /// If the move request have heavy furnitures
        /// </summary>
        public List<string> HeavyFurnitures { get; set; }
        /// <summary>
        /// All other information for the move request
        /// </summary>
        public string AdditionalInformation { get; set; }

    }
}
