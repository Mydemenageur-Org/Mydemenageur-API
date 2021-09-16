using Mydemenageur.API.Entities;
using Mydemenageur.API.Models.Services;
using Access = Mydemenageur.API.Entities;


namespace Mydemenageur.API.Extensions
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
