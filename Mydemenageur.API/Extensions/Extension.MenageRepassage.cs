using Mydemenageur.API.Entities;
using Mydemenageur.API.Models.Services;
using Access = Mydemenageur.API.Entities;


namespace Mydemenageur.API.Extensions
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
