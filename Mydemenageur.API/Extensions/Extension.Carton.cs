using Mydemenageur.API.Entities;
using Mydemenageur.API.Models.Services;
using Access = Mydemenageur.API.Entities;
using System;

namespace Mydemenageur.API.Extensions
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
