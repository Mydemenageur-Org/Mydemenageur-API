using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Mydemenageur.DAL.Entities
{
    public class Vehicles
    {

        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string Id { get; set; }
        [BsonRepresentation(BsonType.ObjectId)]
        /// <summary>
        /// The society own this vehicle
        /// </summary>
        public string SocietyId { get; set; }
        /// <summary>
        /// Number of vehicle in society
        /// </summary>
        public int VehiclesNumber { get; set; }
        /// <summary>
        /// If the society have tarpaulin vehicle
        /// </summary>
        public bool HasTarpaulinVehicle { get; set; }
        /// <summary>
        /// PTAC of tarpaulin ptac
        /// </summary>
        public double PTAC_TarpaulinVehicle { get; set; }
        /// <summary>
        /// If the society have hard wall vehicle
        /// </summary>
        public bool HasHardWallVehicle { get; set; }
        /// <summary>
        /// PTAC of hard wall ptac
        /// </summary>
        public double PTAC_HardWallVehicle { get; set; }
        /// <summary>
        /// If he can transport horse
        /// </summary>
        public bool CanTransportHorse { get; set; }
        /// <summary>
        /// If he can transport vehicle
        /// </summary>
        public bool CanTransportVehicle { get; set; }
        /// <summary>
        /// Total capacity of all vehicle in the society
        /// </summary>
        public double TotalCapacity { get; set; }



    }
}
