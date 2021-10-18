using Mydemenageur.DAL.Entities;
using Mydemenageur.DAL.Models.Services;
using Access = Mydemenageur.DAL.Entities;

namespace Mydemenageur.BLL.Extensions
{
    static partial class Extension
    {
        public static MenageLavageAutoModel ToModelMen(this Access.MenageLavageAuto men)
        {
            if (men != null)
            {
                return GetMen(men);
            }

            return null;
        }

        private static MenageLavageAutoModel GetMen(MenageLavageAuto men)
        {
            return new MenageLavageAutoModel()
            {
                Menage = men.Menage,
                VehiculeType = men.VehiculeType
            };
        }
    }
}
