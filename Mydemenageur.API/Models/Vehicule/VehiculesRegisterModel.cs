using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Mydemenageur.API.Models.Vehicule
{
    public class VehiclesRegisterModel
    {
        [BsonRepresentation(BsonType.ObjectId)]
        /// <summary>
        /// The society of this vehicle
        /// </summary>
        public string SocietyId { get; set; }
        /// <summary>
        /// Number of vehicule in society
        /// </summary>
        public int VehiclesNumber { get; set; }
        /// <summary>
        /// If the society have tarpaulin vehicule
        /// </summary>
        public bool HasTarpaulinVehicule { get; set; }
        /// <summary>
        /// PTAC of tarpaulin ptac
        /// </summary>
        public float PTAC_TarpaulinVehicule { get; set; }
        /// <summary>
        /// If the society have hard wall vehicule
        /// </summary>
        public bool HasHardWallVehicule { get; set; }
        /// <summary>
        /// PTAC of hard wall ptac
        /// </summary>
        public float PTAC_HardWallVehicule { get; set; }
        /// <summary>
        /// If he can transport horse
        /// </summary>
        public bool CanTransportHorse { get; set; }
        /// <summary>
        /// If he can transport vehicule
        /// </summary>
        public bool CanTransportVehicule { get; set; }
        /// <summary>
        /// Total capacity of all vehicule in the society
        /// </summary>
        public float TotalCapacity { get; set; }

    }
}
