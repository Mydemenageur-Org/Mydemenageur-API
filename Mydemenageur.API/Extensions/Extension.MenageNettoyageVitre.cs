using Mydemenageur.API.Entities;
using Mydemenageur.API.Models.Services;
using Access = Mydemenageur.API.Entities;


namespace Mydemenageur.API.Extensions
{
    static partial class Extension
    {
        public static MenageNettoyageVitreModel ToModelMen(this Access.MenageNettoyageVitre men)
        {
            if (men != null)
            {
                return GetMen(men);
            }

            return null;
        }

        private static MenageNettoyageVitreModel GetMen(MenageNettoyageVitre men)
        {
            return new MenageNettoyageVitreModel()
            {
                Menage = men.Menage,
                WindowNumber = men.WindowNumber
            };
        }
    }
}
