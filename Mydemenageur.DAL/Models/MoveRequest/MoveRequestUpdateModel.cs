using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Mydemenageur.DAL.Models.MoveRequest
{
    public class MoveRequestUpdateModel
    {

        [Required]
        [BsonRepresentation(BsonType.ObjectId)]
        public string UserId { get; set; }
        [Required]
        /// <summary>
        /// The move request title
        /// </summary>
        /// <example>Déménagement d'une maison 4 pièces</example>
        public string Title { get; set; }
        [Required]
        /// <summary>
        /// The volume of move request
        /// </summary>
        /// <example>25</example>
        public double MoveRequestVolume { get; set; }
        [Required]
        /// <summary>
        /// If you need funitures to move
        /// </summary>
        public bool NeedFurnitures { get; set; }
        [Required]
        /// <summary>
        /// If you need assembly to move
        /// </summary>
        public bool NeedAssembly { get; set; }
        [Required]
        /// <summary>
        /// If you need diassembly to move
        /// </summary>
        public bool NeedDiassembly { get; set; }
        [Required]
        /// <summary>
        /// The minimum move request date
        /// </summary>
        public DateTime MinimumRequestDate { get; set; }
        [Required]
        /// <summary>
        /// The maximum move request date
        /// </summary>
        public DateTime MaximumRequestDate { get; set; }
        [Required]
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
