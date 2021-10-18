using Mydemenageur.DAL.Entities;
using Mydemenageur.DAL.Models.Services;
using Access = Mydemenageur.DAL.Entities;


namespace Mydemenageur.BLL.Extensions
{
    static partial class Extension
    {
        public static MenageRepassageModel ToModelMen(this Access.MenageRepassage men)
        {
            if (men != null)
            {
                return GetMen(men);
            }

            return null;
        }

        private static MenageRepassageModel GetMen(MenageRepassage men)
        {
            return new MenageRepassageModel()
            {
                Menage = men.Menage,
                ClotheNumber = men.ClotheNumber,
                Frequency = men.Frequency
            };
        }
    }
}
