using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Mydemenageur.API.Models.Vehicule
{
    public class VehiculeRegisterModel
    {
        /// <summary>
        /// Number of vehicule in society
        /// </summary>
        public int VehiculeNumber { get; set; }
        /// <summary>
        /// If the society have tarpaulin vehicule
        /// </summary>
        public bool TarpaulinVehicule { get; set; }
        /// <summary>
        /// PTAC of tarpaulin ptac
        /// </summary>
        public float PTAC_TarpaulinVehicule { get; set; }
        /// <summary>
        /// If the society have hard wall vehicule
        /// </summary>
        public bool HardWallVehicule { get; set; }
        /// <summary>
        /// PTAC of hard wall ptac
        /// </summary>
        public float PTAC_HardWallVehicule { get; set; }
        /// <summary>
        /// If he can transport horse
        /// </summary>
        public bool HorseTransport { get; set; }
        /// <summary>
        /// If he can transport vehicule
        /// </summary>
        public bool VehiculeTransport { get; set; }
        /// <summary>
        /// Total capacity of all vehicule in the society
        /// </summary>
        public float TotalCapacity { get; set; }

    }
}
