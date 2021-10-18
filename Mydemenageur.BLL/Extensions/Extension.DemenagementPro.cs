using Mydemenageur.DAL.Entities;
using Mydemenageur.DAL.Models.Services;
using Access = Mydemenageur.DAL.Entities;

namespace Mydemenageur.BLL.Extensions
{
    static partial class Extension
    {
        public static DemenagementProModel ToModelDemPro(this Access.DemenagementPro demsPro)
        {
            if(demsPro != null)
            {
                return GetDemPro(demsPro);
            }
            return null;
        }

        private static DemenagementProModel GetDemPro(DemenagementPro demsPro)
        {
            return new DemenagementProModel()
            {
                Demenagement = demsPro.Demenagement,
                IsHouse = demsPro.IsHouse,
                HasMultipleFloors = demsPro.HasMultipleFloors,
                StartZipCode = demsPro.StartZipCode,
                StartAddress = demsPro.StartAddress,
                DestZipCode = demsPro.DestZipCode,
                DestAddress = demsPro.DestAddress,
                IsStartingDateKnown = demsPro.IsStartingDateKnown,
                Volume = demsPro.Volume,
                Surface = demsPro.Surface
            };
        }


    }
}
