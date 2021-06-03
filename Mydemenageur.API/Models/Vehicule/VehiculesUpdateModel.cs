using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Mydemenageur.API.Models.Vehicule
{
    public class VehiclesUpdateModel
    {
        [Required]
        /// <summary>
        /// Number of vehicule in society
        /// </summary>
        public int VehiclesNumber { get; set; }
        [Required]
        /// <summary>
        /// If the society have tarpaulin vehicule
        /// </summary>
        public bool HasTarpaulinVehicule { get; set; }
        [Required]
        /// <summary>
        /// PTAC of tarpaulin ptac
        /// </summary>
        public float PTAC_TarpaulinVehicule { get; set; }
        [Required]
        /// <summary>
        /// If the society have hard wall vehicule
        /// </summary>
        public bool HasHardWallVehicule { get; set; }
        [Required]
        /// <summary>
        /// PTAC of hard wall ptac
        /// </summary>
        public float PTAC_HardWallVehicule { get; set; }
        [Required]
        /// <summary>
        /// If he can transport horse
        /// </summary>
        public bool CanTransportHorse { get; set; }
        [Required]
        /// <summary>
        /// If he can transport vehicule
        /// </summary>
        public bool CanTransportVehicule { get; set; }
        [Required]
        /// <summary>
        /// Total capacity of all vehicule in the society
        /// </summary>
        public float TotalCapacity { get; set; }
    }
}
