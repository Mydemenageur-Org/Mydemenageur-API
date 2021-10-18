using Mydemenageur.DAL.Entities;
using Mydemenageur.DAL.Models.Services;
using Access = Mydemenageur.DAL.Entities;


namespace Mydemenageur.BLL.Extensions
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
