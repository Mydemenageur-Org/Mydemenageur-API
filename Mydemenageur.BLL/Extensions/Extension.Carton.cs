using Mydemenageur.DAL.Entities;
using Mydemenageur.DAL.Models.Services;
using Access = Mydemenageur.DAL.Entities;
using System;

namespace Mydemenageur.BLL.Extensions
{
    static partial class Extension
    {
        public static CartonModel ToModelCarton(this Access.Carton cartons)
        {
            if(cartons != null)
            {
                return GetCartonModel(cartons);
            }
            return null;
        }

        private static CartonModel GetCartonModel(Carton cartons)
        {
            return new CartonModel()
            {
                ResearchType = cartons.ResearchType,
                BoxNumber = cartons.BoxNumber,
                BoxSize = cartons.BoxSize,
                DateDisponibility = cartons.DateDisponibility,
                isFlexible = cartons.isFlexible,
                StartDisponibility = cartons.StartDisponibility,
                EndDisponibility = cartons.EndDisponibility,
                ZipCode = cartons.ZipCode,
                City = cartons.City,
                AnnounceTitle = cartons.AnnounceTitle,
                CreatedAt = DateTime.Now,
                ModifiedAt = DateTime.Now,
                UserId = cartons.UserId
            };
        }
    }
}
