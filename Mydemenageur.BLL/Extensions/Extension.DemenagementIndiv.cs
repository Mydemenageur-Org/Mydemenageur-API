using Mydemenageur.DAL.Entities;
using Mydemenageur.DAL.Models.Services;
using Access = Mydemenageur.DAL.Entities;


namespace Mydemenageur.BLL.Extensions
{
    static partial class Extension
    {
        public static DemenagementIndivModel ToModelDemIndiv(this Access.DemenagementIndividuel demIndiv)
        {
            if(demIndiv != null)
            {
                return GetDemenagementIndiv(demIndiv);
            }

            return null;
        }

        private static DemenagementIndivModel GetDemenagementIndiv(DemenagementIndividuel demIndiv)
        {
            return new DemenagementIndivModel()
            {
                Demenagement = demIndiv.Demenagement,
                AskHelpDest = demIndiv.AskHelpDest,
                AskHelpStart = demIndiv.AskHelpStart,
                IsFlexibleDate = demIndiv.IsFlexibleDate,
                PersonnNeeded = demIndiv.PersonnNeeded,
                Volume = demIndiv.Volume
            };
        }
    }
}
