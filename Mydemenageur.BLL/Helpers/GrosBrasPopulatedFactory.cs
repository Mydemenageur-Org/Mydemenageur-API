using Mydemenageur.DAL.Models;
using Mydemenageur.DAL.Models.Users;

namespace Mydemenageur.BLL.Helpers
{
    public static class GrosBrasPopulatedFactory
    {
        public static GrosBrasPopulated GenerateGrosBrasPopulated(GrosBras grosBras, City city, MyDemenageurUser myDemUser) 
        {
            GrosBrasPopulated grosBrasPopulated = new GrosBrasPopulated
            {
                Id = grosBras.Id,
                MyDemenageurUserId = myDemUser,
                ServicesProposed = grosBras.ServicesProposed,
                DiplomaOrExperiences = grosBras.DiplomaOrExperiences,
                Description = grosBras.Description,
                Commitment = grosBras.Commitment,
                ProStatus = grosBras.ProStatus,
                CityId = city,
                Departement = grosBras.Departement,
                CreatedAt = grosBras.CreatedAt,
                UpdatedAt = grosBras.UpdatedAt,
                VeryGoodGrade = grosBras.VeryGoodGrade,
                GoodGrade = grosBras.GoodGrade,
                MediumGrade = grosBras.MediumGrade,
                BadGrade = grosBras.BadGrade,
                Equipment = grosBras.Equipment,
                Title = grosBras.Title
            };

            return grosBrasPopulated;
        }
    }
}
